using System;
using System.Linq;
/*Arrays
1.Write a  Program to assign integer values to an array  and then print the following
	a.	Average value of Array elements
	b.	Minimum and Maximum value in an array */

namespace Assignment2
{
    class Asssignment2ArraysQ1
    {
        public static void Main(string[] args)
        {
            ArraySol.ArrayFun(); // callling input finction to take integer value by user in array
            ArraySol.Avg(); // calculte the average of array element
            ArraySol.MinMax(); //calculate the minimum and maximum value present in array element
            Console.Read();

        }
    }
    class ArraySol
    {
        
        static int size; //declare size variable for size of array
        static int[] arr; // creating an array (arr) of int type

        //function for taking array's element in it and display it
        public static void ArrayFun()

        {
            
            Console.Write("Enter the size of array : ");

            size = Convert.ToInt32(Console.ReadLine()); //user will enter the size  of array
            arr = new int[size]; // creates the size of array passed by user


            Console.WriteLine("Enter the elements of array :"); //putting the elements in array
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = Convert.ToInt32(Console.ReadLine()); //reads  the elements of array by user one by one

            }
            Console.WriteLine("Elments of array : "); //display the elements of array

            for (int j=0; j<arr.Length;j++)
            {
                Console.WriteLine(" "+arr[j]); //prints array elements
            }
            

        }
    
        
        //function to calculate average of array elements and display it
  public static void Avg()
        {
            Console.WriteLine("Average of the array elements : "+arr.Average());

            }
        //function to find minimum and maximu element of array and display it
        public static void MinMax()
        {
            
            Console.WriteLine("Minium value of the array  is " + arr.Min()); //find the minimum element of array and display it
            Console.WriteLine("Maximum vale of the array is "+arr.Max()); // find the maximum element of array and display it 
        }
       

    }
} 
