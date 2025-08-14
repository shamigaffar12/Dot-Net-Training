using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using RailwayReservation.Models;

namespace RailwayReservation.DataAccessClass
{
    public class ReportDataAccess
    {
        public void ShowReport()
        {
            using (SqlConnection conn = DatabaseConnection.getConnection())
            {
                var cmd = new SqlCommand(@"
SELECT 
(SELECT COUNT(*) FROM Reservations WHERE CurrentStatus='Confirmed') AS TotalBookings,
(SELECT COUNT(*) FROM Reservations WHERE CurrentStatus='Cancelled') AS TotalCancelled,
(SELECT ISNULL(SUM(RefundAmount),0) FROM Refunds) AS TotalRefunds,
(SELECT ISNULL(SUM(Amount),0) FROM Reservations) AS GrossAmount
", conn);
                var rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    Console.WriteLine($"Total Bookings: {rd["TotalBookings"]}"); 
                    Console.WriteLine($"Total Cancelled: {rd["TotalCancelled"]}"); 
                    Console.WriteLine($"Total Refunds: {rd["TotalRefunds"]}"); 
                    Console.WriteLine($"Gross Amount: {rd["GrossAmount"]}"); 
                    Console.WriteLine($"Net Revenue: {Convert.ToDecimal(rd["GrossAmount"]) - Convert.ToDecimal(rd["TotalRefunds"]) }"); 
                }
            }
        }
    }
}
