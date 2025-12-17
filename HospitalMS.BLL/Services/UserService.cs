using System;
using HospitalMS.DAL.Models;
using HospitalMS.DAL.Repositories;

namespace HospitalMS.BLL.Services
{
    /// <summary>
    /// UserService - Business Logic สำหรับจัดการผู้ใช้
    /// </summary>
    /// <remarks>
    /// Business Logic Layer (BLL) = ชั้นที่จัดการตรรกะทางธุรกิจ
    /// แยกออกจาก Data Access Layer (DAL) และ Presentation Layer (UI)
    /// </remarks>
    public class UserService
    {
        #region Private Fields

        private readonly UserRepository _userRepository;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor - สร้าง UserService
        /// </summary>
        public UserService()
        {
            _userRepository = new UserRepository();
        }

        #endregion

        #region Authentication Methods

        /// <summary>
        /// ตรวจสอบการ Login และคืนค่าข้อมูลผู้ใช้
        /// </summary>
        /// <param name="username">ชื่อผู้ใช้</param>
        /// <param name="password">รหัสผ่าน</param>
        /// <param name="user">ข้อมูลผู้ใช้ (ถ้า Login สำเร็จ)</param>
        /// <param name="errorMessage">ข้อความ Error (ถ้า Login ไม่สำเร็จ)</param>
        /// <returns>true ถ้า Login สำเร็จ</returns>
        public bool Login(string username, string password, out User user, out string errorMessage)
        {
            user = null;
            errorMessage = string.Empty;

            try
            {
                // ตรวจสอบข้อมูลว่าง
                if (string.IsNullOrWhiteSpace(username))
                {
                    errorMessage = "กรุณาใส่ชื่อผู้ใช้";
                    return false;
                }

                if (string.IsNullOrWhiteSpace(password))
                {
                    errorMessage = "กรุณาใส่รหัสผ่าน";
                    return false;
                }

                // ตรวจสอบ Login
                if (_userRepository.ValidateLogin(username, password))
                {
                    // ดึงข้อมูลผู้ใช้
                    user = _userRepository.GetUserByUsername(username);

                    if (user != null)
                    {
                        // อัพเดทเวลา Login
                        _userRepository.UpdateLastLogin(user.UserID);
                        return true;
                    }
                    else
                    {
                        errorMessage = "ไม่พบข้อมูลผู้ใช้";
                        return false;
                    }
                }
                else
                {
                    errorMessage = "ชื่อผู้ใช้หรือรหัสผ่านไม่ถูกต้อง";
                    return false;
                }
            }
            catch (Exception ex)
            {
                errorMessage = "เกิดข้อผิดพลาด: " + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// เปลี่ยนรหัสผ่าน
        /// </summary>
        /// <param name="userID">รหัสผู้ใช้</param>
        /// <param name="oldPassword">รหัสผ่านเดิม</param>
        /// <param name="newPassword">รหัสผ่านใหม่</param>
        /// <param name="confirmPassword">ยืนยันรหัสผ่านใหม่</param>
        /// <param name="errorMessage">ข้อความ Error</param>
        /// <returns>true ถ้าสำเร็จ</returns>
        public bool ChangePassword(int userID, string oldPassword, string newPassword, string confirmPassword, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                // ตรวจสอบข้อมูล
                if (string.IsNullOrWhiteSpace(oldPassword))
                {
                    errorMessage = "กรุณาใส่รหัสผ่านเดิม";
                    return false;
                }

                if (string.IsNullOrWhiteSpace(newPassword))
                {
                    errorMessage = "กรุณาใส่รหัสผ่านใหม่";
                    return false;
                }

                if (newPassword.Length < 6)
                {
                    errorMessage = "รหัสผ่านต้องมีอย่างน้อย 6 ตัวอักษร";
                    return false;
                }

                if (newPassword != confirmPassword)
                {
                    errorMessage = "รหัสผ่านใหม่ไม่ตรงกัน";
                    return false;
                }

                // ดึงข้อมูลผู้ใช้
                User user = _userRepository.GetUserByID(userID);
                if (user == null)
                {
                    errorMessage = "ไม่พบข้อมูลผู้ใช้";
                    return false;
                }

                // ตรวจสอบรหัสผ่านเดิม
                if (user.PasswordHash != oldPassword)
                {
                    errorMessage = "รหัสผ่านเดิมไม่ถูกต้อง";
                    return false;
                }

                // เปลี่ยนรหัสผ่าน
                bool success = _userRepository.ChangePassword(userID, newPassword);

                if (!success)
                {
                    errorMessage = "ไม่สามารถเปลี่ยนรหัสผ่านได้";
                }

                return success;
            }
            catch (Exception ex)
            {
                errorMessage = "เกิดข้อผิดพลาด: " + ex.Message;
                return false;
            }
        }

        #endregion

        #region Validation Methods

        /// <summary>
        /// ตรวจสอบว่า Username ซ้ำหรือไม่
        /// </summary>
        /// <param name="username">ชื่อผู้ใช้</param>
        /// <returns>true ถ้าซ้ำ</returns>
        public bool IsUsernameDuplicate(string username)
        {
            try
            {
                User user = _userRepository.GetUserByUsername(username);
                return user != null;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// ตรวจสอบความถูกต้องของข้อมูลผู้ใช้
        /// </summary>
        /// <param name="user">ข้อมูลผู้ใช้</param>
        /// <param name="errorMessage">ข้อความ Error</param>
        /// <returns>true ถ้าข้อมูลถูกต้อง</returns>
        public bool ValidateUser(User user, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(user.Username))
            {
                errorMessage = "กรุณาใส่ชื่อผู้ใช้";
                return false;
            }

            if (user.Username.Length < 4)
            {
                errorMessage = "ชื่อผู้ใช้ต้องมีอย่างน้อย 4 ตัวอักษร";
                return false;
            }

            if (string.IsNullOrWhiteSpace(user.PasswordHash))
            {
                errorMessage = "กรุณาใส่รหัสผ่าน";
                return false;
            }

            if (user.PasswordHash.Length < 6)
            {
                errorMessage = "รหัสผ่านต้องมีอย่างน้อย 6 ตัวอักษร";
                return false;
            }

            if (string.IsNullOrWhiteSpace(user.FullName))
            {
                errorMessage = "กรุณาใส่ชื่อ-นามสกุล";
                return false;
            }

            if (user.RoleID <= 0)
            {
                errorMessage = "กรุณาเลือกบทบาท";
                return false;
            }

            return true;
        }

        #endregion
    }
}
