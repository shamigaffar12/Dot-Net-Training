using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * 2. Create a class called Scholarship which has a function Public void Merit() that takes marks and fees as an input. 
If the given mark is >= 70 and <=80, then calculate scholarship amount as 20% of the fees
If the given mark is > 80 and <=90, then calculate scholarship amount as 30% of the fees
If the given mark is >90, then calculate scholarship amount as 50% of the fees.
In all the cases return the Scholarship amount, else throw an user exception
 */


namespace Assignment5
{
    class Scholarship :ApplicationException
    {
       public  string sname;
        public  int marks;
        public double fees;
        public double scholarshipamount;

        public Scholarship(string sname,int marks,double fees)
        {
            this.sname = sname;
            this.marks = marks;
            this.fees = fees;

        }
        public double calculateScholarship(int marks, double fees)
        {
            this.marks = marks;
            this.fees = fees;

            scholarshipamount = fees - (fees * marks) / 100;
            Console.WriteLine("Scolarship Amount: ");
            return scholarshipamount;

        }
        public void Merit(int marks, double fees)
        {
            this.marks = marks;
            this.fees = fees;

            if (marks > 90)
            {
                calculateScholarship(marks, fees);

            }
            else if (marks > 80 && marks <= 90)
            {
                calculateScholarship(marks, fees);

            }
            else if (marks >= 70 && marks <= 80)
            {
                calculateScholarship(marks, fees);
            }
            else
            {
                throw new InvalidInput("Please enter correct marks");

            }

        }

        }
        class Assignment5Q2 : Exception
        {
            static void Main()

            {
                string Name; 
                int marks ;
                double fees;
                Console.WriteLine("Enter the student name");
                Name = Convert.ToString(Console.ReadLine());
                Console.WriteLine("Enter the student marks");
                marks = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the student fees");
                fees = Convert.ToDouble(Console.ReadLine());

                Scholarship scholarship = new Scholarship(Name,marks,fees);
                try
                {
                    scholarship.Merit(marks, fees);
                }
                catch(InvalidInput ex)
                {
                    Console.WriteLine(ex.Message);
                }
            Console.Read();
            }
        }
        
       
        
    
}
