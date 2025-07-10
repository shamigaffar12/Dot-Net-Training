using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * Write a query that returns words starting with letter 'a' and ending with letter 'm'.


Expected input and output
"mum", "amsterdam", "bloom" → "amsterdam"
 */

namespace Asiignment_7
{
    class  FindWords
    {
      
        public static string input;
        public static List<string> wordlist = new List<string>(input.Split(',', (char)StringSplitOptions.RemoveEmptyEntries));
        public static List<string> GetList()
        {
            
            var value = new List<string>(input.Split(',' ,  (char)StringSplitOptions.RemoveEmptyEntries));
           
            foreach(var item in value)
            {

                wordlist.Add(item);
              
            }
            return wordlist;

        }
        public List<string > FindWord()
        {
           
             IEnumerable<string> finds = from name in GetList()
                                        where name.StartsWith("a") && name.EndsWith("m")
                                        select name;
          
            var result = new List<string>();
            
            foreach (var item in finds)
            {
                result.Add(string.Join("  -> ",item));
            }
            return result;
        }

    }
    class Assignment7Q2
    {
        static void Main()
        {
            FindWords letter = new FindWords();
            //FindWords.GetList();
            Console.WriteLine("Enter element in list separated by comma :");
            FindWords.input = Convert.ToString(Console.ReadLine());
            FindWords.GetList();
           var result=letter.FindWord();
            foreach(var item in result)
            {
                Console.WriteLine(item);
            }
            
            
           
            Console.Read();
        }
    }
}
