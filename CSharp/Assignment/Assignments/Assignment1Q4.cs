using System;
 // program for taking input from user and displaying multiplication table of the given number

namespace Assignments
{
   public static class MultipTable
    {
        static void Main(string [] args)
        {

            Table.Mult(); // calling the function which shows the output
            Console.Read();
        }

    }
   public static  class Table
    {
        // function for multplication table
        public static void Mult()
        {
            int num; //declare num variable to store input number
            Console.WriteLine("------Multiplication Table-----");
            Console.WriteLine("            ");
            Console.WriteLine("Enter the number : ");
            num=Convert.ToInt32(Console.ReadLine()); //taking input from the user

            Console.WriteLine("Multiplication Table of "+num+":");

            //displaying the multiplication table

            for (int i=1; i<10; i++)
            {
                int result = num * i;
                Console.WriteLine(num + "*" + i + " = " + result); 
            }

        }
    }
}
