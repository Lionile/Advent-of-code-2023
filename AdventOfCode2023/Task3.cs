using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Task3
    {
        const string filename = "task3.txt";

        public static void RunPart1()
        {
            string[] lines = File.ReadAllLines(filename);
            foreach(string line in lines)
            {
                Console.WriteLine(line);
            }
        }

    }
}
