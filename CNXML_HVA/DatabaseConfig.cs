using System;

namespace CNXML_HVA
{
    /// <summary>
    /// Class quản lý cấu hình kết nối database tập trung
    /// </summary>
    public static class DatabaseConfig
    {
        /// <summary>
        /// Connection String cho SQL Server
        /// Chỉ cần thay đổi ở đây, tất cả các form sẽ tự động cập nhật
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                return @"Data Source=localhost;Initial Catalog=dbSANBONG;Integrated Security=True;TrustServerCertificate=True";
            }
        }

        /// <summary>
        /// Kiểm tra kết nối database
        /// </summary>
        public static bool TestConnection(out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                using (var conn = new System.Data.SqlClient.SqlConnection(ConnectionString))
                {
                    conn.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
    }
}
