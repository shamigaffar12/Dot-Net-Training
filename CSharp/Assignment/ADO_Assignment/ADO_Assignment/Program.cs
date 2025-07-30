using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ADO_Assignment
{
    class Employee
    {
        public static DataTable getEmpTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EmployeeID", typeof(int));
            dt.Columns.Add("FirstName", typeof(string));
            dt.Columns.Add("LastName", typeof(string));
            dt.Columns.Add("Title", typeof(string));
            dt.Columns.Add("DOB", typeof(DateTime));
            dt.Columns.Add("DOJ", typeof(DateTime));
            dt.Columns.Add("City", typeof(string));
            

            //add employee data by user

           Console.WriteLine("Enter number of employees to add: ");
           int empCount =Convert.ToInt32(Console.ReadLine());
                  for(int i=0; i<empCount;i++)
                {
                    Console.WriteLine($"Enter details for Employee {i + 1}:");

                    Console.Write("EmployeeID: ");
                    int EmployeeId = Convert.ToInt32(Console.ReadLine());

                    Console.Write("First Name: ");
                    string firstName = Console.ReadLine();

                    Console.Write("Last Name: ");
                    string lastName = Console.ReadLine();

                    Console.Write("Title: ");
                    string title = Console.ReadLine();

                    Console.Write("Date of Birth (yyyy-mm-dd): ");
                    DateTime DOB = Convert.ToDateTime(Console.ReadLine());

                    Console.Write("Date of Joining as (yyyy-mm-dd) : ");
                    DateTime DOJ = Convert.ToDateTime(Console.ReadLine());

                    Console.Write("City: ");
                    string city = Console.ReadLine();

                    dt.Rows.Add(EmployeeId, firstName, lastName, title, DOB, DOJ, city);
                }
            return dt;
        }

            
    }

}
class Program
    {
        static void Main(string[] args)
        {
        }
    }

