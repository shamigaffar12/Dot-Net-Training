using System;
//Q3.Write a C# Sharp program to read any day number as an integer and display the name of the day as a word.

namespace Assignment2
{
    class Assignment2Q3
    {
        public static void Main(string[] args)
        {
            DayName.Day(); // display the name of  day
            Console.Read();


        }
    }

    class DayName
    {
        //function to take input by user and display name of the day
        public static void Day()
        {
            Console.WriteLine("-----Display Day Name of the Week---- ");
            int day;
            Console.Write("Enter a day number : ");
            day=Convert.ToInt32(Console.ReadLine()); //takes input from user 
             

            //based on the selection of day number it will print the name of day
            switch (day)
            {

                case '1':
                    Console.WriteLine("Monday");
                    break;
                case '2':
                    Console.WriteLine("Tuesday");
                    break;
                case '3':
                    Console.WriteLine("Wednesday");
                    break;
                case '4':
                    Console.WriteLine("Thursday");
                    break;
                case '5':
                    Console.WriteLine("Friday");
                    break;
                case '6':
                    Console.WriteLine("Saturday");
                    break;
                case '7':
                    Console.WriteLine("Sunday");
                    break;

                default :
                    Console.WriteLine("Invalid Input !");
                    break;


            }
        }

    }
}
