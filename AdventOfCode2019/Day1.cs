using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019
{
    /*
     * Day 1: The Tyranny of the Rocket Equation
     */
    public class Day1
    {
        public static void Solve()
        {
            string inputFilePath = $"{Environment.CurrentDirectory}/Input/Day1Input.txt";
            string[] lines = File.ReadAllLines(inputFilePath);
            IEnumerable<int> moduleMasses = lines.Select(int.Parse);

            int totalFuel = moduleMasses.Sum(CalcFuelReq);

            Console.WriteLine("Day 1: The Tyranny of the Rocket Equation");
            Console.WriteLine($"Total Fuel: {totalFuel}");
        }

        private static int CalcFuelReq(int mass)
        {
            return mass / 3 - 2;
        }
    }
}