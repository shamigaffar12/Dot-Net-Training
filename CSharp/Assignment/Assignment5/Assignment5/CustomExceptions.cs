using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment5
{
    public class InsuffientBalanceException : ApplicationException
    {
        public  InsuffientBalanceException(string message):base(message){

        }
    }
    public class InvalidInput : ApplicationException
    {
        public InvalidInput (string message) : base(message)
        {

        }
    }

    class CustomExceptions
    {
    }
}
