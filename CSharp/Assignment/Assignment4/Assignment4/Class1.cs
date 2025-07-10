using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*Assignment 4
 * Scenario: Employee Management System (Console-Based)
-----------------------------------------------------
You are tasked with developing a simple console-based Employee Management System using C# that allows users to manage a list of employees. Use a menu-driven approach to perform CRUD operations on a List<Employee>.

Each Employee has the following properties:

int Id

string Name

string Department

double Salary
 Functional Requirements
Design a menu that repeatedly prompts the user to choose one of the following actions:


===== Employee Management Menu =====
1. Add New Employee
2. View All Employees
3. Search Employee by ID
4. Update Employee Details
5. Delete Employee
6. Exit
====================================
Enter your choice:

 Task:
Write a C# program using:

A class Employee with the above properties.

A List<Employee> to hold all employee records.

A menu-based loop to allow the user to perform the following:

✅ Expected Functionalities (CRUD)

1.Prompt the user to enter details for a new employee and add it to the list.


2.Display all employees 

3.Allow searching an employee by Id and display their details.

4.Search for an employee by Id, and if found, allow the user to update name, department, or salary.

5.Search for an employee by Id and remove the employee from the list.

6.Cleanly exit the program.

Use Exception handling Mechanism

 */
namespace Assignment4
{
    public class EmployeeDataNotFound : ApplicationException
    {
        public EmployeeDataNotFound(string message) : base(message)
        {

        }

    }
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public double Salary { get; set; }
        public List<Employee> empList = new List<Employee>();

        // adding the employee details in list

        internal void AddEmployee(Employee emp)
        {


            Console.WriteLine("Enter the employee Id :");
            emp.Id = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the employee Name :");
            emp.Name = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter the Department :");
            emp.Department = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter the  salary :");
            emp.Salary = Convert.ToDouble(Console.ReadLine());


            empList.Add(emp);

        }




        //display the employee details
        public void DisplayEmployee()
        {
            foreach (var i in empList)
            {
                Console.WriteLine("Employee Id {0} Employee Name {1} Employee Department {2} Employee Salary {3} ", i.Id, i.Name, i.Department, i.Salary);
            }
        }

        //display employee by searching employee Id
        internal List<Employee> GetEmployeeById(Employee emp)
        {


            Console.WriteLine("Enter Employee Id to search");
            int ID = Convert.ToInt32(Console.ReadLine());


            foreach (var item in empList)
            {
                if (item.Id == ID)
                {

                    return empList;


                }


            }



        }



        internal List<Employee> UpdateEmployee(Employee emp)
        {
            Console.WriteLine("Enter Employee Id to search");
            int ID = Convert.ToInt32(Console.ReadLine());
            IEnumerable<Employee> p= emp.empList;
            var item=p.Count();


            for (var i=0; i<item; i++)
            {
                if (Id == ID)
                {
                    Console.WriteLine("Enter the updated Name :");
                    emp.Name = Convert.ToString(Console.ReadLine());
                    Console.WriteLine("Enter the updated Department :");
                    emp.Department = Convert.ToString(Console.ReadLine());
                    Console.WriteLine("Enter the updated salary :");
                    emp.Salary = Convert.ToDouble(Console.ReadLine());

                  
                }
                empList.Add(emp);



            }

            Console.WriteLine("Updated details of employee");

            return empList;

        }


        internal List<Employee> DeleteEmployee()
        {
            Console.WriteLine("Enter the Employee Id ");
            int ID = Convert.ToInt32(Console.ReadLine());
            foreach (var item in empList)

            {
                if (item.Id == ID)
                {
                    empList.Remove(item);
                    Console.WriteLine("Employee Details Deleted");
                }


            }

            return empList;
        }





    }

    public class Assignmen4Q1
    {
        static void Main(string[] args)
        {

            Employee employee = new Employee();
            List<Employee> emp = new List<Employee>();

            int choice = 0;

            do
            {
                Console.WriteLine("Employee Engagement Menu");
                Console.WriteLine("1. Add New Employee");
                Console.WriteLine("2. View All Employees");
                Console.WriteLine("3. Search Employee by ID");
                Console.WriteLine("4. Update Employee Details");
                Console.WriteLine("5. Delete Employee");
                Console.WriteLine("6. Exit");


                Console.WriteLine("Enter your choice :");

                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        employee.AddEmployee(employee);

                        Console.WriteLine("Employee Details Added Successfully");
                        break;
                    case 2:

                        employee.DisplayEmployee();

                        break;

                    case 3:
                        employee.GetEmployeeById(employee);
                        break;
                    case 4:
                        employee.UpdateEmployee(employee);
                        break;
                    case 5:
                        employee.DeleteEmployee();
                        break;
                    case 6:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid Choice");
                        break;
                }
            } while (choice != 0);
            Console.Read();
        }


    }

}