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
    abstract class Student
    {
        string Name;
        string StudentId;
        string Grade;

        public Student(string Name, string StudentId, string Grade)
        {
            this.Name = Name;
            this.StudentId = StudentId;
            this.Grade = Grade;


        }

        public abstract bool IsPassed(string Grade);


    }
    class Program : Student
    {
        public Program(string Name, string StudentId, string Grade) : base(Name, StudentId, Grade)
        {

        }

        static void Main(string[] args)
        {
            string Name, StudentID, Grade;
            Console.WriteLine("Enter the student name :");
            Name = Convert.ToString(Console.ReadLine());

            Console.WriteLine("Enter the studen ID :");
            StudentID = Convert.ToString(Console.ReadLine());

            Console.WriteLine("Enter the Grade :");
            Grade = Convert.ToString(Console.ReadLine());


            Program student = new Program(Name, StudentID, Grade);
            student.IsPassed(Grade);
            Console.Read();
        }





        public override bool IsPassed(string Grade)
        {

            if (Grade != "E")
            {
                Console.WriteLine("Student is passed");
            }
            else
            {
                Console.WriteLine("Student is failed");
            }
            return true;

        }
    }
}


    

   









