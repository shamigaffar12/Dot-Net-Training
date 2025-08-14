using System;
using System.Data;
using System.Data.SqlClient;
namespace RailwayReservation.Models
{
    public class TrainMaster
    {
        public int TrainNumber { get; set; }
        public string TrainName { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
    }
}
