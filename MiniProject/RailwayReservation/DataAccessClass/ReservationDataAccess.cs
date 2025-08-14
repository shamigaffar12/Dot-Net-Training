using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using RailwayReservation.Models;

namespace RailwayReservation.DataAccessClass
{
    public class ReservationDataAccess
    {
        public int Book(int custId, int trainClassId, DateTime travelDate)
        {
            using (SqlConnection conn = DatabaseConnection.getConnection())
            {
                // check availability
                var chk = new SqlCommand("SELECT AvailableSeats, Price FROM TrainClasses WHERE TrainClassId=@id", conn);
                chk.Parameters.AddWithValue("@id", trainClassId);
                var rd = chk.ExecuteReader();
                if (!rd.Read()) { rd.Close(); return 0; }
                int av = Convert.ToInt32(rd["AvailableSeats"]);
                decimal price = Convert.ToDecimal(rd["Price"]);
                rd.Close();
                if (av <= 0) return 0;

                var ins = new SqlCommand("INSERT INTO Reservations (CustId,TrainClassId,TravelDate,CurrentStatus,BookingDate,Amount) VALUES(@c,@tc,@d,'Confirmed',GETDATE(),@amt); SELECT CAST(SCOPE_IDENTITY() AS INT);", conn);
                ins.Parameters.AddWithValue("@c", custId);
                ins.Parameters.AddWithValue("@tc", trainClassId);
                ins.Parameters.AddWithValue("@d", travelDate);
                ins.Parameters.AddWithValue("@amt", price);
                var obj = ins.ExecuteScalar();
                return obj != null ? (int)obj : 0;
            }
        }
    }
}
