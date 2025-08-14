using System;
using System.Data;
using System.Data.SqlClient;

namespace RailwayReservation
{
    public static class DatabaseConnection
    {
        private static SqlConnection con;
        private static string connectionString = "Data Source=ICS-LT-4V2CJ84\\SQLEXPRESS;Database=railwaydatabase;" +
                "User ID=sa; Password=Sh@7991138912";
        public static SqlConnection getConnection()
        {
            con = new SqlConnection(connectionString);
            con.Open();
            return con;
        }
    }
}
