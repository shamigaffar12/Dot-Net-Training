using System;
/*3.Write a C# program to implement a method that takes an integer as 
 * input and throws an exception if the number is negative.
 * Handle the exception in the calling code.
*/
namespace ConsoleApp1
{
    //creating custom exception class
public class NegativeValueException : Exception
    {
        public  NegativeValueException(string message) : base(message)
        {

        }
    }

    //class to take an integer and check it is positive or negative
    class Number : Exception
    {
        public int number { get; set; }
        public void negativeValueCheck (int n)
        {
            this.number = n;
            if (n < 0)
            {
                throw new NegativeValueException("Cannot Accept Negative Integers!"); //throw excpetion if input is negative integer

            }
        }
    }
    class ExceptionEx
    {
        static void Main(string[] args)
        {
            Number num = new Number(); //calling the Number class
        labelcheck:
            //Handling the code with the help of try catch 
            try
            {
                
                Console.WriteLine("Enter a integer number :");
                int num1 = Convert.ToInt32(Console.ReadLine());//Reading input from user
                num.negativeValueCheck(num1); //calling function to input integer
            }
            catch(NegativeValueException ex)
            {
                Console.WriteLine("Exception Occured : " + ex.Message); //Handling the exception
                goto labelcheck; //jump to input till positive number not added;
            }
            
            Console.Read();
        }
    }
}