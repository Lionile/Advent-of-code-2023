using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public class Task1
    {
        const string filename = "../../../test files/task1.txt";
        const string testFilename1 = "../../../test files/task1test.txt";
        const string testFilename2 = "../../../test files/task1test2.txt";

        readonly static Dictionary<string, char> numberWords = new Dictionary<string, char>() { { "one", '1' }, { "two", '2' }, { "three", '3' }, { "four", '4' },
                                                                                        {"five", '5' }, {"six", '6' }, {"seven", '7' }, {"eight", '8' }, {"nine", '9' }};

        public static void RunPart1()
        {
            string[] lines = File.ReadAllLines(filename);
            int total = 0;

            foreach(string line in lines)
            {
                char first = '0';
                char last = '0';
                bool foundFirst = false;

                foreach(char c in line)
                {
                    if (Char.IsNumber(c))
                    {
                        if (!foundFirst)
                        {
                            foundFirst = true;
                            first = c;
                        }
                        last = c;
                        
                    }
                }
                total += Int32.Parse((first.ToString() + last.ToString()));
            }

            Console.WriteLine(total);
        }



        public static void RunPart2()
        {
            string[] lines = File.ReadAllLines(filename);
            int total = 0;

            foreach (string line in lines)
            {
                char first = '0';
                char last = '0';
                bool foundFirst = false;

                for(int i = 0; i < line.Length; i++)
                {
                    if (Char.IsNumber(line[i]))
                    {
                        if (!foundFirst)
                        {
                            foundFirst = true;
                            first = line[i];
                        }
                        last = line[i];

                    }

                    foreach(string num in numberWords.Keys)
                    {
                        if(num.Length <= line.Length - i)
                        {
                            if (line.Substring(i).StartsWith(num))
                            {
                                if (!foundFirst)
                                {
                                    foundFirst = true;
                                    first = numberWords[num];
                                }
                                last = numberWords[num];
                            }
                        }
                    }
                }


                total += Int32.Parse((first.ToString() + last.ToString()));
            }

            Console.WriteLine(total);
        }
    }
}
