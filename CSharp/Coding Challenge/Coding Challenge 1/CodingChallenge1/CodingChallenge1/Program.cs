using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*1. Write a C# Sharp program to remove the character at a given position in the string. The given position will be in the range 0..(string length -1) inclusive.
 
Sample Input:
"Python", 1
"Python", 0
"Python", 4
Expected Output:
Pthon
ython
Pythn
*/
namespace CodingChallenge1

    
{
    class Python
    {
        public void input(string s , int pos)
        {
            string original = s;
            int count = 1;
            Console.WriteLine("After removing character: " + s.Remove(pos, count));
            
            
        }
    }
    /*
     * 2. Write a C# Sharp program to exchange the first and 
     * last characters in a given string and return the new string.
 
Sample Input:
"abcd"
"a"
"xy"
Expected Output:
 
dbca
a
yx
     */
    class Exchange
    {
        string s;
        public void exChar(string s)
        {
            this.s = s;
            string temp = s;
          
            
            string first=Convert.ToString(temp.ElementAt(0));
            
            
           
            string last = Convert.ToString(temp.Last());
            string mid=temp.Substring(1, temp.Length-2);
           

            Console.WriteLine(last+mid+first);





        }
    }
    class Greatest
    {
        
        public void greater(int num1,int num2, int num3)
        {
            if (num1 > num2 && num1>num3)
            {
                Console.WriteLine("Greatest Number is :" + num3);
            }
            else if (num2 > num1 && num2 > num3)
            {
                Console.WriteLine("Greatest Number is :" + num2);
            }
            else
            {
                Console.WriteLine("Greatest number is : " + num3);
            }

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Python obj1 = new Python();
            obj1.input("Python", 1);
         
            Exchange ex = new Exchange();
            ex.exChar("abcd");

            Greatest gr = new Greatest();
            gr.greater(10, 20, 30);
            Console.Read();
          
        }
    }
}
