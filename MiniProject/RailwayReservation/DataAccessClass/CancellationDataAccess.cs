using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using RailwayReservation.Models;


namespace RailwayReservation.DataAccessClass
{
    public class CancellationDataAccess
    {
        public decimal CancelByUser(int bookingId)
        {
            using (SqlConnection conn = DatabaseConnection.getConnection())
            {
                // fetch booking details
                var cmd = new SqlCommand(@"SELECT r.BookingId, r.TrainClassId, r.TravelDate, r.Amount, r.CustId FROM Reservations r WHERE r.BookingId=@b", conn);
                cmd.Parameters.AddWithValue("@b", bookingId);
                var rd = cmd.ExecuteReader();
                if (!rd.Read()) { rd.Close(); return 0m; }
                DateTime travel = (DateTime)rd[2];
                decimal amount = Convert.ToDecimal(rd[3]);
                int custId = Convert.ToInt32(rd[4]);
                rd.Close();

                double days = (travel - DateTime.Now).TotalDays;
                decimal refund = 0m;
                if (days >= 4) refund = amount * 0.8m;
                else if (days >= 2) refund = amount * 0.7m;
                else if (days >= 1) refund = amount * 0.5m;
                else refund = 0m;

                if (refund > 0)
                {
                    var ins = new SqlCommand("INSERT INTO Refunds (BookingId,CustId,RefundAmount,RefundDate,Reason) VALUES(@b,@c,@r,GETDATE(),'UserCancellation')", conn);
                    ins.Parameters.AddWithValue("@b", bookingId);
                    ins.Parameters.AddWithValue("@c", custId);
                    ins.Parameters.AddWithValue("@r", refund);
                    ins.ExecuteNonQuery();
                }

                var ic = new SqlCommand("INSERT INTO Cancellations (BookingId) VALUES(@b)", conn);
                ic.Parameters.AddWithValue("@b", bookingId);
                ic.ExecuteNonQuery();

                var upd = new SqlCommand("UPDATE Reservations SET CurrentStatus='Cancelled' WHERE BookingId=@b", conn);
                upd.Parameters.AddWithValue("@b", bookingId);
                upd.ExecuteNonQuery();

                return refund;
            }
        }
    }
}
