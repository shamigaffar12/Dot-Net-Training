using System;
using System.Data;
using System.Data.SqlClient;
namespace RailwayReservation.Models
{
    public class TrainClass
    {
        public int TrainClassId { get; set; }
        public int TrainNumber { get; set; }
        public string ClassType { get; set; }
        public int AvailableSeats { get; set; }
        public int MaxSeats { get; set; }
        public decimal Price { get; set; }
    }
}
