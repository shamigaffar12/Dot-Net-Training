using System;
//program for swapping of two nunber

namespace Assignment2
{
    class Assignment2Q1
    {
        static void Main(string[] args)
        {
            SwapNum.swapping(); // display the swapped number
            Console.Read();
        }
    }

    class SwapNum
    {
        //function that take two input from user and swap it 
        public static void swapping()
          
        {
            Console.WriteLine("---Swap two numbers-----");
            int num1, num2, swap; // variable declaration
            Console.Write("Enter the first number : ");
            num1=Convert.ToInt32(Console.ReadLine()); //input first number by user
            Console.Write("Enter the second number : ");
            num2 = Convert.ToInt32(Console.ReadLine()); // input second number by user

            //swapping the num1 and num2 number using swap variable
            swap = num1;
            num1 = num2;
            num2 = swap;

            Console.WriteLine(" After sapping the given number output is  : ");
            Console.WriteLine("Swapping of first number :" + num1); //prints swaaped value (num2)
            Console.WriteLine("Swapping second number : " + num2); //prints swappaed value(num1)







        }
    }
}
