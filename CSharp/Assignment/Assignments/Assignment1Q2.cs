using System;


// program to check given number is postive or not
namespace Assignments
{

  public static   class Check
    {

       
      public  static void Main(String [] args)
        {
            int num;
            
            Console.WriteLine("------Check given number is positive or not-----");
            Console.WriteLine("Enter a number :");

            num= Convert.ToInt32(Console.ReadLine()); // taking input from user

            Check.CheckFun(num); //calling CheckFun() function here
            Console.Read();

        }

        //function to check input number is positive or not 
        // here input comes as parameter of function that is number
        static void  CheckFun(int number)
        {
            //condition for checking the input number is positive or not
            if (number > 0)
            {
                Console.WriteLine(number + " is a positive number");
            }
            else
            {
                Console.WriteLine(number + "is a negative number");
            }
        }

    }
}