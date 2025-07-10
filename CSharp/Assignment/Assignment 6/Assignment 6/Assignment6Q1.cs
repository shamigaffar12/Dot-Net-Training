using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_6
{
    class Books
    {
        public string bookname { get; set; }
        public string authorname { get; set; }


        public Books(string bookname, string authorname)
        {
            this.bookname = bookname;
            this.authorname = authorname;

        }
        public void Display()
        {
            Console.WriteLine("Book Name {0} , Author Name  {1}", bookname, authorname);
        }


    }
    class BookDetails
    {
        public static int size;
        public Books[] shelf = new Books[size];

        public Books this[int index]
        {
            get { return shelf[index]; }
            set { shelf[index] = value; }
        }

        public void Display()
        {
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine("Book " + (i + 1) + " Details ");
                shelf[i].Display();
            }
        }


        class BookShelf
        {

            static void Main()
            {
                Console.WriteLine("-----Book Details-----");
                Console.WriteLine("Enter the number of books to add");
                size = Convert.ToInt32(Console.ReadLine());
                BookDetails bk1 = new BookDetails();

                string bkname, authname;
                for (int i = 0; i < size; i++)
                {
                    Console.WriteLine("Enter the book " + (i + 1) + "  name :  ");
                    bkname = Convert.ToString(Console.ReadLine());
                    Console.WriteLine("Enter the Author " + (i + 1) + "  name :  ");
                    authname = Convert.ToString(Console.ReadLine());
                    BookShelf book = new BookShelf();
                    bk1.shelf[i] = new Books(bkname, authname);



                }
                Console.WriteLine("*******************************");
                bk1.Display();

                Console.Read();
            }

        }
    }
}