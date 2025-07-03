using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{

    class UndGraduate : Student
    {
        public UndGraduate(string Name, string StudentId, string Grade) : base(Name, StudentId, Grade)
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


            UndGraduate student = new UndGraduate(Name, StudentID, Grade);
            student.IsPassed(Grade);


            Console.Read();
        }

        public override bool IsPassed(string Grade)
        {

            int result = Convert.ToInt32(Grade);
            if (result >= 70)
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

