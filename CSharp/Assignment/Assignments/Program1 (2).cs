using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignments
{
    class Solution
    {
        static void Main(string[] args)
        {
            double num1, num2, result; // variables to store  two numbers & store result
            char operat;


            Console.WriteLine("Enter your first number : ");
            num1 = Convert.ToDouble(Console.ReadLine()); // taking the input from user side for first number
            Console.WriteLine("Choose operator from below ");
            Console.WriteLine("+ , - ,* , /");
            operat = Convert.ToChar(Console.ReadLine()); // selection of operator by user 
            Console.WriteLine("Enter your second number : ");
            num2 = Convert.ToDouble(Console.ReadLine()); //taking the input from user for second number

            Solution.Calculate(operat); //calling the Calculate() function for calculation 
            Console.Read();
        }

        static void Calculate(char Operator) // function for calculation

        {
            char operat = Operator;


            switch (operat)
            {
                case '+':
                    result = num1+num2;
                    Console.WriteLine("Result of num1 " + operat + " num2 = " + result);
                    break;
                case '-':
                    result = num1-num2;
                    Console.WriteLine("Result of num1 " + operat + " num2 = " + result);
                    break;
                case '*':
                    result = num1*num2;
                    Console.WriteLine("Result of num1 " + operat + " num2 = " + result);
                    break;
                case '/':
                    result = num1/num2;
                    Console.WriteLine("Result of num1 " + operat + " num2 = " + result);
                    break;
                default:
                    Console.WriteLine("Invalid Opertor !");
            }

            
            }
            

      
        }
    }
}
