using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_CodeFirst.Models
{
   
    public class Movies
    {
        [Key]
        public int Mid { get; set; }

        [Required]
        public string MovieName { get; set; }

        [Required]
        public string DirectorName { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfRelease { get; set; }
    }
}
