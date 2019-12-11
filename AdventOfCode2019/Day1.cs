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
            Console.WriteLine("Day 1: The Tyranny of the Rocket Equation");
            Console.WriteLine();

            Part1();
            Part2();
        }


        private static void Part1()
        {
            IEnumerable<int> moduleMasses = GetModuleMasses();
            int totalFuel = moduleMasses.Sum(CalcFuelReq1);

            Console.WriteLine("Part 1:");
            Console.WriteLine($"Total Fuel: {totalFuel}");
            Console.WriteLine();
        }

        private static void Part2()
        {
            IEnumerable<int> moduleMasses = GetModuleMasses();
            int totalFuel = moduleMasses.Sum(CalcFuelReq2);

            Console.WriteLine("Part 2:");
            Console.WriteLine($"Total Fuel: {totalFuel}");
            Console.WriteLine();
        }

        private static IEnumerable<int> GetModuleMasses()
        {
            string inputFilePath = $"{Environment.CurrentDirectory}/Input/Day1Input.txt";
            string[] lines = File.ReadAllLines(inputFilePath);
            IEnumerable<int> moduleMasses = lines.Select(int.Parse);
            return moduleMasses;
        }

        private static int CalcFuelReq1(int mass)
        {
            return mass / 3 - 2;
        }

        private static int CalcFuelReq2(int mass)
        {
            var fuelAmounts = new List<int>();
            int initFuel = CalcFuelReq1(mass);
            fuelAmounts.Add(initFuel);

            int nextFuel = CalcFuelReq1(fuelAmounts.Last());

            while (nextFuel > 0)
            {
                fuelAmounts.Add(nextFuel);
                nextFuel = CalcFuelReq1(fuelAmounts.Last());
            }

            return fuelAmounts.Sum();
        }
    }
}