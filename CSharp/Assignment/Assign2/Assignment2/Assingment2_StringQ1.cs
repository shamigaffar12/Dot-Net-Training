using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * String Assignment
 1. Write a program in C# to accept a word from the user and display the length of it.
  */

namespace Assignment2
{
    class Assingment2_StringQ1
    {
        public static void Main(string [] args)
        {
            Str_Display.Str(); // calling the function that takes input by user and display length 
            Console.Read();
        }
    }

    class Str_Display
    {
        //function that take input form user and display length of word
        public static void Str()
        {
            Console.Write("Enter a word : ");
            String str =Console.ReadLine(); // Read a word of string type by user
            str.Trim(); // Remove unusual spaces
            Console.WriteLine(" ");
            Console.WriteLine("Length of word " + str + "  : " + str.Length); // displaying the length of word.
        }
       
    }

}
