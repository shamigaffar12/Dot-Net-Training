using System;
using System.Data;
using System.Data.SqlClient;
namespace RailwayReservation.Models
{
    public class Admin
    {
        public int AdminId { get; set; }
        public string AdminUsername { get; set; }
        public string AdminPassword { get; set; }
    }
}
