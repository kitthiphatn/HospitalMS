using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace HospitalMS.DAL
{
    /// <summary>
    /// DatabaseHelper - คลาสช่วยเชื่อมต่อและทำงานกับฐานข้อมูล
    /// </summary>
    public class DatabaseHelper
    {
        /// <summary>
        /// สร้าง SqlConnection ใหม่
        /// </summary>
        /// <returns>SqlConnection object</returns>
        public static SqlConnection GetConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["HospitalDB"].ConnectionString;
            return new SqlConnection(connectionString);
        }

        /// <summary>
        /// ทดสอบการเชื่อมต่อฐานข้อมูล
        /// </summary>
        /// <returns>true ถ้าเชื่อมต่อสำเร็จ, false ถ้าไม่สำเร็จ</returns>
        public static bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection Error: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Execute Query ที่ไม่ต้องการผลลัพธ์ (INSERT, UPDATE, DELETE)
        /// </summary>
        /// <param name="query">SQL Query</param>
        /// <param name="parameters">Parameters (optional)</param>
        /// <returns>จำนวนแถวที่ได้รับผลกระทบ</returns>
        public static int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (parameters != null)
                            cmd.Parameters.AddRange(parameters);

                        conn.Open();
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing non-query: " + ex.Message);
            }
        }

        /// <summary>
        /// Execute Query ที่ต้องการค่าเดียว (SELECT COUNT, SELECT MAX, etc.)
        /// </summary>
        /// <param name="query">SQL Query</param>
        /// <param name="parameters">Parameters (optional)</param>
        /// <returns>ค่าที่ได้จาก Query</returns>
        public static object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (parameters != null)
                            cmd.Parameters.AddRange(parameters);

                        conn.Open();
                        return cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing scalar: " + ex.Message);
            }
        }

        /// <summary>
        /// Execute Query และคืนค่าเป็น DataTable
        /// </summary>
        /// <param name="query">SQL Query</param>
        /// <param name="parameters">Parameters (optional)</param>
        /// <returns>DataTable ที่มีข้อมูล</returns>
        public static DataTable ExecuteDataTable(string query, SqlParameter[] parameters = null)
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (parameters != null)
                            cmd.Parameters.AddRange(parameters);

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
                throw new Exception("Error executing data table: " + ex.Message);
            }
        }

        /// <summary>
        /// สร้าง SqlParameter
        /// </summary>
        /// <param name="parameterName">ชื่อ Parameter (เช่น @Username)</param>
        /// <param name="value">ค่า</param>
        /// <returns>SqlParameter</returns>
        public static SqlParameter CreateParameter(string parameterName, object value)
        {
            return new SqlParameter(parameterName, value ?? DBNull.Value);
        }
    }
}
