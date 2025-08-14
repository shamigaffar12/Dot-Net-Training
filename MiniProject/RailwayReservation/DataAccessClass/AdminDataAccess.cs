using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using RailwayReservation.Models;


namespace RailwayReservation.DataAccessClass
{
    public class AdminDataAccess
    {
        public TrainDataAccess trainAccess;
        public bool Login(string user, string pass)
        {
            using (SqlConnection conn = DatabaseConnection.getConnection())
            {
                var cmd = new SqlCommand("SELECT COUNT(*) FROM Admins WHERE AdminUsername=@u AND AdminPassword=@p", conn);
                cmd.Parameters.AddWithValue("@u", user);
                cmd.Parameters.AddWithValue("@p", pass);
                int c = (int)cmd.ExecuteScalar();
                return c > 0;
            }
        }



        public AdminDataAccess()
        {
            trainAccess = new TrainDataAccess(); // Reuse train functions if needed
        }

        public void ShowAllBookings()
        {
            using (SqlConnection conn = DatabaseConnection.getConnection())
            {
                string query = @"
                    SELECT r.BookingId, c.CustName, t.TrainName, t.Source, t.Destination, tc.ClassType,
                           r.PassengerCount, r.TravelDate, r.BookingDate, r.CurrentStatus
                    FROM Reservations r
                    JOIN Customers c ON r.CustId = c.CustId
                    JOIN TrainClasses tc ON r.TrainClassId = tc.TrainClassId
                    JOIN TrainMaster t ON tc.TrainNumber = t.TrainNumber
                    ORDER BY r.BookingId DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader rd = cmd.ExecuteReader();

                Console.WriteLine("\n--- All Bookings ---");
                while (rd.Read())
                {
                    Console.WriteLine(
                        $"Booking ID: {rd["BookingId"]}, Customer: {rd["CustName"]}, Train: {rd["TrainName"]}, " +
                        $"From: {rd["Source"]}, To: {rd["Destination"]}, Class: {rd["ClassType"]}, " +
                        $"Passengers: {rd["PassengerCount"]}, Travel: {rd["TravelDate"]}, Status: {rd["CurrentStatus"]}"
                    );
                }
                rd.Close();
            }
        }

        public void ShowAllCancellations()
        {
            using (SqlConnection conn = DatabaseConnection.getConnection())
            {
                string query = @"
                    SELECT can.CancelId, can.BookingId, c.CustName, t.TrainName, tc.ClassType, 
                           can.CancelDate, r.PassengerCount
                    FROM Cancellations can
                    JOIN Reservations r ON can.BookingId = r.BookingId
                    JOIN Customers c ON r.CustId = c.CustId
                    JOIN TrainClasses tc ON r.TrainClassId = tc.TrainClassId
                    JOIN TrainMaster t ON tc.TrainNumber = t.TrainNumber
                    ORDER BY can.CancelId DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader rd = cmd.ExecuteReader();

                Console.WriteLine("\n--- All Cancellations ---");
                while (rd.Read())
                {
                    Console.WriteLine(
                        $"Cancel ID: {rd["CancelId"]}, Booking ID: {rd["BookingId"]}, " +
                        $"Customer: {rd["CustName"]}, Train: {rd["TrainName"]}, " +
                        $"Class: {rd["ClassType"]}, Cancelled On: {rd["CancelDate"]}, Passengers: {rd["PassengerCount"]}"
                    );
                }
                rd.Close();
            }
        }
            public void ShowAllCustomersWithBookings()
        {
            using (SqlConnection conn = DatabaseConnection.getConnection())
            {
                string query = @"
                SELECT c.CustId, c.CustName, c.CustPhone, c.CustEmail,
                       r.BookingId, t.TrainName, tc.ClassType, r.TravelDate, r.CurrentStatus
                FROM Customers c
                LEFT JOIN Reservations r ON c.CustId = r.CustId
                LEFT JOIN TrainClasses tc ON r.TrainClassId = tc.TrainClassId
                LEFT JOIN TrainMaster t ON tc.TrainNumber = t.TrainNumber
                ORDER BY c.CustId, r.BookingId";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader rd = cmd.ExecuteReader();

                Console.WriteLine("\n--- Customer Details with Bookings ---");
                int lastCustId = -1;
                while (rd.Read())
                {
                    int custId = Convert.ToInt32(rd["CustId"]);
                    if (custId != lastCustId)
                    {
                        Console.WriteLine($"\nCustomer ID: {custId}, Name: {rd["CustName"]}, Phone: {rd["CustPhone"]}, Email: {rd["CustEmail"]}");
                        lastCustId = custId;
                    }
                    if (rd["BookingId"] != DBNull.Value)
                    {
                        Console.WriteLine($"  Booking ID: {rd["BookingId"]}, Train: {rd["TrainName"]}, Class: {rd["ClassType"]}, Date: {rd["TravelDate"]}, Status: {rd["CurrentStatus"]}");
                    }
                }
                rd.Close();
            }
        }

