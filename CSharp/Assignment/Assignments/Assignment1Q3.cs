using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// program for basic mathematical calculation (+ ,-,*,/) by taking input from the user

namespace Assignments
{
    class Solution
    {
        static void Main(string[] args)
        {
            Solution.Calculate(); //calling the Calculate() function for calculation 
            Console.Read();
        }

        static void Calculate() // function for calculation

        {

            double num1, num2 , result; // variables to store  two numbers & store result
            char operat;
            Console.WriteLine("Enter your first number : ");
            num1 = Convert.ToDouble(Console.ReadLine()); // taking the input from user side for first number
            Console.WriteLine("Choose operator from below ");
            Console.WriteLine("+ , - ,* , /");
            operat = Convert.ToChar(Console.ReadLine()); // selection of operator by user 
            Console.WriteLine("Enter your second number : ");
            num2 = Convert.ToDouble(Console.ReadLine()); //taking the input from user for second number


            switch (operat)
            {
                case '+':
                    result =num1+num2;
                    Console.WriteLine("Result of num1 " + operat + " num2 = " + result);
                    break;
                case '-':
                    result = num1-num2;
                    Console.WriteLine("Result of "+num1  + operat +  num2 +"= " + result);
                    break;
                case '*':
                    result = num1*num2;
                    Console.WriteLine("Result of "+ num1  + operat +  num2 +" = " + result);
                    break;
                case '/':
                    result = num1/num2;
                    Console.WriteLine("Result of " + num1 + operat + num2 + " = " + result);
                    break;
                default :
                    Console.WriteLine("Invalid Opertor !");
                    break;
            }


        }


    }
    }


