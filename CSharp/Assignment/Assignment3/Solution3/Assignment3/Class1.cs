using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * Scenario:
A company is developing a billing system. A developer is asked to calculate the total cost of 5 items, each priced at ₹200, and then apply a 10% discount.
Question:
Write a simple C# code snippet using arithmetic operators to:
Calculate the total price
Apply the 10% discount
Print the final amount to be paid
 */

namespace Assignment3
{
    class Product
    {
        double qty;
        double price;
        double discount;
        double total;

        public void customer()
        {
            Console.WriteLine("Enter the quantity :");
            qty = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter the price of each item :");
            price = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter the discount in % :");
            discount = Convert.ToDouble(Console.ReadLine());

            total = (qty * price) - (discount/100)*(qty*price);
            Console.WriteLine("Total Cost : ");
            Console.WriteLine(total);
        }
    }

    /*
     * Operator Overloading
Scenario:
You are creating a Box class that has a Length property. The team wants to be able to "add" two boxes by summing their lengths.
Question:
Create a simple Box class and overload the + operator so that adding two boxes returns a new box with the combined length.
     */
    class Box
    {
        public int length;

        public static Box operator +(Box box1,Box box2)

        {
            Box box = new Box();
            box.length = box1.length + box2.length;
            return box;

        }

       
    }

    /*
     *.Scenario:
You are building a leaderboard and want to compare player scores.
Question:
Write a class Player with a Score property and implement a method that:
Compares two Player objects using ==
Uses .Equals()
Uses .CompareTo() for sorting logic
Explain what each comparison is checking.
     */
    class Player
    {
        
        public int score { get;  set; }
        
        public Player(int score)
        {
            this.score = score;
        }
        public void Scores()
        {
            Player p1 = new Player(10000);
            //p1.score = 1000;
            Player p2 = new Player(50000);

            Console.WriteLine("Equality operator :"+(p1==p2));

            Console.WriteLine("Comparing values in each object By Equals "+p1.Equals(p2));
           Console.WriteLine("Compare two object "+p1.score.CompareTo(p2.score));
            //Player.CompareTo( p2);


        }

    }
    class Class1
    {
        public static void Main(string [] args)
        {
            //Product product = new Product();
            //product.customer();
           
            //Box b1 = new Box();
            //b1.length = 10;
            //Box b2 = new Box();
            //b2.length = 5;
            //Box b3 = b1 + b2;
            //Console.WriteLine("Length of box1 + Box2 : " + b3.length);
            Player p1 = new Player(10000);
            //p1.score = 1000;
            Player p2 = new Player(50000);
            p1.Scores();
            p2.Scores();
            Player.
            //Console.WriteLine(p1.score.CompareTo(p2.score));
            //Console.Read();



        }
    }
}
