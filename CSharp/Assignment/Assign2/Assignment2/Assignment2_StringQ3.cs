using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*String Assignment
 *Q3.Write a program in C# to accept two words from user and find out if they are same.
 */

namespace Assignment2
{
    class Assignment2_StringQ3
    {
        public static void Main(string [] args)
        {
            strCompare.stringCompare(); //calling stringCompare() function here
            Console.Read();
        }
        class strCompare
        {
            //function to compare two given words by user as string
            public static void stringCompare()
            {
                Console.WriteLine("Enter first word : ");
                string str1 = Console.ReadLine(); //Read first number from user
                str1.Trim(); //Remove unused space 
                Console.WriteLine("Enter second word : ");
                string str2 = Console.ReadLine(); ////Read second number from user
                str2.Trim(); //Remove unused space 
                // comparing str1 and str2 are same or not and displaying its result
                if (str1.Equals(str2) is true) //true 

                {
                    Console.WriteLine(" The given words are same. ");
                }
                else //false
                {
                    Console.WriteLine("The given words are not same !");

                }
               

            }

        }
    }
}
