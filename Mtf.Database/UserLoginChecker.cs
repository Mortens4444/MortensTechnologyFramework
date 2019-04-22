using System.Data.SqlClient;

namespace Mtf.Database
{
    public class UserLoginChecker
    {
        public bool CanUserLogin(string connectionString, bool throwException)
        {
            var result = false;
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    connection.Close();
                    result = true;
                }
            }
            catch
            {
                if (throwException)
                {
                    throw;
                }
            }
            return result;
        }
    }
}