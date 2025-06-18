using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignments
{
    class CheckEqual
    {
        static void Main(string[] args)
        {
            CheckEqual.EqualityCheck(); //calling the CheckEqual() for equality checking
            Console.Read();
        }

        static void EqualityCheck() // function to check wheter given numbers are equal or not

        {
            labelcheck:
            int num1, num2; // variables to store  two numbers
            Console.WriteLine("Assignment 1  Q1. Check both number are equal or not");
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("Enter your first number : ");
            num1 = Convert.ToInt32(Console.ReadLine()); // taking the input from user side for first number
            Console.WriteLine("Enter your second number : ");
            num2 = Convert.ToInt32(Console.ReadLine()); //taking the input from user for second number

            if(num1==0 || num2 == 0) //check if the given number is not zero 
            {
                Console.WriteLine("Please enter the number greater than 0 !");
                goto labelcheck; //it will jump to above code until the user gives input greater than zero in num1 & num2 field

            }

           else if (num1 == num2) //conditional checking for equality of num1 and num2
            {
                Console.WriteLine(num1 + " and " + num2 + "are equal"); // if num1 and num2 are equal then print the message
            }
            else
            {
                Console.WriteLine(num1 + "and " + num2+ "are not equal");// message print if not equal
            }

        }
    }
}
