using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;

namespace RailwayReservation
{

    public static class TicketGenerator
    {
       public static void PrintTicket()
        {
            Console.Write("BookingId: ");
            if (!int.TryParse(Console.ReadLine(), out int bid) || bid <= 0)
            {
                Console.WriteLine("Invalid Booking ID. Press any key to return to menu.");
                Console.ReadKey();
                return;
            }

            using (var conn = DatabaseConnection.getConnection())
            using (var cmd = new SqlCommand(@"
        SELECT 
            r.BookingId, c.CustName, c.CustPhone, c.CustEmail,
            t.TrainName, t.Source, t.Destination,
            tc.ClassType,
            r.TravelDate, r.BookingDate,
            r.PassengerCount,
            tc.Price
        FROM Reservations r
        JOIN Customers c ON r.CustId = c.CustId
        JOIN TrainClasses tc ON r.TrainClassId = tc.TrainClassId
        JOIN TrainMaster t ON tc.TrainNumber = t.TrainNumber
        WHERE r.BookingId = @b", conn))
            {
                cmd.Parameters.AddWithValue("@b", bid);

                using (var rd = cmd.ExecuteReader())
                {
                    if (!rd.Read())
                    {
                        Console.WriteLine("Booking not found. Press any key to return to menu.");
                        Console.ReadKey();
                        return;
                    }

                    int bookingId = (int)rd["BookingId"];
                    string custName = rd["CustName"].ToString();
                    string trainName = rd["TrainName"].ToString();
                    string classType = rd["ClassType"].ToString();
                    DateTime travelDate = (DateTime)rd["TravelDate"];
                    DateTime bookingDate = (DateTime)rd["BookingDate"];
                    int passengerCount = (int)rd["PassengerCount"];
                    decimal pricePerSeat = (decimal)rd["Price"];
                    string source = rd["Source"].ToString();
                    string destination = rd["Destination"].ToString();

                    // Generate seat/berth numbers dynamically
                    string seatNumbers = "";
                    string berthNumbers = "";
                    for (int i = 1; i <= passengerCount; i++)
                    {
                        seatNumbers += $"{i} ";
                        berthNumbers += $"B{i} ";
                    }

                    TicketGenerator.SavePdfTicket(
                        bookingId, custName, trainName, classType,
                        travelDate, passengerCount, pricePerSeat,
                        pricePerSeat * passengerCount, source, destination,
                        bookingDate, seatNumbers, berthNumbers
                    );
                }
            }

        }

        public static void SavePdfTicket(
            int bookingId, string customerName, string trainName, string classType,
            DateTime travelDate, int passengerCount, decimal pricePerSeat,
            decimal totalFare, string source, string destination, DateTime bookingDate,
            string seatNumbers, string berthNumbers)
        {
            string folder = @"C:\Infinite Trainining 2025\MiniProject\Tickets\";
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            string file = $"{folder}Ticket_{bookingId}.pdf";
            using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
            {
                Document doc = new Document(PageSize.A5.Rotate(), 36, 36, 36, 36);
                PdfWriter.GetInstance(doc, fs);
                doc.Open();

                // Title with emphasis
                doc.Add(new Paragraph("✈ RAILWAY TICKET ✈", new Font(Font.FontFamily.HELVETICA, 20, Font.BOLD, BaseColor.BLUE))
                { Alignment = Element.ALIGN_CENTER });

                doc.Add(new Paragraph(new Chunk(new DottedLineSeparator())));

                // Build a details table
                PdfPTable table = new PdfPTable(2) { WidthPercentage = 100 };
                float[] widths = { 1f, 1f };
                table.SetWidths(widths);

                AddCell(table, "Booking ID:", bookingId.ToString(), true);
                AddCell(table, "Customer:", customerName);
                AddCell(table, "Train:", trainName, true);
                AddCell(table, "From → To:", $"{source} → {destination}");
                AddCell(table, "Class:", classType, true);
                AddCell(table, "Travel Date:", travelDate.ToString("dd-MM-yyyy"));
                AddCell(table, "Booking Date:", bookingDate.ToString("dd-MM-yyy HH : mm"), true);
                AddCell(table, "Passengers:", passengerCount.ToString());
                AddCell(table, "Fare/Seat:", $"₹{pricePerSeat:F2}", true);
                AddCell(table, "Total Fare:", $"₹{totalFare:F2}");
                AddCell(table, "Seats:", seatNumbers.Trim(), true);
                AddCell(table, "Berths:", berthNumbers.Trim());

                doc.Add(table);

                doc.Add(new Paragraph(new Chunk(new DottedLineSeparator())));

                doc.Add(new Paragraph("Thank you for booking with us!\nWish you a safe and pleasant journey.",
                                      new Font(Font.FontFamily.HELVETICA, 12, Font.ITALIC, BaseColor.DARK_GRAY))
                { Alignment = Element.ALIGN_CENTER });

                doc.Close();
            }

            Console.WriteLine($" Ticket generated: {file}");
        }

        private static void AddCell(PdfPTable table, string label, string value, bool boldLabel = false)
        {
            Font labelFont = new Font(Font.FontFamily.HELVETICA, 10, boldLabel ? Font.BOLD : Font.NORMAL);
            Font valueFont = new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL);
            PdfPCell cell = new PdfPCell(new Phrase($"{label} {value}", labelFont))
            {
                Border = Rectangle.NO_BORDER,
                PaddingBottom = 4
            };
            table.AddCell(cell);
        }

    }
}
