using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*Code Challenge 3
 * 1. Write a program to find the Sum and the Average points scored
 * by the teams in the IPL. Create a Class called CricketTeam that 
 * has a function called Pointscalculation(int no_of_matches) that 
 * takes no.of matches as input and accepts that many scores from the user.
 * The function should then return the Count of Matches, Average and Sum of the scores.
 */

namespace Code_Challenge_3
{
    class CricketTeam {
        public int no_of_matches;
        public int PointsCalculation(int no_of_matches)

        {
            this.no_of_matches = no_of_matches;
           

            var score = new List <int> (no_of_matches);
            foreach (var item in score)
            {
                score.Add(item);
            }
            var length = score.Count();
             double average = score.Average();
            var sumofscore = score.Sum();

            //return length,sumofscore,average;
            return 0;
            
        }
    }
    class Program
    {
        static void Main(string[] args)
        {int no_of_matches;
            CricketTeam team1 = new CricketTeam();
            Console.WriteLine("Enter the number of matches :");
            no_of_matches = Convert.ToInt32(Console.ReadLine());

            team1.PointsCalculation(no_of_matches);
                Console.Read();
        }
    }
}
