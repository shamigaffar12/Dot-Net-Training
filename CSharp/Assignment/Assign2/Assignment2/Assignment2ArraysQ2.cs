using System;
using System.Linq;
/*
 Write a program in C# to accept ten marks and display the following
	a.	Total
	b.	Average
	c.	Minimum marks
	d.	Maximum marks
	e.	Display marks in ascending order
	f.	Display marks in descending order
 */

namespace Assignment2
{
    class Class4
    {
        public static void Main(string[] args)
        {
            Marks.Input(); // function to enter the marks by user
            Marks.Sum();// display total marks
            Marks.Avg(); // display average of marks
            Marks.Min(); // display minimum mark
            Marks.Max(); // display maximum mark
            Marks.DisplayAsc(); // diplay the marks in sorted order as ascending order
            Marks.DisplayDsc(); //sorting the marks in descending order
            Console.WriteLine(" Original Array Data : ");
            Marks.OrgData(); //display the marks input by user without any changes in order of elements
            Console.Read();

        }
    }
    class Marks
    {

        static int[] arr = new int[10]; //creating array to store marks in it 

        static int[] dArray = new int[10]; //creating duplicate array to copy the marks in it
                                           //& all desired actions will be done on this array
        //function to take input of marks from user
        public static void Input()

        {
            Console.WriteLine("Enter marks :");
            for (int i = 0; i < arr.Length; i++)
            {
               arr[i] = Convert.ToInt32(Console.ReadLine()); // Raeds the marks of students in original array(arr) one by one

            }
            Console.WriteLine(" ");
            Console.Write("Marks are : ");
            OrgData(); // prints original array elements as it is.
            Array.Copy(arr, dArray, 10); // copying the marks in duplicate Array (dArray)
            Console.WriteLine(" ");


        }
        //function to display original array element
        public static void OrgData()
        {
           
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(" " + arr[i]);
            }
        }

        //function to display the marks of dArray
        public static void display()
        {
            
            for (int i = 0; i <dArray.Length; i++)
            {
                Console.Write(" " + dArray[i]);
            }
        }

     //function to calculate total marks
        public static void Sum()

        {
            
            Console.WriteLine(" ");
            Console.WriteLine("Total Marks : " + dArray.Sum()); //calculate total marks & display it
        }
        //function to calculate average of marks
        public static void Avg()
        {
            
            
            Console.WriteLine("Average Marks : " + dArray.Average()); //calculate average of marks & display it
        }

        //function to find minimum  marks
        public static void Min()
        {
           
            Console.WriteLine("Minimum Marks : " + dArray.Min()); //find minimum of mark and display it
        }

        //function to find mmaximum  mark
        public static void Max()
        {
            
            Console.WriteLine("Maximum Marks : " + dArray.Max()); // find maximum of marks and display it
        }


        //function to arrange the marks in ascending order
        public static void DisplayAsc()
        {
            Array.Sort(dArray); //sort the array in ascending order
            Console.WriteLine(" ");
            Console.Write("Marks in Ascending Order : ");
            Marks.display(); //display the marks in ascending order


        }

        //function to araaange marks as descending order
        public static void DisplayDsc()
        {
           
            Array.Reverse(dArray); // reversing the sorted array to arrange as descending order
            Console.WriteLine(" ");
            Console.Write("Marks in Descending Order : ");
            Marks.display(); // display marks in descending order
            Console.WriteLine(" ");

           

        }
       
    }
}
