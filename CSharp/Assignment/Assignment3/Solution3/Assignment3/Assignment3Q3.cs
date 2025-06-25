using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 3. Create a class called Saledetails which has data members like Salesno,  Productno,  Price, dateofsale, Qty, TotalAmount
- Create a method called Sales() that takes qty, Price details of the object and updates the TotalAmount as  Qty *Price
- Pass the other information like SalesNo, Productno, Price,Qty and Dateof sale through constructor
- call the show data method to display the values without an object.
 */

namespace Assignment3
{
    class Saledetails
    {
        public string Salesno, Productno;
        public double Price;
        public DateTime dateofsale;
        public int Qty;
        public double TotalAmount;

        public Saledetails(string salesNo, string productNo, double price, int qty,DateTime salesDate)
        {
            this.Salesno = salesNo;
            this.Productno = productNo;
            this.Price = price;
            this.Qty = qty;
            this.dateofsale = salesDate;
        }

        public double Sales(int Qty ,double Price)
        {
            this.Qty = Qty;
            this.Price = Price;
            TotalAmount = Qty * Price;

            return TotalAmount;
           
        }
        public void showData()
        {
            Console.WriteLine("Sales Number : "+Salesno);
            Console.WriteLine("Product Number : " + Productno);
            Console.WriteLine("Quantity of product  : " + Qty);
            Console.WriteLine("Price of each product : " + Price);
            Console.WriteLine("Purchased Date : " + dateofsale);
            Console.WriteLine("Total Amount to be paid :" + Sales(Qty, Price));



        }
       
        
        


    }
    class Assignment3Q3
    {
        public static void Main(string [] args)
        {
            string Salesno, Productno;
            double Price;
            DateTime dateofsale;
            int Qty;
            double TotalAmount;

            Console.WriteLine("Enter the sales number :");
            Salesno=Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter the product number :");
            Productno = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter the price of each product :");
            Price = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter the quantity of purchased item :");
            Qty = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the Date of Purchasing :");
               dateofsale= Convert.ToDateTime(Console.ReadLine());
            Saledetails customer = new Saledetails(Salesno,Productno, Price, Qty,dateofsale);
            customer.showData();
            Console.Read();
            

        }
    }
}
