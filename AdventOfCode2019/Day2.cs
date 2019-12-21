using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;

namespace AdventOfCode2019
{
    /*
     * Day 2: 1202 Program Alarm
     */
    public class Day2
    {
        public static void Solve()
        {
            Console.WriteLine("Day 2: 1202 Program Alarm");
            Console.WriteLine();

            //Test1();

            Part1();
            // 3790645

            Part2();
            // 6577
        }

        private static void Test1()
        {
            var intCode = new List<int>
            {
                1,
                9,
                10,
                3,
                2,
                3,
                11,
                0,
                99,
                30,
                40,
                50
            };

            var computer = new IntcodeComputer(intCode);
            computer.Compute();

            List<int> result = computer.Memory;
        }

        private static void Part1()
        {
            List<int> intCode = GetIntCode();

            var computer = new IntcodeComputer(intCode);
            computer.Memory[1] = 12;
            computer.Memory[2] = 2;
            computer.Compute();

            Console.WriteLine("Part 1:");
            Console.WriteLine($"Position 0: {computer.Memory[0]}");
            Console.WriteLine();
        }

        private static void Part2()
        {
            (int noun, int verb) = FindInputs();

            Console.WriteLine("Part 2:");
            Console.WriteLine($"100 * {noun} + {verb} =");
            Console.WriteLine($"{100 * noun + verb}");
            Console.WriteLine();
        }

        private static List<int> GetIntCode()
        {
            string inputFilePath = $"{Environment.CurrentDirectory}/Input/Day2Input.txt";
            string txt = File.ReadAllText(inputFilePath);
            var intCode = IntcodeComputer.ParseIntcode(txt).ToList();
            return intCode;
        }

        private static (int noun, int verb) FindInputs()
        {
            List<int> intcode = GetIntCode();
            var computer = new IntcodeComputer(intcode);

            foreach (int noun in Enumerable.Range(0, 100))
            {
                foreach (int verb in Enumerable.Range(0, 100))
                {
                    computer.Reset();
                    computer.Memory[1] = noun;
                    computer.Memory[2] = verb;
                    computer.Compute();

                    if (computer.Memory[0] == 19690720)
                    {
                        return (noun, verb);
                    }
                }
            }

            throw new Exception("No matching inputs found");
        }
    }
}