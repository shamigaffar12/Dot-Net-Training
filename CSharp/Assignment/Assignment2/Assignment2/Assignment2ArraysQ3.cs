using System;
//3.Write a C# Sharp program to copy the elements of one array into another array.(do not use any inbuilt functions)

namespace Assignment2
{
    class Assignment2ArraysQ3
    {
        public static void Main(string [] args)
        {
            Array_Copy.ArrayFun(); //calling the function that takes array size and its element from user
            Array_Copy.arr_Copy();//function to copy the original array
            Console.Read();

        }
    }
    class Array_Copy
    {

        static int size; //variable for size of array declaration
        static int[] arr; // original array declaration

        //function to take size of array and elements of array by user
        public static void ArrayFun()

        {
            Console.Write("Enter the size of array : ");
            size = Convert.ToInt32(Console.ReadLine());// taking the size of array by user
            arr = new int[size]; //initializing size of array
            Console.WriteLine("Enter the elements of array :");
            //enter the elements in array
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = Convert.ToInt32(Console.ReadLine());

            }
            Console.Write("Original Array Data : ");

            for (int j = 0; j < arr.Length; j++)
            {
                Console.Write(" " + arr[j]); // prints original array elements
            }
            Console.WriteLine(" ");

        }
        //function for copying the original araay elements in copy array 
        public static void arr_Copy()

        {

            int[] arr_copy=new int[size]; //creating a copy array of same size as of original array
            
            Console.Write("Copy Array Data : ");
            for(int i = 0; i < arr.Length; i++)
            {
                Console.Write(" "+(arr_copy[i] = arr [i])); //elments of original array is copied to other arrray one by one
              

            }
        }
    }


      
    }

