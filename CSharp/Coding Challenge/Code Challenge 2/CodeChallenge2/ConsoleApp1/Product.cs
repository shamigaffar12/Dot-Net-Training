using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* Code Challenge: 2

Time: 1 hr 15 mts

1.Create an Abstract class Student with Name, StudentId, Grade as members and also an abstract method Boolean Ispassed(grade) which takes grade as an input and checks whether student passed the course or not.  

Create 2 Sub classes Undergraduate and Graduate that inherits all members of the student and overrides Ispassed(grade) method

For the UnderGrad class, if the grade is above 70.0, then isPassed returns true, otherwise it returns false. For the Grad class, if the grade is above 80.0, then isPassed returns true, otherwise returns false.

Test the above by creating appropriate objects


2. Create a Class called Products with Productid, Product Name, Price. Accept 10 Products, sort them based on the price, and display the sorted Products

3. Write a C# program to implement a method that takes an integer as input and throws an exception if the number is negative. Handle the exception in the calling code.
*/

namespace ConsoleApp1


{
    public abstract class Student
    {
       public static string Name;
       public static string StudentId;
       public static double Grade;

        public Student(string Name, string StudentId, double Grade)
        {
            Name = Name;
            StudentId = StudentId;
            Grade = Grade;


        }

        public abstract bool IsPassed(double Grade);


    }
    class Undergraduate : Student

    {
        public Undergraduate(string Name, string StudentId, double Grade) : base(Name, StudentId, Grade)
        {

        }

        public override bool IsPassed(double Grade)

        {
            if (Grade > 70)
            {
                return true;

            }
            else
            {
                return false;
            }

        }
    }

    class Graduate : Student
    {
        public Graduate(string Name, string StudentId, double Grade) : base(Name, StudentId, Grade)
        {

        }

        public override bool IsPassed(double Grade)
        {
            if (Grade > 80)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    class Students
    {

        static void Main(string[] args)
        {
            int choice;

            do
            {
                Console.WriteLine("--- Student Grade Checker ---");
                Console.WriteLine("1. Add Undergraduate Student and Display Result");
                Console.WriteLine("2. Add Graduate Student and Display Result");
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice: ");
                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:


                        AddStudent();

                        Student underGraduateStud = new Undergraduate(Student.Name, Student.StudentId, Student.Grade);
                        Console.WriteLine("Under Graduate Student Added Succeesfully");
                        var result = underGraduateStud.IsPassed(Student.Grade);
                        Console.WriteLine($"Student  is Passed :  {result}");

                        break;
                    case 2:
                        AddStudent();
                        Student GraduateStudent = new Graduate(Student.Name, Student.StudentId, Student.Grade);
                        Console.WriteLine("Graduate Student Added Successfully ");
                        var status = GraduateStudent.IsPassed(Student.Grade);
                        Console.WriteLine($"Student  is Passed :  {status}");
                        break;
                    case 3:
                        System.Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid Choice ! ");
                        break;


                }
                Console.WriteLine("********************************");
            } while (choice != 4);

            Console.Read();

            
            }
        public static void AddStudent()
        {
           

            Console.WriteLine("Enter the student name :");
            Student.Name = Convert.ToString(Console.ReadLine());

            Console.WriteLine("Enter the studen ID :");
            Student.StudentId = Convert.ToString(Console.ReadLine());

            Console.WriteLine("Enter the Grade :");
            Student.Grade = Convert.ToDouble(Console.ReadLine());



             
        }


    }


}      
   









