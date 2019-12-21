using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019
{
    /*
     * Day 5: Sunny with a Chance of Asteroids
     */
    public class Day5
    {
        public static void Solve()
        {
            Console.WriteLine("Day 5: Sunny with a Chance of Asteroids");
            Console.WriteLine();

            //Test1();
            //Test2();

            Part1();
            //Part2();
        }

        public static void Test1()
        {
            var intcode = new List<int>
            {
                1002,
                4,
                3,
                4,
                33
            };
            var computer = new IntcodeComputer(intcode);
            computer.Compute();
            List<int> result = computer.Memory;
        }

        public static void Test2()
        {
            var intcode = new List<int>
            {
                3,
                0,
                4,
                0,
                99
            };
            var input = new List<int> {1};
            var computer = new IntcodeComputer(intcode, input);
            computer.Compute();
            List<int> result = computer.Output;
        }

        private static void Part1()
        {
            var computer = new IntcodeComputer(GetIntCode(), new List<int> {1});
            computer.Compute();

            if (computer.Output.Take(computer.Output.Count - 1).Any(x => x != 0))
            {
                throw new Exception("Test Failed");
            }

            Console.WriteLine("Part 1:");
            Console.WriteLine($"Diagnostic Code: {computer.Output.Last()}");
            Console.WriteLine();
        }

        private static void Part2()
        {
            Console.WriteLine("Part 2:");
            Console.WriteLine("");
            Console.WriteLine();
        }

        private static List<int> GetIntCode()
        {
            string inputFilePath = $"{Environment.CurrentDirectory}/Input/Day5Input.txt";
            string txt = File.ReadAllText(inputFilePath);
            List<int> intCode = IntcodeComputer.ParseIntcode(txt).ToList();
            return intCode;
        }
    }
}