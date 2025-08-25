using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_CodeFirst.Models
{


    public class MoviesDbContext : MoviesContextBase
    {
        public MoviesDbContext() : base("connectstr")  // Use the connection string name here
        {
        }
    }
}