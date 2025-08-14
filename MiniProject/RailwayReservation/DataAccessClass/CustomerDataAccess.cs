using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using RailwayReservation.Models;

namespace RailwayReservation.DataAccessClass
{
    public class CustomerDataAccess
    {
        public bool Login(string email, string pass)
        {
            using (SqlConnection conn = DatabaseConnection.getConnection())
            {
                var cmd = new SqlCommand("SELECT COUNT(*) FROM Customers WHERE CustEmail=@e AND CustPassword=@p", conn);
                cmd.Parameters.AddWithValue("@e", email);
                cmd.Parameters.AddWithValue("@p", pass);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public int Register(Customer c)
        {
            using (SqlConnection conn = DatabaseConnection.getConnection())
            {
                var cmd = new SqlCommand("INSERT INTO Customers (CustName,CustPhone,CustEmail,CustPassword) VALUES(@n,@ph,@e,@p); SELECT CAST(SCOPE_IDENTITY() AS INT);", conn);
                cmd.Parameters.AddWithValue("@n", c.CustName);
                cmd.Parameters.AddWithValue("@ph", c.CustPhone);
                cmd.Parameters.AddWithValue("@e", c.CustEmail);
                cmd.Parameters.AddWithValue("@p", c.CustPassword);
                var obj = cmd.ExecuteScalar();
                return obj != null ? (int)obj : 0;
            }
        }

        public void BookTicket()
        {
            using (SqlConnection conn = DatabaseConnection.getConnection())
            {
                try
                {
                    Console.Write("Enter Customer ID: ");
                    int custId = int.Parse(Console.ReadLine());

                    Console.Write("Enter Train Number: ");
                    int trainNumber = int.Parse(Console.ReadLine());
               

                    Console.Write("Enter Class Type (SLEEPER, AC3, AC2, AC1 )");
                string classType = Console.ReadLine().Trim().ToUpper();

                // Validate Class Type
                if (!new[] { "SLEEPER", "AC3", "AC2", "AC1" }.Contains(classType))
                {
                    Console.WriteLine("Invalid class type entered.");
                    return;
                }

                Console.Write("Enter Date of Travel (yyyy-mm-dd): ");
                DateTime travelDate = DateTime.Parse(Console.ReadLine());

                if (travelDate.Date < DateTime.Today)
                {
                    Console.WriteLine("Cannot book tickets for past dates.");
                    return;
                }

                Console.Write("Enter Number of Passengers: ");
                int passengerCount = int.Parse(Console.ReadLine());

                // Get TrainClassId, AvailableSeats, Price based on TrainNumber and ClassType
                SqlCommand getClassCmd = new SqlCommand(
          "SELECT TrainClassId, AvailableSeats, Price FROM TrainClasses WHERE TrainNumber = @tn AND UPPER(ClassType) = @ct", conn);
                getClassCmd.Parameters.AddWithValue("@tn", trainNumber);
                getClassCmd.Parameters.AddWithValue("@ct", classType);
                SqlDataReader seatReader = getClassCmd.ExecuteReader();
                if (!seatReader.Read())
                {
                    Console.WriteLine("Invalid Train Number or Class Type.");
                    return;
                }

                int trainClassId = Convert.ToInt32(seatReader["TrainClassId"]);
                int availableSeats = Convert.ToInt32(seatReader["AvailableSeats"]);
                decimal pricePerSeat = Convert.ToDecimal(seatReader["Price"]);
                seatReader.Close();

                if (availableSeats < passengerCount)
                {
                    Console.WriteLine($"Only {availableSeats} seats available. Booking cancelled.");
                    return;
                }

                decimal totalFare = passengerCount * pricePerSeat;
                DateTime bookingDate = DateTime.Now;

                // Insert booking
                SqlCommand bookingCmd = new SqlCommand(@"
            INSERT INTO Reservations (CustId, TrainClassId, TravelDate, CurrentStatus, BookingDate, PassengerCount) 
            OUTPUT INSERTED.BookingId 
            VALUES (@cust, @trainClass, @travel, 'Confirmed', @bookDate, @count)", conn);
                bookingCmd.Parameters.AddWithValue("@cust", custId);
                bookingCmd.Parameters.AddWithValue("@trainClass", trainClassId);
                bookingCmd.Parameters.AddWithValue("@travel", travelDate);
                bookingCmd.Parameters.AddWithValue("@bookDate", bookingDate);
                bookingCmd.Parameters.AddWithValue("@count", passengerCount);
                int bookingId = Convert.ToInt32(bookingCmd.ExecuteScalar());

                // Update available seats
                SqlCommand seatUpdateCmd = new SqlCommand("UPDATE TrainClasses SET AvailableSeats = AvailableSeats - @count " +
"WHERE TrainClassId = @id AND AvailableSeats >= @count", conn);

                seatUpdateCmd.Parameters.AddWithValue("@count", passengerCount);
                seatUpdateCmd.Parameters.AddWithValue("@id", trainClassId);
                seatUpdateCmd.ExecuteNonQuery();

                // Generate seat and berth numbers
                string seatNumbers = "";
                string berthNumbers = "";
                for (int i = 1; i <= passengerCount; i++)
                {
                    seatNumbers += (availableSeats - passengerCount + i) + " ";
                    berthNumbers += "B" + (availableSeats - passengerCount + i) + " ";
                }

                // Fetch ticket details
                SqlCommand ticketCmd = new SqlCommand(@"
            SELECT c.CustName, c.CustPhone, c.CustEmail, 
                   t.TrainName, t.Source, t.Destination, tc.ClassType
            FROM Customers c
            JOIN Reservations r ON c.CustId = r.CustId
            JOIN TrainClasses tc ON r.TrainClassId = tc.TrainClassId
            JOIN TrainMaster t ON tc.TrainNumber = t.TrainNumber
            WHERE r.BookingId = @bid", conn);
                ticketCmd.Parameters.AddWithValue("@bid", bookingId);
                SqlDataReader tr = ticketCmd.ExecuteReader();
                tr.Read();

                Console.WriteLine("\n--- Ticket Details ---");
                Console.WriteLine($"Booking ID: {bookingId}");
                Console.WriteLine($"Passenger Name: {tr["CustName"]}");
                Console.WriteLine($"Phone: {tr["CustPhone"]}");
                Console.WriteLine($"Email: {tr["CustEmail"]}");
                Console.WriteLine($"Train: {tr["TrainName"]}");
                Console.WriteLine($"Source: {tr["Source"]}");
                Console.WriteLine($"Destination: {tr["Destination"]}");
                Console.WriteLine($"Class: {tr["ClassType"]}");
                Console.WriteLine($"No of Tickets: {passengerCount}");
                Console.WriteLine($"Fare per Ticket: {pricePerSeat}");
                Console.WriteLine($"Total Fare: {totalFare}");
                Console.WriteLine($"Seats: {seatNumbers}");
                Console.WriteLine($"Berths: {berthNumbers}");
                Console.WriteLine($"Date of Travel: {travelDate}");
                Console.WriteLine($"Booking Date & Time: {bookingDate}");
                Console.WriteLine("---------------------------");
                tr.Close();
            } 
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public void CancelTicket()
        {
            using (SqlConnection conn = DatabaseConnection.getConnection())
            {
                Console.Write("Enter Booking ID to cancel: ");
                int bookingId = int.Parse(Console.ReadLine());

                // Get booking details
                SqlCommand getInfoCmd = new SqlCommand(@"
                    SELECT r.TravelDate, r.TrainClassId, r.PassengerCount, tc.Price
                    FROM Reservations r
                    JOIN TrainClasses tc ON r.TrainClassId = tc.TrainClassId
                    WHERE r.BookingId = @bid", conn);
                getInfoCmd.Parameters.AddWithValue("@bid", bookingId);
                SqlDataReader rd = getInfoCmd.ExecuteReader();
                if (!rd.Read())
                {
                    Console.WriteLine("Invalid Booking ID.");
                    return;
                }

                DateTime travelDate = Convert.ToDateTime(rd["TravelDate"]);
                int trainClassId = Convert.ToInt32(rd["TrainClassId"]);
                int passengerCount = Convert.ToInt32(rd["PassengerCount"]);
                decimal price = Convert.ToDecimal(rd["Price"]);
                rd.Close();

                // Refund calculation
                double daysBefore = (travelDate - DateTime.Now).TotalDays;
                decimal refundAmount = 0;
                decimal refundPercent = 0;
                if (daysBefore >= 4) { refundAmount = price * passengerCount * 0.80m; refundPercent = 80; }
                else if (daysBefore >= 2) { refundAmount = price * passengerCount * 0.70m; refundPercent = 70; }
                else if (daysBefore >= 1) { refundAmount = price * passengerCount * 0.50m; refundPercent = 50; }

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

                // Increment seats (internal)
                SqlCommand seatUpdateCmd = new SqlCommand(
                    "UPDATE TrainClasses SET AvailableSeats = AvailableSeats + @count WHERE TrainClassId = @id", conn);
                seatUpdateCmd.Parameters.AddWithValue("@count", passengerCount);
                seatUpdateCmd.Parameters.AddWithValue("@id", trainClassId);
                seatUpdateCmd.ExecuteNonQuery();

                // Show refund details only (no available seats to customer)
                Console.WriteLine("\n--- Refund Details ---");
                Console.WriteLine($"Booking ID: {bookingId}");
                Console.WriteLine($"Days before travel: {Math.Floor(daysBefore)}");
                Console.WriteLine($"Refund Percentage: {refundPercent}%");
                Console.WriteLine($"Refund Amount: {refundAmount}");
            }
        }
        public void ViewTrainsAndSeats()
        {
            using (SqlConnection conn = DatabaseConnection.getConnection())
            {
                string query = @"
            SELECT t.TrainNumber, t.TrainName, t.Source, t.Destination, 
                   tc.ClassType, tc.AvailableSeats, tc.MaxSeats, tc.Price
            FROM TrainMaster t
            JOIN TrainClasses tc ON t.TrainNumber = tc.TrainNumber
            WHERE t.IsActive = 1
            ORDER BY t.TrainNumber, tc.ClassType";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader rd = cmd.ExecuteReader();

                Console.WriteLine("\n--- Available Trains & Seats ---");
                while (rd.Read())
                {
                    Console.WriteLine(
                        $"Train No: {rd["TrainNumber"]}, Name: {rd["TrainName"]}, " +
                        $"From: {rd["Source"]}, To: {rd["Destination"]}, Class: {rd["ClassType"]}, " +
                        $"Seats: {rd["AvailableSeats"]}/{rd["MaxSeats"]}, Price: {rd["Price"]}"
                    );
                }
                rd.Close();
            }
        }

        public void ViewBookingStatus()
        {
            using (SqlConnection conn = DatabaseConnection.getConnection())
            {
                Console.Write("Enter Booking ID: ");
                int bookingId = int.Parse(Console.ReadLine());

                string query = @"
            SELECT r.BookingId, r.CurrentStatus, r.TravelDate, r.BookingDate, r.PassengerCount,
                   c.CustName, t.TrainName, t.Source, t.Destination, tc.ClassType
            FROM Reservations r
            JOIN Customers c ON r.CustId = c.CustId
            JOIN TrainClasses tc ON r.TrainClassId = tc.TrainClassId
            JOIN TrainMaster t ON tc.TrainNumber = t.TrainNumber
            WHERE r.BookingId = @bid";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@bid", bookingId);

                SqlDataReader rd = cmd.ExecuteReader();
                if (!rd.Read())
                {
                    Console.WriteLine("Booking not found.");
                    return;
                }

                Console.WriteLine("\n--- Booking Status ---");
                Console.WriteLine($"Booking ID: {rd["BookingId"]}");
                Console.WriteLine($"Customer: {rd["CustName"]}");
                Console.WriteLine($"Train: {rd["TrainName"]}");
                Console.WriteLine($"From: {rd["Source"]}, To: {rd["Destination"]}");
                Console.WriteLine($"Class: {rd["ClassType"]}");
                Console.WriteLine($"No. of Passengers: {rd["PassengerCount"]}");
                Console.WriteLine($"Travel Date: {rd["TravelDate"]}");
                Console.WriteLine($"Booking Date: {rd["BookingDate"]}");
                Console.WriteLine($"Current Status: {rd["CurrentStatus"]}");
                rd.Close();
            }
        }
    }
}