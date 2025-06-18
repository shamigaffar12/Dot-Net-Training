using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// program to compute the sum of two given integers. If two values are the same, return the triple of their sum.
/// </summary>
namespace Assignments
{
   static class Program
    {
        public static void Main(string [] args)
        {
            
            Console.Write(Sum.SumSol());//calling the SumSol() function and displaying its result
            Console.Read();
            
        }          
    }

    public static class Sum
    {
        //function for checking equality of two number and if equal find sum triplet
        public static int SumSol()
        {
            int num1, num2;//declare variable to store first number and second number enter by the user
            Console.WriteLine(" ");
            Console.Write("Enter the first number : ");
            num1 = Convert.ToInt32(Console.ReadLine()); //take input of first number from user
            Console.WriteLine("   ");

            Console.Write("Enter the second number : ");
            num2 = Convert.ToInt32(Console.ReadLine());  //take input of second number from user
            int result = num1 + num2; //calculte sum of num1 and num2 store in result 
            Console.WriteLine("");
            //checking the equality condition
            if (num1 == num2)
            {
                Console.WriteLine("The input number "+num1+" and "+num2+" is equal and their sum is "+result);
                Console.WriteLine("");
                Console.WriteLine("So the triplet sum of "+num1+num2+" is ");
                return (result * 3); // since input numbers are equal so it return triplet sum

            }
            else
            {
                Console.WriteLine(num1+" and "+num2+" are not equal ! ");
                Console.WriteLine("");
                Console.WriteLine("Sum of "+ num1 + " and " + num2 + " is "+result);

            }
            
            return 0;

        }
    }
}
