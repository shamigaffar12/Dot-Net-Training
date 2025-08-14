using System;
using System.Data;
using System.Data.SqlClient;
namespace RailwayReservation.Models
{
    public class Cancellation
    {
        public int CancelId { get; set; }
        public int BookingId { get; set; }
        public System.DateTime CancelDate { get; set; }
    }
}
