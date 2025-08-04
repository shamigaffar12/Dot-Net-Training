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
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DOJ { get; set; }
        public string City { get; set; }
        static List<Employee> emplist = new List<Employee>();

        //adding the employee data in emplist
        internal void AddEmployee(Employee emp)
        {
            emplist.Add(emp);
        }
        // create a function for query
       public static void Search(int choice)
        {
            switch (choice)
            {
                case 1:
                    var result = emplist.Where(emp => emp.DOJ < new DateTime(2015, 1, 1));
                    Display(result);
                    break;

                case 2:
                    var emp1 = emplist.Where(emp => emp.DOB > new DateTime(1990, 1, 1));
                    Display(emp1);
                    break;

                case 3:
                    var emp2 = emplist
                        .Where(emp => emp.Title == "Consultant" || emp.Title == "Associate");
                    Display(emp2);
                    break;

                case 4:
                    Console.WriteLine("Total no of Employees: " + emplist.Count);
                    break;

                case 5:
                    int count = emplist.Count(emp => emp.City == "Chennai");
                    Console.WriteLine("Employees in Chennai: " + count);
                    break;

                case 6:
                    int maxId = emplist.Max(emp => emp.EmployeeID);
                    Console.WriteLine("Highest Employee ID: " + maxId);
                    break;

                case 7:
                    int count2 = emplist.Count(emp => emp.DOJ > new DateTime(2015, 1, 1));
                    Console.WriteLine("Employees joined after 1/1/2015: " + count2);
                    break;

                case 8:
                    int count3 = emplist.Count(emp => emp.Title != "Associate");
                    Console.WriteLine("Employees not Associates: " + count3);
                    break;

                case 9:
                    var emp3 = emplist.GroupBy(emp => emp.City);
                    foreach (var item  in emp3)
                    {
                        Console.WriteLine($"{item.Key}: {item.Count()}");
                    }
                    break;

                case 10:
                    var city= emplist.GroupBy(emp => new { emp.City, emp.Title })
                   .Select(c => new { c.Key.City, c.Key.Title, Count = c.Count() });
                    foreach (var item in city)
                    {
                        Console.WriteLine($"{item.City} - {item.Title}: {item.Count}");
                    }
                    break;

                case 11:
                    var resultmax = emplist.Max(emp => emp.DOB);
                    var emp4 = emplist.Where( emp=> emp.DOB == resultmax);
                    Console.WriteLine("Youngest Employee:");
                    Display(emp4);
                    break;
                case 12:
                    Environment.Exit(0);
                    break;


                default:
                    Console.WriteLine("Invalid choice. Select valid option.");
                    break;
            }
        }

        //create function to display employee data
        public static void Display(IEnumerable<Employee> emp)
        {
            foreach (var item in emp)
            {
                Console.WriteLine($"EmployeeID : {item.EmployeeID}, First Name : {item.FirstName}, Last Name : {item.LastName},  Title : {item.Title},  DOB : {item.DOB.ToShortDateString()},  DOJ : {item.DOJ.ToShortDateString()}, City: {item.City}");
            }
        }

    }


    class Program
    {
        static void Main(string[] args)
        {

            Employee emp = new Employee();

            emp.AddEmployee(new Employee { EmployeeID = 1001, FirstName = "Malcolm", LastName = "Daruwalla", Title = "Manager", DOB = DateTime.Parse("11/16/1984"), DOJ = DateTime.Parse("06/08/2011"), City = "Mumbai" });
            emp.AddEmployee(new Employee { EmployeeID = 1002, FirstName = "Asdin", LastName = "Dhalla", Title = "AsstManager", DOB = DateTime.Parse("08/20/1984"), DOJ = DateTime.Parse("07/07/2012"), City = "Mumbai" });
            emp.AddEmployee(new Employee { EmployeeID = 1003, FirstName = "Madhavi", LastName = "Oza", Title = "Consultant", DOB = DateTime.Parse("11/14/1987"), DOJ = DateTime.Parse("04/12/2015"), City = "Pune" });
            emp.AddEmployee(new Employee { EmployeeID = 1004, FirstName = "Saba", LastName = "Shaikh", Title = "SE", DOB = DateTime.Parse("06/03/1990"), DOJ = DateTime.Parse("02/02/2016"), City = "Pune" });
            emp.AddEmployee(new Employee { EmployeeID = 1005, FirstName = "Nazia", LastName = "Shaikh", Title = "SE", DOB = DateTime.Parse("03/08/1991"), DOJ = DateTime.Parse("02/02/2016"), City = "Mumbai" });
            emp.AddEmployee(new Employee { EmployeeID = 1006, FirstName = "Amit", LastName = "Pathak", Title = "Consultant", DOB = DateTime.Parse("11/07/1989"), DOJ = DateTime.Parse("08/08/2014"), City = "Chennai" });
            emp.AddEmployee(new Employee { EmployeeID = 1007, FirstName = "Vijay", LastName = "Natrajan", Title = "Consultant", DOB = DateTime.Parse("12/02/1989"), DOJ = DateTime.Parse("06/01/2015"), City = "Mumbai" });
            emp.AddEmployee(new Employee { EmployeeID = 1008, FirstName = "Rahul", LastName = "Dubey", Title = "Associate", DOB = DateTime.Parse("11/11/1993"), DOJ = DateTime.Parse("11/06/2014"), City = "Chennai" });
            emp.AddEmployee(new Employee { EmployeeID = 1009, FirstName = "Suresh", LastName = "Mistry", Title = "Associate", DOB = DateTime.Parse("08/12/1992"), DOJ = DateTime.Parse("12/03/2014"), City = "Chennai" });
            emp.AddEmployee(new Employee { EmployeeID = 1010, FirstName = "Sumit", LastName = "Shah", Title = "Manager", DOB = DateTime.Parse("04/12/1991"), DOJ = DateTime.Parse("01/02/2016"), City = "Pune" });

            while (true)
            {
                Console.WriteLine("--- Employee Menu Options ---");
                Console.WriteLine("1. Employees joined before 1/1/2015");
                Console.WriteLine("2. Employees born after 1/1/1990");
                Console.WriteLine("3. Emp either Consultants or Associates");
                Console.WriteLine("4. Total no of Employees");
                Console.WriteLine("5. Employees in Chennai");
                Console.WriteLine("6. Highest Employee ID");
                Console.WriteLine("7. Emp Joined after 1/1/2015");
                Console.WriteLine("8. Emp Not Associates");
                Console.WriteLine("9. Total emp by City");
                Console.WriteLine("10. Total emp by City & Title");
                Console.WriteLine("11. Youngest Employee");
                Console.WriteLine("10. Exit");

                Console.Write("Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                
                    Employee.Search(choice);
                }
               
            }




        }   }

