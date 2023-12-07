using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Task3
    {
        const string filename = "../../../test files/task3.txt";

        const string pattern = @"\d+";

        readonly static char[] symbols = { '*', '#', '&', '+', '$', '@', '/', '%', '-', '='};

        static Dictionary<Tuple<int,int>, List<int>> symbolNumberPairs = new Dictionary<Tuple<int,int>, List<int>>();

        public static void RunPart1()
        {
            string[] lines = File.ReadAllLines(filename);
            int sum = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                MatchCollection matches = Regex.Matches(lines[i], pattern);


                foreach (Match match in matches)
                {
                    GroupCollection data = match.Groups;
                    //Console.WriteLine($"[{i},{match.Index}]({match.Length}): " + Int32.Parse(data[0].ToString()));
                    if (IsNumberValid(lines, i, match.Index, match.Length))
                    {
                        int num = Int32.Parse(data[0].ToString());
                        //Console.WriteLine($"Line {i}: " + num);
                        sum += num;
                    }
                }

            }

            Console.WriteLine("Total sum: " + sum);
        }



        public static void RunPart2()
        {
            string[] lines = File.ReadAllLines(filename);
            int sum = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                MatchCollection matches = Regex.Matches(lines[i], pattern);


                foreach (Match match in matches)
                {
                    GroupCollection data = match.Groups;
                    //Console.WriteLine($"[{i},{match.Index}]({match.Length}): " + Int32.Parse(data[0].ToString()));

                    List<Tuple<int, int>> symbolLocations = new List<Tuple<int, int>>();

                    if (IsNumberValid(lines, i, match.Index, match.Length, symbolLocations))
                    {
                        int num = Int32.Parse(data[0].ToString());
                        foreach(Tuple<int, int> symbol in symbolLocations)
                        {
                            if (!symbolNumberPairs.ContainsKey(symbol))
                                symbolNumberPairs[symbol] = new List<int>();

                            symbolNumberPairs[symbol].Add(num);
                        }
                        //Console.WriteLine($"Line {i}: " + num);
                    }
                }

            }

            foreach(List<int> pair in symbolNumberPairs.Values)
            {
                if(pair.Count == 2)
                    sum += pair[0] * pair[1];
            }

            Console.WriteLine("Total sum: " + sum);
        }



        public static bool IsNumberValid(string[] lines, int row, int column, int length)
        {
            // for each character in the number, check it's surroundings for a symbol
            for (int i = column; i < column + length; i++)
            {
                //Console.WriteLine($"-----DIGIT: {lines[row][i]} ({row},{i})-----");
                bool tempResult = IsDigitValid(lines, row, i);
                if (tempResult) return true;
            }

            return false;
        }



        public static bool IsDigitValid(string[] lines, int row, int column)
        {
            // check the digit surroundings, make sure it's not in a corner and goes out of bounds
            int iEnd = row == lines.Length - 1 ? row : row + 1;
            int jEnd = column == lines[row].Length - 1 ? column : column + 1;

            for (int i = row == 0 ? 0 : row - 1; i <= iEnd; i++)
            {
                for (int j = column == 0 ? 0 : column - 1; j <= jEnd; j++)
                {
                    if (!(i == row && j == column))
                    {
                        //Console.WriteLine($"[{i},{j}]: {symbols.Contains(lines[i][j])}");
                        if (symbols.Contains(lines[i][j]))
                        {
                            return true;
                        }
                    }

                }
            }

            return false;
        }



        public static bool IsNumberValid(string[] lines, int row, int column, int length, List<Tuple<int, int>> symbolLocations)
        {
            // for each character in the number, check it's surroundings for a symbol
            for (int i = column; i < column + length; i++)
            {
                //Console.WriteLine($"-----DIGIT: {lines[row][i]} ({row},{i})-----");
                bool tempResult = IsDigitValid(lines, row, i, symbolLocations);
                if (tempResult) return true;
            }

            return false;
        }



        public static bool IsDigitValid(string[] lines, int row, int column, List<Tuple<int,int>> symbolLocations)
        {
            // check the digit surroundings, make sure it's not in a corner and goes out of bounds
            int iEnd = row == lines.Length - 1 ? row : row + 1;
            int jEnd = column == lines[row].Length - 1 ? column : column + 1;

            bool isValid = false;

            for (int i = row == 0 ? 0 : row - 1; i <= iEnd; i++)
            {
                for (int j = column == 0 ? 0 : column - 1; j <= jEnd; j++)
                {
                    if (!(i == row && j == column))
                    {
                        //Console.WriteLine($"[{i},{j}]: {symbols.Contains(lines[i][j])}");
                        if (symbols.Contains(lines[i][j]))
                        {
                            symbolLocations.Add(new Tuple<int, int>(i, j));
                            isValid = true;
                        }
                    }

                }
            }

            return isValid;
        }

    }
}
