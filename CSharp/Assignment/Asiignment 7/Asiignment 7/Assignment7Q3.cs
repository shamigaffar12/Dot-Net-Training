using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 *   3.	Create a list of employees with following property EmpId, EmpName, EmpCity, EmpSalary. Populate some data
Write a program for following requirement
a.	To display all employees data
b.	To display all employees data whose salary is greater than 45000
c.	To display all employees data who belong to Bangalore Region
d.	To display all employees data by their names is Ascending order

 */

namespace Asiignment_7
{
    public class Employee
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string EmpCity { get; set; }
        public double EmpSalary { get; set; }
        public List<Employee> empList = new List<Employee>();


        public static Employee AddEmployee()
        {
            int empId;
            string empName, empCity;
            double empSalary;
            

            Console.WriteLine("Enter Employee Id:");
            empId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Employee Name:");
            empName = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter Employee City:");
            empCity = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter Employee Salary:");
            empSalary = Convert.ToDouble(Console.ReadLine());

            return new Employee

            {
                EmpId = empId,
                EmpName = empName,
                EmpCity = empCity,
                EmpSalary = empSalary

            };
        }
     

    }
    class Assignment7Q3
    {
        static void Main() {
            
            List<Employee> employee = new List<Employee>();
            int choice = 0;
            do
            {
               
                Console.WriteLine("------Employee Details-------");
                Console.WriteLine("1.Add new emplooye");
                Console.WriteLine("2.Display all emplooye");
                Console.WriteLine("3.Display  emplooye (salaray>4500)");
                Console.WriteLine("4.Display emplooye (Banglore region)");
                Console.WriteLine("5.Display sorted emplooye By their name(ascending)");
                Console.WriteLine("6.Exit");
                Console.WriteLine("Enter your choice : ");
                choice = int.Parse(Console.ReadLine());


                switch (choice)
                {

                    case 1:
                        Console.Write("Enter number of employees to add: ");
                        int count = Convert.ToInt32(Console.ReadLine());

                        for (int i = 0; i < count; i++)
                        {
                            Console.WriteLine("Enter details for Employee " + (i + 1));
                            employee.Add(Employee.AddEmployee());
                        }
                        Console.WriteLine("Employee added successfully! ");
                        break;
                    case 2:

                        //  Display all employee data
                        Console.WriteLine(" Employees Details :");
                        DisplayEmployee(employee);
                        break;
                    case 3:
                        // display employee whose salary is grester than 45000
                        var emp = employee.Where(i => i.EmpSalary > 45000);
                        Console.WriteLine(" List of Employees whose salary > 45000  :");
                        DisplayEmployee(emp);
                        break;

                    case 4:

                        // display employee who belongs to Banglore Region
                        var eCity = employee.Where(i => i.EmpCity is "Banglore");
                        Console.WriteLine("Listof Employees who belongs to Banglore Region :");
                        DisplayEmployee(eCity);
                        break;
                    case 5:

                        //display employee data in ascending order
                        var sort = employee.OrderBy(i => i.EmpName);
                        Console.WriteLine(" Employee details in sorted in ascending order by their names :  :");
                        DisplayEmployee(sort);
                        break;
                    case 6:
                        System.Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid Choice !");
                        Console.WriteLine("Please enter a valid choice (1-6) ");
                        Console.WriteLine("-------------------------------------");
                        break;
                }

                Console.WriteLine("************************************");
                Console.Read();

            }while (choice != 6) ;
        }



        
        static void DisplayEmployee(IEnumerable<Employee>employee)
        {
            
            foreach (var item in employee )
            {
                Console.WriteLine($"ID: {item.EmpId}, Name: {item.EmpName}, City: {item.EmpCity}, Salary: {item.EmpSalary}");
            }

        }
    }
}
