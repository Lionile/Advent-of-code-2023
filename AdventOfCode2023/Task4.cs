using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Task4
    {
        const string filename = "../../../test files/task4.txt";

        const string pattern = @"\d+";

        public static void RunPart1()
        {
            int sum = 0;
            string[] lines = File.ReadAllLines(filename);

            foreach (string line in lines)
            {
                int winningNums = GetNumOfWinningCards(line);
                if (winningNums > 0)
                    sum += (int)Math.Pow(2, winningNums - 1);
            }

            Console.WriteLine(sum);
        }


        public static void RunPart2()
        {
            int sum = 0;
            string[] lines = File.ReadAllLines(filename);
            int cardCount = lines.Length;
            int[] numOfWins = new int[cardCount]; // number of winning cards for each card, index + 1 = card number
            int[] finalCardCount = new int[cardCount]; // holds the final number of cards after calculating everything

            for(int i = 0; i < cardCount; i++)
            {
                finalCardCount[i]++; // we start with one of each card
            }

            for(int i = 0; i < cardCount; i++)
            {
                numOfWins[i] = GetNumOfWinningCards(lines[i]);
            }

            for(int i = 0; i < cardCount; i++)
            {
                // for each subsequent (i + j) card add as many cards as there are of the one you are checking (i)
                for(int j = 1; j <= numOfWins[i]; j++)
                {
                    finalCardCount[i + j] += finalCardCount[i];
                }
            }

            for(int i = 0; i < numOfWins.Length; i++)
            {
                sum += finalCardCount[i];
            }

            Console.WriteLine(sum);
        }



        private static int GetNumOfWinningCards(string line)
        {
            string[] parts = line.Split("|");
            MatchCollection firstMatches = Regex.Matches(parts[0], pattern);
            MatchCollection secondMatches = Regex.Matches(parts[1], pattern);

            string[] first = new string[firstMatches.Count]; // values before the '|' symbol (winning numbers)
            for (int i = 0; i < firstMatches.Count; i++)
            {
                first[i] = firstMatches[i].ToString();
            }

            string[] second = new string[secondMatches.Count]; // values after the '|' symbol (card numbers)
            for (int i = 0; i < secondMatches.Count; i++)
            {
                second[i] = secondMatches[i].ToString();
            }

            first = first.Skip(1).ToArray(); // remove the first found value (the card number)

            int winningNums = 0;
            foreach (string value in second)
            {
                if (first.Contains(value))
                    winningNums++;
            }

            return winningNums;
        }
    }
}
