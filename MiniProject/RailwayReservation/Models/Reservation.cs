using System;
using System.Data;
using System.Data.SqlClient;
namespace RailwayReservation.Models
{
    public class Reservation
    {
        public int BookingId { get; set; }
        public int CustId { get; set; }
        public int TrainClassId { get; set; }
        public System.DateTime TravelDate { get; set; }
        public string CurrentStatus { get; set; }
        public System.DateTime BookingDate { get; set; }
        public decimal Amount { get; set; }
    }
}
