using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Create a class called student which has data members like rollno, name, class, Semester, branch, int[] marks = new int marks [5](marks of 5 subjects )

//-Pass the details of student like rollno, name, class, SEM, branch in constructor

//- For marks write a method called GetMarks() and give marks for all 5 subjects

//-Write a method called displayresult, which should calculate the average marks

//-If marks of any one subject is less than 35 print result as failed
//-If marks of all subject is >35, but average is < 50 then also print result as failed
//-If avg > 50 then print result as passed.

//-Write a DisplayData() method to display all object members values.
namespace Assignment3
{
    class Student
    {
        string RollNO;
        string name;
        int studentClass;
        int SEM;
        string branch;
        int[] marks = new int [5];

        public Student(string rollNo, string sName, int stdClass, int sem, string stdBranch)

        {
            this.RollNO = rollNo;
            this.name = sName;
            this.studentClass = stdClass;
            this.SEM = sem;
            this.branch = stdBranch;

        }
        public void GetMarks()
        {
            Console.WriteLine("----Enter the marks of the students-----");
            for (int i = 0; i < marks.Length; i++)
            {
                Console.Write("Enter the maeks of subject" + (i + 1) + " : ");
                marks[i] = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("  ");

            }
        }
        public void displayresult()
        {
            int average;
            int totalmarks;
            totalmarks = Convert.ToInt32(marks.Sum());
            average = Convert.ToInt32(marks.Average());
            bool result=false;

            foreach (int i in marks)
            {
                if (i < 35 || totalmarks > 35 && average < 50)
                {
                    result = true;
                }

            }
            if (result = true)
            {
                Console.WriteLine("Result : failed ");
            }
            else
            {
                Console.WriteLine("Result : passed ");
            }
        }
                public void DisplayData()
        {
            Console.WriteLine("---- Student Details -----");
            Console.WriteLine("Roll Number  of the student : " + RollNO);
            Console.WriteLine("Name of the student : "+name);
            Console.WriteLine("Class of the student : " + studentClass);
            Console.WriteLine("Semester of the student : " + SEM);
            Console.WriteLine("Branch of the student : " + branch);
            
            Console.WriteLine("----Student's Result----");
            int x = 0;
            foreach (int i in marks)
            {
             
                x++;
                Console.WriteLine("Marks of subject"+x+" = "+i);
            }
          
            Console.WriteLine("Total marks : "+marks.Sum());
            Console.WriteLine("Average marks : " + marks.Average());
           


        }

        }

    
    class Assignment3Q2
    {
        public static void Main(string [] args)
        {
            string RollNO;
            string name;
            int studentClass;
            int SEM;
            string branch;
            Console.WriteLine("Enter the Roll Number of student : ");
            RollNO=Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter the name of student : ");
            name = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter the class of student : ");
            studentClass= Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the semester of student : ");
            SEM = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the branch of student : ");
            branch = Convert.ToString(Console.ReadLine());

            Student student = new Student(RollNO,name,studentClass,SEM,branch);
            student.GetMarks();
            student.DisplayData();
            student.displayresult();
            Console.Read();

        }
    }
}
