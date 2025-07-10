using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * 4. Write a console program that uses delegate object
 * as an argument to call Calculator Functionalities like
 * 1. Addition, 2. Subtraction and 3. Multiplication by 
 * taking 2 integers and returning the output to the user. 
 * You should display all the return values accordingly.
 */

namespace Code_Challenge_3
{

    public delegate T Calc<T>(T arg);

    public delegate int CalculatorDelegate(int x, int y);

        class Calculator
        {
        public static void Calculate<T>(T[] values, Calc<T> targ)
        {
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = targ(values[i]);
            }
        }
        public static void DoOperation(int a, int b, CalculatorDelegate add)
        {
            int result = a+b;
            Console.WriteLine(result);
        }
        static int Subtracttion(int n1, int n2)
        {
            return n1 - n2;
        }

        static int Multiply(int n1, int n2)
        {
            return n1 * n2;
        }

        static int Divide(int a1, int a2)
        {
            return a1 / a2;
        }
    }



        class class4
        {
            
          

            static void Main()
            {
               Calculator cal = new Calculator();


                Console.Write("Enter first integer: ");
                int x = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter second integer: ");
                int y = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Choose an operation:");
                Console.WriteLine("1. Addition");
                Console.WriteLine("2. Subtraction");
                Console.WriteLine("3. Multiplication");
                Console.Write("Your choice  ");
            int choice = 0;
            switch (choice) { 


           

            }



        }
             

        
    



