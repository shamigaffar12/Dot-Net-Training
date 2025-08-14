using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using RailwayReservation.Models;

namespace RailwayReservation.DataAccessClass
{
    public class TrainDataAccess
    {
        public List<TrainMaster> GetAllTrains()
        {
            var list = new List<TrainMaster>();
            using (SqlConnection conn = DatabaseConnection.getConnection())
            {
                var cmd = new SqlCommand("SELECT TrainNumber,TrainName,Source,Destination FROM TrainMaster", conn);
                var rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    list.Add(new TrainMaster
                    {
                        TrainNumber = Convert.ToInt32(rd["TrainNumber"]),
                        TrainName = rd["TrainName"].ToString(),
                        Source = rd["Source"].ToString(),
                        Destination = rd["Destination"].ToString()
                    });
                }
            }
            return list;
        }

        public List<TrainClass> GetClassesForTrain(int trainNumber)
        {
            var list = new List<TrainClass>();
            using (SqlConnection conn = DatabaseConnection.getConnection())
            {
                var cmd = new SqlCommand("SELECT TrainClassId,TrainNumber,ClassType,AvailableSeats,MaxSeats,Price FROM TrainClasses WHERE TrainNumber=@tn", conn);
                cmd.Parameters.AddWithValue("@tn", trainNumber);
                var rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    list.Add(new TrainClass
                    {
                        TrainClassId = Convert.ToInt32(rd["TrainClassId"]),
                        TrainNumber = Convert.ToInt32(rd["TrainNumber"]),
                        ClassType = rd["ClassType"].ToString(),
                        AvailableSeats = Convert.ToInt32(rd["AvailableSeats"]),
                        MaxSeats = Convert.ToInt32(rd["MaxSeats"]),
                        Price = Convert.ToDecimal(rd["Price"]) 
                    });
                }
            }
            return list;
        }

        public void ShowAllTrains()
        {
            using (SqlConnection conn = DatabaseConnection.getConnection())
            {
                string query = @"
                SELECT tm.TrainNumber, tm.TrainName, tm.Source, tm.Destination, 
                       tc.ClassType, tc.AvailableSeats, tc.MaxSeats, tc.Price
                FROM TrainMaster tm
                JOIN TrainClasses tc ON tm.TrainNumber = tc.TrainNumber
                ORDER BY tm.TrainNumber, tc.ClassType";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader rd = cmd.ExecuteReader();
                Console.WriteLine("\n--- Train List ---");
                while (rd.Read())
                {
                    Console.WriteLine($"Train No: {Convert.ToInt32(rd["TrainNumber"])}, " +
                                      $"Name: {rd["TrainName"]}, " +
                                      $"From: {rd["Source"]} To: {rd["Destination"]}, " +
                                      $"Class: {rd["ClassType"]}, " +
                                      $"Seats: {rd["AvailableSeats"]}/{rd["MaxSeats"]}, " +
                                      $"Price: {rd["Price"]}");
                }
                rd.Close();
            }
        }

        public void AddTrain()
        {
            using (SqlConnection conn = DatabaseConnection.getConnection())
            {
                Console.Write("Enter Train Number: ");
                int trainNumber = int.Parse(Console.ReadLine());
                Console.Write("Enter Train Name: ");
                string trainName = Console.ReadLine();
                Console.Write("Enter Source: ");
                string source = Console.ReadLine();
                Console.Write("Enter Destination: ");
                string destination = Console.ReadLine();

                // Insert into TrainMaster
                SqlCommand cmd = new SqlCommand("INSERT INTO TrainMaster (TrainNumber, TrainName, Source, Destination) VALUES (@num, @name, @src, @dest)", conn);
                cmd.Parameters.AddWithValue("@num", trainNumber);
                cmd.Parameters.AddWithValue("@name", trainName);
                cmd.Parameters.AddWithValue("@src", source);
                cmd.Parameters.AddWithValue("@dest", destination);
                cmd.ExecuteNonQuery();

                // Add each class type
                string[] classes = { "Sleeper", "AC3", "AC2", "AC1" };
                foreach (var cls in classes)
                {
                    Console.WriteLine($"\nEnter details for {cls}:");
                    Console.Write("Max Seats: ");
                    int maxSeats = int.Parse(Console.ReadLine());
                    Console.Write("Price: ");
                    decimal price = decimal.Parse(Console.ReadLine());

                    SqlCommand classCmd = new SqlCommand("INSERT INTO TrainClasses (TrainNumber, ClassType, AvailableSeats, MaxSeats, Price) VALUES (@num, @cls, @max, @max, @price)", conn);
                    classCmd.Parameters.AddWithValue("@num", trainNumber);
                    classCmd.Parameters.AddWithValue("@cls", cls);
                    classCmd.Parameters.AddWithValue("@max", maxSeats);
                    classCmd.Parameters.AddWithValue("@price", price);
                    classCmd.ExecuteNonQuery();
                }

                Console.WriteLine("Train added successfully.");
            }
        }

        public void RemoveTrain()
        {
            using (SqlConnection conn = DatabaseConnection.getConnection())
            {
                Console.Write("Enter Train Number to remove: ");
                int trainNumber = int.Parse(Console.ReadLine());

                SqlCommand cmd = new SqlCommand("DELETE FROM TrainClasses WHERE TrainNumber=@num", conn);
                cmd.Parameters.AddWithValue("@num", trainNumber);
                cmd.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand("DELETE FROM TrainMaster WHERE TrainNumber=@num", conn);
                cmd2.Parameters.AddWithValue("@num", trainNumber);
                cmd2.ExecuteNonQuery();

                Console.WriteLine("Train removed successfully.");
            }
        }
    }
}

    