        public void DeleteBookingAndRefund()
        {
            using (SqlConnection conn = DatabaseConnection.getConnection())
            {
                Console.Write("Enter Booking ID to delete: ");
                int bookingId = int.Parse(Console.ReadLine());

                // Get booking info
                SqlCommand getBookingCmd = new SqlCommand(@"
                    SELECT TrainClassId, PassengerCount, tc.Price
                    FROM Reservations r
                    JOIN TrainClasses tc ON r.TrainClassId = tc.TrainClassId
                    WHERE r.BookingId = @bid", conn);
                getBookingCmd.Parameters.AddWithValue("@bid", bookingId);
                SqlDataReader rd = getBookingCmd.ExecuteReader();

                if (!rd.Read())
                {
                    Console.WriteLine("Booking not found.");
                    return;
                }

                int trainClassId = Convert.ToInt32(rd["TrainClassId"]);
                int passengerCount = Convert.ToInt32(rd["PassengerCount"]);
                decimal price = Convert.ToDecimal(rd["Price"]);
                rd.Close();

                decimal refundAmount = passengerCount * price; // 100% refund

                // Insert cancellation
                SqlCommand cancelCmd = new SqlCommand(
                    "INSERT INTO Cancellations (BookingId, CancelDate) VALUES (@bid, GETDATE())", conn);
                cancelCmd.Parameters.AddWithValue("@bid", bookingId);
                cancelCmd.ExecuteNonQuery();

                // Insert refund
                SqlCommand refundCmd = new SqlCommand(
                    "INSERT INTO Refunds (BookingId, RefundAmount, RefundDate) VALUES (@bid, @amt, GETDATE())", conn);
                refundCmd.Parameters.AddWithValue("@bid", bookingId);
                refundCmd.Parameters.AddWithValue("@amt", refundAmount);
                refundCmd.ExecuteNonQuery();

                // Update booking status
                SqlCommand statusCmd = new SqlCommand(
                    "UPDATE Reservations SET CurrentStatus='Cancelled' WHERE BookingId=@bid", conn);
                statusCmd.Parameters.AddWithValue("@bid", bookingId);
                statusCmd.ExecuteNonQuery();

                // Increment available seats
                SqlCommand seatUpdateCmd = new SqlCommand(
                    "UPDATE TrainClasses SET AvailableSeats = AvailableSeats + @count WHERE TrainClassId = @id", conn);
                seatUpdateCmd.Parameters.AddWithValue("@count", passengerCount);
                seatUpdateCmd.Parameters.AddWithValue("@id", trainClassId);
                seatUpdateCmd.ExecuteNonQuery();

                Console.WriteLine($"Booking ID {bookingId} deleted and 100% refund ({refundAmount}) issued.");
            }
        }

        public void ShowReport()
        {
            using (SqlConnection conn = DatabaseConnection.getConnection())
            {
                Console.WriteLine("\n===== Admin Finance Report =====");

                // Total Active Bookings
                var totalBookingsCmd = new SqlCommand(
                    "SELECT COUNT(*) FROM Reservations WHERE CurrentStatus = 'Confirmed'", conn);
                int totalBookings = Convert.ToInt32(totalBookingsCmd.ExecuteScalar());
                Console.WriteLine($"Total Active Bookings: {totalBookings}");

                // Total Cancellations
                var totalCancelsCmd = new SqlCommand(
                    "SELECT COUNT(*) FROM Reservations WHERE CurrentStatus = 'Cancelled'", conn);
                int totalCancellations = Convert.ToInt32(totalCancelsCmd.ExecuteScalar());
                Console.WriteLine($"Total Cancellations: {totalCancellations}");

                // Total Passengers
                var totalPassengersCmd = new SqlCommand(
                    "SELECT ISNULL(SUM(PassengerCount), 0) FROM Reservations WHERE CurrentStatus = 'Confirmed'", conn);
                int totalPassengers = Convert.ToInt32(totalPassengersCmd.ExecuteScalar());
                Console.WriteLine($"Total Passengers Booked: {totalPassengers}");

                // Total Revenue
                var totalRevenueCmd = new SqlCommand(@"
            SELECT ISNULL(SUM(tc.Price * r.PassengerCount), 0)
            FROM Reservations r
            JOIN TrainClasses tc ON r.TrainClassId = tc.TrainClassId
            WHERE r.CurrentStatus = 'Confirmed'", conn);
                decimal totalRevenue = Convert.ToDecimal(totalRevenueCmd.ExecuteScalar());
                Console.WriteLine($"Total Revenue: Rs{totalRevenue:N2}");

                // Total Refunds
                var totalRefundsCmd = new SqlCommand("SELECT ISNULL(SUM(RefundAmount), 0) FROM Refunds", conn);
                decimal totalRefunds = Convert.ToDecimal(totalRefundsCmd.ExecuteScalar());
                Console.WriteLine($"Total Refunds Given: Rs{totalRefunds:N2}");

                // Net Profit
                decimal netProfit = totalRevenue - totalRefunds;
                Console.WriteLine($"Net Profit: Rs{netProfit:N2}");

                // Revenue Breakdown by Class
                Console.WriteLine("\nRevenue by Class Type:");
                var classRevenueCmd = new SqlCommand(@"
            SELECT tc.ClassType, ISNULL(SUM(tc.Price * r.PassengerCount), 0) AS Revenue
            FROM Reservations r
            JOIN TrainClasses tc ON r.TrainClassId = tc.TrainClassId
            WHERE r.CurrentStatus = 'Confirmed'
            GROUP BY tc.ClassType", conn);

                using (var reader = classRevenueCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string classType = reader["ClassType"].ToString();
                        decimal classRevenue = Convert.ToDecimal(reader["Revenue"]);
                        Console.WriteLine($" - {classType}: Rs{classRevenue:N2}");
                    }
                }

                // Refunds by Reason
                Console.WriteLine("\nRefund  by Reason:");
                var refundReasonCmd = new SqlCommand(@"
            SELECT Reason, ISNULL(SUM(RefundAmount), 0) AS TotalRefund
            FROM Refunds
            GROUP BY Reason", conn);

                using (var reader = refundReasonCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string reason = reader["Reason"].ToString();
                        decimal amount = Convert.ToDecimal(reader["TotalRefund"]);
                        Console.WriteLine($" - {reason}: Rs{amount:N2}");
                    }
                }

                // Revenue by Train
                Console.WriteLine("\nRevenue by Train:");
                var trainRevenueCmd = new SqlCommand(@"
            SELECT t.TrainName, SUM(tc.Price * r.PassengerCount) AS Revenue
            FROM Reservations r
            JOIN TrainClasses tc ON r.TrainClassId = tc.TrainClassId
            JOIN TrainMaster t ON tc.TrainNumber = t.TrainNumber
            WHERE r.CurrentStatus = 'Confirmed'
            GROUP BY t.TrainName", conn);

                using (var reader = trainRevenueCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string train = reader["TrainName"].ToString();
                        decimal amount = Convert.ToDecimal(reader["Revenue"]);
                        Console.WriteLine($" - {train}: Rs {amount:N2}");
                    }
                }

               
            }
        }



        public void AddTrain()
        {
            trainAccess.AddTrain();
        }

        public void ShowAllTrains()
        {
            using (SqlConnection conn = DatabaseConnection.getConnection())
            {
                string query = @"
                SELECT t.TrainNumber, t.TrainName, t.Source, t.Destination, tc.ClassType, tc.AvailableSeats, tc.Price
                FROM TrainMaster t
                JOIN TrainClasses tc ON t.TrainNumber = tc.TrainNumber
                WHERE t.IsActive = 1
                ORDER BY t.TrainNumber";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader rd = cmd.ExecuteReader();
                Console.WriteLine("\n--- Active Trains ---");
                while (rd.Read())
                {
                    Console.WriteLine($"Train No: {rd["TrainNumber"]}, Name: {rd["TrainName"]}, From: {rd["Source"]}, To: {rd["Destination"]}, Class: {rd["ClassType"]}, Seats: {rd["AvailableSeats"]}, Price: {rd["Price"]}");
                }
                rd.Close();
            }
        }
        public void ShowInactiveTrains()
        {
            using (SqlConnection conn = DatabaseConnection.getConnection())
            {
                string query = @"
                    SELECT TrainNumber, TrainName, Source, Destination
                    FROM TrainMaster
                    WHERE IsActive = 0";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader rd = cmd.ExecuteReader();

                Console.WriteLine("\n--- Inactive Trains ---");
                while (rd.Read())
                {
                    Console.WriteLine($"Train No: {rd["TrainNumber"]}, Name: {rd["TrainName"]}, " +
                                      $"From: {rd["Source"]}, To: {rd["Destination"]}");
                }
                rd.Close();
            }
        }

        public void RemoveTrain()
        {
            using (SqlConnection conn = DatabaseConnection.getConnection())
            {
                Console.Write("Enter Train Number to mark inactive: ");
                int trainNo = int.Parse(Console.ReadLine());

                // Step 1: Check for existing future bookings
                SqlCommand checkBookings = new SqlCommand(@"
            SELECT COUNT(*) FROM Reservations r
            JOIN TrainClasses tc ON r.TrainClassId = tc.TrainClassId
            WHERE tc.TrainNumber = @tno AND r.TravelDate >= CAST(GETDATE() AS DATE)
              AND r.CurrentStatus = 'Confirmed'", conn);

                checkBookings.Parameters.AddWithValue("@tno", trainNo);
                int existingBookings = (int)checkBookings.ExecuteScalar();

                if (existingBookings > 0)
                {
                    Console.WriteLine(" Cannot delete. The train has existing future bookings.");
                   
                    return;
                }

              
                SqlCommand cmd = new SqlCommand(
                    "UPDATE TrainMaster SET IsActive = 0 WHERE TrainNumber = @tno", conn);
                cmd.Parameters.AddWithValue("@tno", trainNo);
                int rows = cmd.ExecuteNonQuery();

                Console.WriteLine(rows > 0 ? " Train marked as inactive." : " Train not found.");
            }
        }



    }
}
