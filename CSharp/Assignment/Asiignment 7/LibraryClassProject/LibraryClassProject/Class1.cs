using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryClassProject
{
    
    public class Ticketfare
    {
        public static string ClaculateConcession(int age , double totalfare)
        {
            if (age <= 5)
            {
                return "Little Champs - Free Ticket";
            }
            else if (age > 60)
            {
                double concessionFare = totalfare - (totalfare * 0.30); 
                return $"Senior Citizen - Fare after concession Rs: {concessionFare}";
            }
            else
            {
                return $"Ticket Booked - Fare Rs : {totalfare}";
            }
        }
    }
}
