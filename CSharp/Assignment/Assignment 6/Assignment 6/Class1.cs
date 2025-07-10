using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
/*
3.Write a program in C# Sharp to count the number of lines in a file.
 */
namespace Assignment_6
{
    class ReadLineFile
    {
       public int fileLinesCount;
    }
    class Class1
    {
        static void Main()
        {
            ReadLineFile count = new ReadLineFile();
            string fileLocation= @" C:\Infinite Trainining 2025\CSharp\Assignment\Assignment 6\Assignment 6\FileHandling\ArrayEx.txt";

            if (File.Exists (fileLocation)){
                count.fileLinesCount = File.ReadAllLines(fileLocation).Length;
                Console.WriteLine("The given file has " + count.fileLinesCount + " lines. ");

            }
            else
            {
                Console.WriteLine("File does not exists!");
            }
            Console.Read();

        }
    }
}
