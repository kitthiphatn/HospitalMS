using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HospitalMS.DAL.Models;

namespace HospitalMS.DAL.Repositories
{
    /// <summary>
    /// UserRepository - จัดการข้อมูล User ในฐานข้อมูล
    /// </summary>
    /// <remarks>
    /// Repository Pattern = แยกส่วนการเข้าถึงข้อมูลออกจาก Business Logic
    /// ทำให้โค้ดสะอาด ง่ายต่อการทดสอบและบำรุงรักษา
    /// </remarks>
    public class UserRepository
    {
        #region Private Fields

        private readonly DatabaseHelper _db;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor - สร้าง UserRepository
        /// </summary>
        public UserRepository()
        {
            _db = DatabaseHelper.Instance;
        }

        #endregion

        #region Authentication Methods

        /// <summary>
        /// ตรวจสอบการ Login
        /// </summary>
        /// <param name="username">ชื่อผู้ใช้</param>
        /// <param name="password">รหัสผ่าน</param>
        /// <returns>true ถ้า Login สำเร็จ, false ถ้าไม่สำเร็จ</returns>
        public bool ValidateLogin(string username, string password)
        {
            try
            {
                string query = @"
                    SELECT COUNT(*) 
                    FROM Users 
                    WHERE Username = @Username 
                    AND PasswordHash = @Password 
                    AND IsActive = 1";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Username", username),
                    new SqlParameter("@Password", password)
                };

