using System;
// Write a C# program that takes a number as input and displays it four times in a row
// (separated by blank spaces), and then four times in the next row, with no separation.
// You should do it twice: Use the console. Write and use {0}.

namespace Assignment2
{
    class Number
    {
        public static void Main(string [] args)
        {
            Num.Display(); //calling Display() function to display the result
            Console.Read();

        }
    }
    class Num
    {
        //function to take a numric digit by user and displaying it in pattern
        public static void Display()
        {
            int num1; //declaring num1 variable to store the digit in it
            Console.Write("Enter a digit :");
            num1 = Convert.ToInt32(Console.ReadLine());//ttake a numeric digit as input by user
            Console.WriteLine("Output :");
            int r = 0;
            //condition for displaying the pattern of digits
            while (r != 2) {
                Console.WriteLine(" ");
                for (int i = 0; i < 4; i++)
                {
                    Console.Write("{0}", num1);
                    Console.Write(" ");
                }
                Console.WriteLine(" ");
                for (int j = 0; j < 4; j++)
                {
                    Console.Write("{0}", num1);

                }


                r++;
            
        }


        }


        }
    }

