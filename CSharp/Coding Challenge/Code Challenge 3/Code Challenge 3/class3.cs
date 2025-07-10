using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
/*
 * 3. Write a program in C# Sharp to append some text to an existing file.
 * If file is not available, then create one in the same workspace.
Hint: (Use the appropriate mode of operation. Use stream writer class)
 
 */
namespace Code_Challenge_3
{
    class FileEg
    {
        public void file1()
        {
            string fileloctaion= @"C:\Infinite Trainining 2025\CSharp\Coding Challenge\Code Challenge 3\FileHandling\test.txt";
            if (File.Exists(fileloctaion))
            {
                StreamWriter write = File.AppendText(fileloctaion );
                Console.WriteLine("Successfully appended existing file");

            }
            else
            {
                Stream stream = new FileStream(fileloctaion, FileMode.Create);
                StreamWriter writer = File.AppendText(fileloctaion);
                Console.WriteLine("Successfully created new file and appended");
            }
            
        }
    }
    class class3
    {
        static void Main()
        {
            FileEg file2 = new FileEg();
            file2.file1();

            Console.Read();
        }
    }
}
