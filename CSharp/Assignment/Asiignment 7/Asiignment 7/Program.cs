using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*1.) 
Write a query that returns list of numbers and their squares only if square is greater than 20 

Example input [7, 2, 30]  
Expected output
→ 7 - 49, 30 - 900
*/
namespace Asiignment_7
{
    class CheckSquare
    {
        
        public List<int> number = new List<int>();

    }
    class Program
    {
        static void Main(string[] args)
        {
            CheckSquare num = new CheckSquare();
            Console.WriteLine("Enter the numbers seperated by , : ");
            string numInput = Console.ReadLine();
            var nums = numInput.Split(',', (char)StringSplitOptions.RemoveEmptyEntries);
            foreach(var item in nums )
            {
                int num1 = Convert.ToInt32(item.Trim());
                num.number.Add(num1);
            }
            //Query Expression
            IEnumerable<int> value = from v in num.number
                                     where Math.Pow(v , 2)>20
                                     select v;
            //iterate in value to display the result
            foreach(var item in value)
            {
                Console.WriteLine($"{item}  {Math.Pow(item , 2)}");
            }

            Console.Read();
        }
    }
}
