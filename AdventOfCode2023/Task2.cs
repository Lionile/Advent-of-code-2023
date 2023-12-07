using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AdventOfCode2023
{
    internal class Task2
    {
        const string filename = "../../../test files/task2.txt";

        const string pattern = @"(\d+) (red|green|blue)";



        public static void RunPart1() // regex solution
        {
            int maxRed = 12, maxGreen = 13, maxBlue = 14;

            string[] lines;
            int sum = 0;
            bool possible = true;

            lines = File.ReadAllLines(filename);
            for (int i = 0; i < lines.Length; i++)
            {
                possible = true;
                MatchCollection matches = Regex.Matches(lines[i], pattern);


                foreach (Match match in matches)
                {
                    GroupCollection data = match.Groups;
                    if (data[2].ToString() == "red" && Int32.Parse(data[1].ToString()) > maxRed)
                    {
                        possible = false;
                        break;
                    }
                    else if (data[2].ToString() == "green" && Int32.Parse(data[1].ToString()) > maxGreen)
                    {
                        possible = false;
                        break;
                    }
                    else if (data[2].ToString() == "blue" && Int32.Parse(data[1].ToString()) > maxBlue)
                    {
                        possible = false;
                        break;
                    }
                }
                if (possible)
                    sum += i + 1;
            }

            Console.WriteLine("Sum of IDs: " + sum);
        }



        public static void RunPart2() // regex solution
        {
            string[] lines;
            int sum = 0;

            lines = File.ReadAllLines(filename);
            for (int i = 0; i < lines.Length; i++)
            {
                int leastRed = 0, leastGreen = 0, leastBlue = 0;
                MatchCollection matches = Regex.Matches(lines[i], pattern);


                foreach (Match match in matches)
                {
                    GroupCollection data = match.Groups;
                    string color = data[2].ToString();
                    int count = Int32.Parse(data[1].ToString());
                    if (color == "red" && count > leastRed)
                    {
                        leastRed = count;
                    }
                    else if (color == "green" && count > leastGreen)
                    {
                        leastGreen = count;
                    }
                    else if (color == "blue" && count > leastBlue)
                    {
                        leastBlue = count;
                    }
                }

                sum += leastRed * leastGreen * leastBlue;
            }

            Console.WriteLine("Sum of IDs: " + sum);
        }

    }
}
