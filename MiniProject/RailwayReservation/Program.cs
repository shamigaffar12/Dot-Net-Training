using System;
using System.Data;
using System.Data.SqlClient;
using RailwayReservation.DataAccessClass;
using RailwayReservation.Models;

namespace RailwayReservation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*********Railway Reservation System**********") ;
            while (true)
            {
                Console.WriteLine(" 1-Admin ");
                Console.WriteLine(" 2-Customer Login ");
                Console.WriteLine(" 3-Register ");
                Console.WriteLine(" 4-Exit");

                Console.Write("Select the option : "); 
                var ch = Console.ReadLine();
                if (ch=="1") AdminFlow();
                else if (ch=="2") CustomerFlow();
                else if (ch=="3") RegisterCustomer();
                else break;
            }
        }

        static void AdminFlow()
        {
            Console.Write("Admin username: ");
            var u = Console.ReadLine();
            Console.Write("Password: "); 
            var p = Console.ReadLine();
            var ada = new AdminDataAccess();
            if (!ada.Login(u,p))
            { 
                Console.WriteLine("Invalid admin."); 
                return; 
            }
            Console.WriteLine("Admin logged in.");
            AdminDataAccess adminAccess = new AdminDataAccess();

            while (true)
            {
                Console.WriteLine("\n--- Admin Menu ---");
                Console.WriteLine("1. View All Bookings");
                Console.WriteLine("2. View All Cancellations");
                Console.WriteLine("3. Add Train");
                Console.WriteLine("4. Remove Train");
                Console.WriteLine("5. View All Trains");
                Console.WriteLine("6. Show InActive Trains");
                Console.WriteLine("7. View All Customers & Their Bookings");
                Console.WriteLine("8. View Reports");
                Console.WriteLine("9. Delete Booking (Full Refund)");
                Console.WriteLine("0. Logout");
                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        adminAccess.ShowAllBookings();
                        break;
                    case "2":
                        adminAccess.ShowAllCancellations();
                        break;
                    case "3":
                        adminAccess.AddTrain();
                        break;
                    case "4":
                        adminAccess.RemoveTrain();
                        break;
                    case "5":
                        adminAccess.ShowAllTrains();
                        break;
                    case "6":
                        adminAccess.ShowInactiveTrains();
                        break;
                    case "7":
                        adminAccess.ShowAllCustomersWithBookings();
                        break;
                    case "8":
                        adminAccess.ShowReport();
                        break;
                    case "9":
                        
                        adminAccess.DeleteBookingAndRefund();
                        break;
                    case "0":
                        Console.WriteLine("Logging out of admin menu...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void RegisterCustomer()
        {
            var c = new Customer();
            Console.Write("Name: "); c.CustName = Console.ReadLine();
            Console.Write("Phone: "); c.CustPhone = Console.ReadLine();
            Console.Write("Email: "); c.CustEmail = Console.ReadLine();
            Console.Write("Password: "); c.CustPassword = Console.ReadLine();
            var cda = new CustomerDataAccess();
            int id = cda.Register(c);
            Console.WriteLine($"Registered CustId: {id}"); 
        }

        static void CustomerFlow()
        {
            Console.Write("Email: "); 
            string e = Console.ReadLine();
            Console.Write("Password: ");
            string p = Console.ReadLine();
            var cda = new CustomerDataAccess();
            if (!cda.Login(e,p)) 
            {
                Console.WriteLine("Invalid customer."); 
                return; 
            }
            Console.WriteLine("Customer logged in.");
            var tda = new TrainDataAccess();
            var rda = new ReservationDataAccess();
            var cnda = new CancellationDataAccess();
            CustomerDataAccess customer = new CustomerDataAccess();
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n===== Customer Menu =====");
                Console.WriteLine("1. View Trains & Available Seats");
                Console.WriteLine("2. Book Ticket");
                Console.WriteLine("3. Cancel Ticket");
                Console.WriteLine("4. View Booking Status");
                Console.WriteLine("5. Print Ticket");
                Console.WriteLine("6. Back to Main Menu");
                Console.Write("Enter choice: ");

                
              
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        customer.ViewTrainsAndSeats();
                        break;
                    case "2":
                        customer.BookTicket();
                        break;
                    case "3":
                        customer.CancelTicket();
                        break;
                    case "4":
                        customer.ViewBookingStatus();
                        break;
                    case "5":
                        TicketGenerator.PrintTicket();
                        break;


                    case "6":
                        exit = true;
                        break;
                }
            }
        }
    }
        }
    

