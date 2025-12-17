using System;

namespace CNXML_HVA
{

    public static class DatabaseConfig
    {

        public static string ConnectionString
        {
            get
            {
                return @"Data Source=localhost;
                         Initial Catalog=dbSANBONG;
                         User ID=sa;
                         Password=1234567;
                         Encrypt=True;
                         TrustServerCertificate=True;";
            }
        }

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
