using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* String Assignment
 * Q2.Write a program in C# to accept a word from the user and display the reverse of it. 
 */
namespace Assignment2
{
    class Assingment2_StringQ2
    {
        public static void Main(string[] args)
        {
            Str2.strRev(); // calling function that reverse the input word
            Console.Read();
        }
    }
    class Str2
    {
        // function to take input from user and display reverse of it
        public static void strRev()
        {
            Console.WriteLine("Enter a word : ");
            String str = Console.ReadLine(); // Reads string input from user
            string rev = new string(str.Reverse().ToArray()); // here rev store the reverse of str
            Console.WriteLine("Reverse of " + str + " is " + rev); // displaying the reverse of string
        }
    }

}
