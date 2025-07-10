using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*2.Create a Class called Products with Productid, 
 * Product Name, Price. Accept 10 Products, sort 
 * them based on the price, and display the sorted Products */
namespace ConsoleApp1
{
 // creating product class to accept product details
    class Product
    {
        //create properties to accept value and to read it
        public string productId { get; set; }
        public string productName { get; set; }
        public double productPrice { get; set; }


       //creating non parametrized conctructor
        public Product()
        {
            
            Console.WriteLine("Enter the Product Id ");
            productId = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter the Product Name :");
            productName = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter the Product Price :");
            productPrice = Convert.ToDouble(Console.ReadLine());
            
        }
    }
    class Products
    {
        static void Main(string[] args)
        {
            //creating a list of Product Type
            List<Product> pList = new List<Product>();
            int choice;
            do
            {

                Console.WriteLine("--- Product Details---");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. Display Product sorted by price");
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice: ");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {

                    case 1:
                        Console.WriteLine("Enter the number of products to add :");

                        int num = Convert.ToInt32(Console.ReadLine());

                        for (int i = 0; i < num; i++)
                        {
                            Console.WriteLine("Enter product details of Product"+(i + 1));
                            pList.Add(new Product());

                        }
                        Console.WriteLine("Product Added Successfully");
                        break;
                    case 2:
                        //sorting the product list
                         var result=pList.OrderBy(p => p.productPrice);
                        foreach (var item in result )
                        
                            {
                                Console.WriteLine("Product ID   : " + item.productId);
                                Console.WriteLine("Product Name : " + item.productName);
                                Console.WriteLine("Product Price: " + item.productPrice);
                                
                            }
                        
                        break;
                    case 4:
                        System.Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid Choice !");
                        break;
                }

                Console.WriteLine("********************************");
              
            } while (choice != 4);
            Console.Read();
        }

    }



    
}
