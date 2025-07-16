using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*Create a Generic List Collection empList and populate it with the following records.
 */

namespace CSharpCC
{
    public class Employee

    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DOJ { get; set; }

        public string City { get; set; }
        public static List<Employee> empList = new List<Employee>();
        public static Employee AddEmployee()
        {
            int empId;
            string fName, lName, empTitle, empCity;
            DateTime empDOB, empDOJ;



            Console.WriteLine("Enter Employee Id:");
            empId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Employee First Name:");
            fName = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter Employee Last Name:");
            lName = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter Employee Title:");
            empTitle = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter Employee Date of Birth:");
            empDOB = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Enter Employee Date of Joining:");
            empDOJ = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Enter Employee City:");
            empCity = Convert.ToString(Console.ReadLine());

            return new Employee

            {
                EmployeeId = empId,
                FirstName = fName,
                LastName = lName,
                Title = empTitle,
                DOB = empDOB,
                DOJ = empDOJ,
                City = empCity,


            };

        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employee = new List<Employee>();
            int choice = 0;
            do
            {

                Console.WriteLine("------Employee Details-------");
                Console.WriteLine("1.Add new emplooye");
                Console.WriteLine("2.Display all emplooye");
                Console.WriteLine("3.Display  emplooye (Not in mumbai");
                Console.WriteLine("4.Display emplooye (Title is AsstManager)");
                Console.WriteLine("5.Display emplooye (Last Name start with s)");
                Console.WriteLine("6.Exit");
                Console.WriteLine("Enter your choice : ");
                choice = Convert.ToInt32(Console.ReadLine());


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
                        // display employee whose location is not mumbai
                        var emp = employee.Where(i => i.City != "Mumbai");
                Console.WriteLine(" List of Employees not in Mumbai :");
                DisplayEmployee(emp);
                break;

                    case 4:

                        // display employee whose title is AsstManager
                        var eTitle = employee.Where(i => i.Title == "AsstManager");
                Console.WriteLine("List of Asst Manager Employee:");
                DisplayEmployee(eTitle);
                break;
                    case 5:

                        //display employee list lastName starts with s
                        var result = employee.Where(i => i.LastName.StartsWith("S"));
                Console.WriteLine(" Employee List whose  lastName starts with S :  :");
                DisplayEmployee(result);
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


        } while (choice != 6);

            Console.Read();
            }

    //display method
    static void DisplayEmployee(IEnumerable<Employee> employee)
    {

        foreach (var item in employee)
        {
            Console.WriteLine($"EmployeeID: {item.EmployeeId}, FirstName: {item.FirstName},LastName: {item.LastName},Title: {item.Title}, DOB:{item.DOB} , DOJ : {item.DOJ} , City { item.City}");
        }


    }
}

}



    


