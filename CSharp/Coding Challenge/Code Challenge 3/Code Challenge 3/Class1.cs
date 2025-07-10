using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*2.Write a class Box that has Length and breadth as its members. 
 * Write a function that adds 2 box objects and stores in the 3rd.
 * Display the 3rd object details. Create a Test class to execute the above.
 */

namespace Code_Challenge_3
{
    class Box
    {
        public int length;
        public int breadth;

        public static Box operator + (Box box1, Box box2)
        {
            Box temp = new Box();
            temp.length = box1.length+box2.length;
            temp.breadth = box1.breadth + box2.breadth;

            return temp;
        }

    }
    class Class1

    {
        static void Main()
        {
            Box box1 = new Box();
            Console.WriteLine("Enter the length of box1 :");
            box1.length =Convert.ToInt32( Console.ReadLine());
            Console.WriteLine("Enter the breadth of box1 :");
            box1.length = Convert.ToInt32(Console.ReadLine());

            Box box2 = new Box();
            Console.WriteLine("Enter the length of box2 :");
            box2.length = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the breadth of box2 :");

            box2.breadth = Convert.ToInt32(Console.ReadLine());

            
            Box box3 = box1 + box2;
            Console.WriteLine("Length of box1 + box2 is  " + box3.length);
            Console.WriteLine("Breadth of box1+box2 is " + box3.breadth);
            Console.Read();


        }
    }
}
