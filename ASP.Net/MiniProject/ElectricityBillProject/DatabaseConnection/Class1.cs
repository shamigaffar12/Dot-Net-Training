using System.Data.SqlClient;
using System.Configuration;

namespace DatabaseConnection
{
    public class DBHandler
    {
        public static SqlConnection GetConnection()
        {

            string cs = ConfigurationManager.ConnectionStrings["EBConn"].ConnectionString;
            return new SqlConnection(cs);
        }
    }
}