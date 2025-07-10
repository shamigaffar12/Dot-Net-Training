using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

/*
 * 2. Write a program in C# Sharp to create a file and write an array of strings to the file.

3. Write a program in C# Sharp to count the number of lines in a file.
 */

namespace Assignment_6
{
    [Serializable]
    class ArrayFileHandling
    {
        public static int size;
        public string[] arr = new string[size];

            


    }
        class Program
        {
            static void Main(string[] args)
            {
              
                Console.WriteLine("Enter the size of Array :");
                ArrayFileHandling.size = Convert.ToInt32(Console.ReadLine());
            ArrayFileHandling file1 = new ArrayFileHandling();

            for (int i = 0; i < file1.arr.Length; i++)
                {
                    Console.WriteLine("Enter the element of data : ");
                    file1.arr[i] = Console.ReadLine();
                }

                //writing into the file
                IFormatter formatter = new BinaryFormatter();

                Stream stream = new FileStream(@" C:\Infinite Trainining 2025\CSharp\Assignment\Assignment 6\Assignment 6\FileHandling\ArrayEx.txt",
              FileMode.Create, FileAccess.Write);
                formatter.Serialize(stream, file1);
                stream.Close();

                Console.WriteLine("Reading from file");

                 stream = new FileStream(@" C:\Infinite Trainining 2025\CSharp\Assignment\Assignment 6\Assignment 6\FileHandling\ArrayEx.txt",
              FileMode.Open, FileAccess.Read);

                ArrayFileHandling dfile = (ArrayFileHandling)formatter.Deserialize(stream);
            Console.WriteLine("       ");
            Console.Write("Array  Elements are :   ");
            foreach (var item in file1.arr)
            {
                Console.Write(item+" , "); 
            }

                Console.Read();
            }
        }
    }

