using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace HospitalMS.DAL
{
    /// <summary>
    /// DatabaseHelper - คลาสช่วยเชื่อมต่อและทำงานกับฐานข้อมูล
    /// </summary>
    /// <remarks>
    /// คลาสนี้ใช้ Singleton Pattern เพื่อให้มี instance เดียวในทั้งโปรแกรม
    /// ช่วยจัดการการเชื่อมต่อฐานข้อมูล SQL Server
    /// </remarks>
    public class DatabaseHelper
    {
        #region Singleton Pattern

        // Instance เดียวของ DatabaseHelper (Singleton)
        private static DatabaseHelper _instance;
        
        // Object สำหรับ lock เพื่อป้องกันการสร้าง instance หลายตัวพร้อมกัน
        private static readonly object _lock = new object();

        /// <summary>
        /// ดึง Instance ของ DatabaseHelper (Singleton Pattern)
        /// </summary>
        public static DatabaseHelper Instance
        {
            get
            {
                // ถ้ายังไม่มี instance ให้สร้างใหม่
                if (_instance == null)
                {
                    lock (_lock) // ป้องกันการสร้างพร้อมกัน (Thread-safe)
                    {
                        if (_instance == null)
                        {
                            _instance = new DatabaseHelper();
                        }
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Connection String สำหรับเชื่อมต่อฐานข้อมูล
        /// อ่านจากไฟล์ App.config
        /// </summary>
        private string ConnectionString { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor แบบ Private (เพื่อใช้ Singleton Pattern)
        /// </summary>
        private DatabaseHelper()
        {
            try
            {
                // อ่าน Connection String จาก App.config
                ConnectionString = ConfigurationManager.ConnectionStrings["HospitalDB"].ConnectionString;
            }
            catch (Exception ex)
            {
                throw new Exception("ไม่สามารถอ่าน Connection String ได้: " + ex.Message);
            }
        }

        #endregion

        #region Connection Methods

        /// <summary>
        /// สร้าง SqlConnection ใหม่
        /// </summary>
        /// <returns>SqlConnection object</returns>
        public SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        /// <summary>
        /// ทดสอบการเชื่อมต่อฐานข้อมูล
        /// </summary>
        /// <returns>true ถ้าเชื่อมต่อสำเร็จ, false ถ้าไม่สำเร็จ</returns>
        public bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Execute Methods

        /// <summary>
        /// Execute Query ที่ไม่ต้องการผลลัพธ์ (INSERT, UPDATE, DELETE)
        /// </summary>
        /// <param name="query">SQL Query</param>
        /// <param name="parameters">Parameters (optional)</param>
        /// <returns>จำนวนแถวที่ได้รับผลกระทบ</returns>
        public int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // เพิ่ม Parameters ถ้ามี
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        conn.Open();
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("เกิดข้อผิดพลาดในการ Execute Query: " + ex.Message);
            }
        }

        /// <summary>
        /// Execute Query ที่ต้องการค่าเดียว (SELECT COUNT, SELECT MAX, etc.)
        /// </summary>
        /// <param name="query">SQL Query</param>
        /// <param name="parameters">Parameters (optional)</param>
        /// <returns>ค่าที่ได้จาก Query</returns>
        public object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        conn.Open();
                        return cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("เกิดข้อผิดพลาดในการ Execute Scalar: " + ex.Message);
            }
        }

        /// <summary>
        /// Execute Query ที่ต้องการอ่านข้อมูลหลายแถว (SELECT)
        /// </summary>
        /// <param name="query">SQL Query</param>
        /// <param name="parameters">Parameters (optional)</param>
        /// <returns>SqlDataReader สำหรับอ่านข้อมูล</returns>
        /// <remarks>
        /// ต้องใช้ใน using statement และปิด connection เอง
        /// </remarks>
        public SqlDataReader ExecuteReader(string query, SqlParameter[] parameters = null)
        {
            try
            {
                SqlConnection conn = GetConnection();
                SqlCommand cmd = new SqlCommand(query, conn);

                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                conn.Open();
                
                // CommandBehavior.CloseConnection จะปิด connection อัตโนมัติเมื่อปิด reader
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw new Exception("เกิดข้อผิดพลาดในการ Execute Reader: " + ex.Message);
            }
        }

        /// <summary>
        /// Execute Query และคืนค่าเป็น DataTable
        /// </summary>
        /// <param name="query">SQL Query</param>
        /// <param name="parameters">Parameters (optional)</param>
        /// <returns>DataTable ที่มีข้อมูล</returns>
        public DataTable ExecuteDataTable(string query, SqlParameter[] parameters = null)
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("เกิดข้อผิดพลาดในการ Execute DataTable: " + ex.Message);
            }
        }

        /// <summary>
        /// Execute Stored Procedure
        /// </summary>
        /// <param name="procedureName">ชื่อ Stored Procedure</param>
        /// <param name="parameters">Parameters</param>
        /// <returns>จำนวนแถวที่ได้รับผลกระทบ</returns>
        public int ExecuteStoredProcedure(string procedureName, SqlParameter[] parameters = null)
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(procedureName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        conn.Open();
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("เกิดข้อผิดพลาดในการ Execute Stored Procedure: " + ex.Message);
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// สร้าง SqlParameter
        /// </summary>
        /// <param name="parameterName">ชื่อ Parameter (เช่น @Username)</param>
        /// <param name="value">ค่า</param>
        /// <returns>SqlParameter</returns>
        public SqlParameter CreateParameter(string parameterName, object value)
        {
            return new SqlParameter(parameterName, value ?? DBNull.Value);
        }

        /// <summary>
        /// สร้าง SqlParameter array จาก Dictionary
        /// </summary>
        /// <param name="parameters">Dictionary ของ parameter name และ value</param>
        /// <returns>SqlParameter array</returns>
        public SqlParameter[] CreateParameters(System.Collections.Generic.Dictionary<string, object> parameters)
        {
            if (parameters == null || parameters.Count == 0)
                return null;

            SqlParameter[] sqlParams = new SqlParameter[parameters.Count];
            int index = 0;

            foreach (var param in parameters)
            {
                sqlParams[index++] = CreateParameter(param.Key, param.Value);
            }

            return sqlParams;
        }

        #endregion
    }
}
