using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Grad : Student
    {
        public Grad(string Name, string StudentId, string Grade) : base(Name, StudentId, Grade)
        {

        }

        static void Main(string[] args)
        {
            string Name, StudentID; string Grade;
            Console.WriteLine("Enter the student name :");
            Name = Convert.ToString(Console.ReadLine());

            Console.WriteLine("Enter the studen ID :");
            StudentID = Convert.ToString(Console.ReadLine());

            Console.WriteLine("Enter the Grade :");
            Grade = Convert.ToString(Console.ReadLine());


            Grad student = new Grad(Name, StudentID, Grade);
            student.IsPassed(Grade);

            Console.Read();
        }

        public override bool IsPassed(string Grade)
        {

            int result = Convert.ToInt32(Grade);
            if (result >= 80)
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
