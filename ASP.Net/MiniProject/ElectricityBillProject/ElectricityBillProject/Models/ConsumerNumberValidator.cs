using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text.RegularExpressions;

namespace ElectricityBillProject.Models
{
    public static class ConsumerNumberValidator
    {
        // Validates format like: EB12345
        public static void Validate(string consumerNumber)
        {
            if (string.IsNullOrWhiteSpace(consumerNumber) || !Regex.IsMatch(consumerNumber, @"^EB\d{5}$"))
            {
                throw new FormatException("Invalid Consumer Number format. It must be like EB12345.");
            }
        }

       
        public static bool IsValid(string consumerNumber)
        {
            return Regex.IsMatch(consumerNumber ?? "", @"^EB\d{5}$");
        }
    }
}
