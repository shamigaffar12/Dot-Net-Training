using System;
using System.Data;
using System.Data.SqlClient;
namespace RailwayReservation.Models
{
    public class Customer
    {
        public int CustId { get; set; }
        public string CustName { get; set; }
        public string CustPhone { get; set; }
        public string CustEmail { get; set; }
        public string CustPassword { get; set; }
    }
}
