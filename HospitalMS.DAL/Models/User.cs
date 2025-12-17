using System;

namespace HospitalMS.DAL.Models
{
    /// <summary>
    /// User Model - แทนข้อมูลผู้ใช้งานระบบ
    /// </summary>
    /// <remarks>
    /// Model คือคลาสที่แทนข้อมูลในตาราง (Table) ของฐานข้อมูล
    /// Properties ในคลาสนี้จะตรงกับ Columns ในตาราง Users
    /// </remarks>
    public class User
    {
        #region Properties

        /// <summary>
        /// รหัสผู้ใช้ (Primary Key)
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// ชื่อผู้ใช้สำหรับ Login
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// รหัสผ่านที่เข้ารหัสแล้ว (Hash)
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// ชื่อ-นามสกุลเต็ม
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// อีเมล
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// เบอร์โทรศัพท์
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// รหัสบทบาท (Foreign Key ไปยัง Roles)
        /// </summary>
        public int RoleID { get; set; }

        /// <summary>
        /// ชื่อบทบาท (จาก JOIN กับตาราง Roles)
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// สถานะการใช้งาน (true = ใช้งาน, false = ระงับ)
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// วันที่สร้างบัญชี
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// วันที่ Login ครั้งล่าสุด
        /// </summary>
        public DateTime? LastLogin { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor แบบไม่มี Parameter (Default Constructor)
        /// </summary>
        public User()
        {
            // กำหนดค่าเริ่มต้น
            IsActive = true;
            CreatedDate = DateTime.Now;
        }

        /// <summary>
        /// Constructor แบบมี Parameter
        /// </summary>
        public User(string username, string passwordHash, string fullName, int roleID)
        {
            Username = username;
            PasswordHash = passwordHash;
            FullName = fullName;
            RoleID = roleID;
            IsActive = true;
            CreatedDate = DateTime.Now;
        }

        #endregion

        #region Methods

        /// <summary>
        /// แสดงข้อมูลผู้ใช้
        /// </summary>
        public override string ToString()
        {
            return $"{FullName} ({Username}) - {RoleName}";
        }

        #endregion
    }
}