                int count = Convert.ToInt32(_db.ExecuteScalar(query, parameters));
                return count > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("เกิดข้อผิดพลาดในการตรวจสอบ Login: " + ex.Message);
            }
        }

        /// <summary>
        /// ดึงข้อมูลผู้ใช้จาก Username
        /// </summary>
        /// <param name="username">ชื่อผู้ใช้</param>
        /// <returns>User object หรือ null ถ้าไม่พบ</returns>
        public User GetUserByUsername(string username)
        {
            try
            {
                string query = @"
                    SELECT u.UserID, u.Username, u.PasswordHash, u.FullName, 
                           u.Email, u.Phone, u.RoleID, r.RoleName, 
                           u.IsActive, u.CreatedDate, u.LastLogin
                    FROM Users u
                    INNER JOIN Roles r ON u.RoleID = r.RoleID
                    WHERE u.Username = @Username";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Username", username)
                };

                using (SqlDataReader reader = _db.ExecuteReader(query, parameters))
                {
                    if (reader.Read())
                    {
                        return MapReaderToUser(reader);
                    }
                }

                return null; // ไม่พบผู้ใช้
            }
            catch (Exception ex)
            {
                throw new Exception("เกิดข้อผิดพลาดในการดึงข้อมูลผู้ใช้: " + ex.Message);
            }
        }

        /// <summary>
        /// อัพเดทเวลา Login ล่าสุด
        /// </summary>
        /// <param name="userID">รหัสผู้ใช้</param>
        /// <returns>true ถ้าสำเร็จ</returns>
        public bool UpdateLastLogin(int userID)
        {
            try
            {
                string query = @"
                    UPDATE Users 
                    SET LastLogin = @LastLogin 
                    WHERE UserID = @UserID";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@LastLogin", DateTime.Now),
                    new SqlParameter("@UserID", userID)
                };

                int rowsAffected = _db.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("เกิดข้อผิดพลาดในการอัพเดทเวลา Login: " + ex.Message);
            }
        }

        #endregion

        #region CRUD Methods

        /// <summary>
        /// ดึงข้อมูลผู้ใช้ทั้งหมด
        /// </summary>
        /// <returns>List ของ User</returns>
        public List<User> GetAllUsers()
        {
            try
            {
                string query = @"
                    SELECT u.UserID, u.Username, u.PasswordHash, u.FullName, 
                           u.Email, u.Phone, u.RoleID, r.RoleName, 
                           u.IsActive, u.CreatedDate, u.LastLogin
                    FROM Users u
                    INNER JOIN Roles r ON u.RoleID = r.RoleID
                    ORDER BY u.FullName";

                List<User> users = new List<User>();

                using (SqlDataReader reader = _db.ExecuteReader(query))
                {
                    while (reader.Read())
                    {
                        users.Add(MapReaderToUser(reader));
                    }
                }

                return users;
            }
            catch (Exception ex)
            {
                throw new Exception("เกิดข้อผิดพลาดในการดึงข้อมูลผู้ใช้ทั้งหมด: " + ex.Message);
            }
        }

        /// <summary>
        /// ดึงข้อมูลผู้ใช้จาก UserID
        /// </summary>
        /// <param name="userID">รหัสผู้ใช้</param>
        /// <returns>User object หรือ null</returns>
        public User GetUserByID(int userID)
        {
            try
            {
                string query = @"
                    SELECT u.UserID, u.Username, u.PasswordHash, u.FullName, 
                           u.Email, u.Phone, u.RoleID, r.RoleName, 
                           u.IsActive, u.CreatedDate, u.LastLogin
                    FROM Users u
                    INNER JOIN Roles r ON u.RoleID = r.RoleID
                    WHERE u.UserID = @UserID";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserID", userID)
                };

                using (SqlDataReader reader = _db.ExecuteReader(query, parameters))
                {
                    if (reader.Read())
                    {
                        return MapReaderToUser(reader);
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("เกิดข้อผิดพลาดในการดึงข้อมูลผู้ใช้: " + ex.Message);
            }
        }

        /// <summary>
        /// เพิ่มผู้ใช้ใหม่
        /// </summary>
        /// <param name="user">ข้อมูลผู้ใช้</param>
        /// <returns>UserID ของผู้ใช้ที่สร้างใหม่</returns>
        public int AddUser(User user)
        {
            try
            {
                string query = @"
                    INSERT INTO Users (Username, PasswordHash, FullName, Email, Phone, RoleID, IsActive)
                    VALUES (@Username, @Password, @FullName, @Email, @Phone, @RoleID, @IsActive);
                    SELECT CAST(SCOPE_IDENTITY() AS INT);";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Username", user.Username),
                    new SqlParameter("@Password", user.PasswordHash),
                    new SqlParameter("@FullName", user.FullName),
                    new SqlParameter("@Email", user.Email ?? (object)DBNull.Value),
                    new SqlParameter("@Phone", user.Phone ?? (object)DBNull.Value),
                    new SqlParameter("@RoleID", user.RoleID),
                    new SqlParameter("@IsActive", user.IsActive)
                };

                int newUserID = Convert.ToInt32(_db.ExecuteScalar(query, parameters));
                return newUserID;
            }
            catch (Exception ex)
            {
                throw new Exception("เกิดข้อผิดพลาดในการเพิ่มผู้ใช้: " + ex.Message);
            }
        }

        /// <summary>
        /// แก้ไขข้อมูลผู้ใช้
        /// </summary>
        /// <param name="user">ข้อมูลผู้ใช้ที่ต้องการแก้ไข</param>
        /// <returns>true ถ้าสำเร็จ</returns>
        public bool UpdateUser(User user)
        {
            try
            {
                string query = @"
                    UPDATE Users 
                    SET FullName = @FullName,
                        Email = @Email,
                        Phone = @Phone,
                        RoleID = @RoleID,
                        IsActive = @IsActive
                    WHERE UserID = @UserID";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@FullName", user.FullName),
                    new SqlParameter("@Email", user.Email ?? (object)DBNull.Value),
                    new SqlParameter("@Phone", user.Phone ?? (object)DBNull.Value),
                    new SqlParameter("@RoleID", user.RoleID),
                    new SqlParameter("@IsActive", user.IsActive),
                    new SqlParameter("@UserID", user.UserID)
                };

                int rowsAffected = _db.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("เกิดข้อผิดพลาดในการแก้ไขข้อมูลผู้ใช้: " + ex.Message);
            }
        }

        /// <summary>
        /// เปลี่ยนรหัสผ่าน
        /// </summary>
        /// <param name="userID">รหัสผู้ใช้</param>
        /// <param name="newPassword">รหัสผ่านใหม่</param>
        /// <returns>true ถ้าสำเร็จ</returns>
        public bool ChangePassword(int userID, string newPassword)
        {
            try
            {
                string query = @"
                    UPDATE Users 
                    SET PasswordHash = @Password 
                    WHERE UserID = @UserID";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Password", newPassword),
                    new SqlParameter("@UserID", userID)
                };

                int rowsAffected = _db.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("เกิดข้อผิดพลาดในการเปลี่ยนรหัสผ่าน: " + ex.Message);
            }
        }

        /// <summary>
        /// ลบผู้ใช้ (Soft Delete - เปลี่ยน IsActive เป็น false)
        /// </summary>
        /// <param name="userID">รหัสผู้ใช้</param>
        /// <returns>true ถ้าสำเร็จ</returns>
        public bool DeleteUser(int userID)
        {
            try
            {
                string query = @"
                    UPDATE Users 
                    SET IsActive = 0 
                    WHERE UserID = @UserID";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserID", userID)
                };

                int rowsAffected = _db.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("เกิดข้อผิดพลาดในการลบผู้ใช้: " + ex.Message);
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// แปลง SqlDataReader เป็น User object
        /// </summary>
        /// <param name="reader">SqlDataReader</param>
        /// <returns>User object</returns>
        private User MapReaderToUser(SqlDataReader reader)
        {
            return new User
            {
                UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                Username = reader.GetString(reader.GetOrdinal("Username")),
                PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
                FullName = reader.GetString(reader.GetOrdinal("FullName")),
                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
                Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? null : reader.GetString(reader.GetOrdinal("Phone")),
                RoleID = reader.GetInt32(reader.GetOrdinal("RoleID")),
                RoleName = reader.GetString(reader.GetOrdinal("RoleName")),
                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                LastLogin = reader.IsDBNull(reader.GetOrdinal("LastLogin")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("LastLogin"))
            };
        }

        #endregion
    }
}
