﻿using System;
using System.Collections.Generic;
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
            Part2();
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
            List<int> result = Compute(intCode);
        }

        private static void Part1()
        {
            List<int> intCode = GetIntCode().ToList();

            intCode[1] = 12;
            intCode[2] = 2;

            List<int> result = Compute(intCode);

            Console.WriteLine("Part 1:");
            Console.WriteLine($"Position 0: {result[0]}");
            Console.WriteLine();
        }

        private static void Part2()
        {
            (int noun, int verb) inputs = FindInputs();

            Console.WriteLine("Part 2:");
            Console.WriteLine($"100 * {inputs.noun} + {inputs.verb} =");
            Console.WriteLine($"{100 * inputs.noun + inputs.verb}");
            Console.WriteLine();
        }

        private static IEnumerable<int> GetIntCode()
        {
            string inputFilePath = $"{Environment.CurrentDirectory}/Input/Day2Input.txt";
            string txt = File.ReadAllText(inputFilePath);
            IEnumerable<int> intCode = txt.Split(',').Select(int.Parse);
            return intCode;
        }

        private static List<int> Compute(List<int> intCode)
        {
            int currentPos = 0;
            int[] validOpCodes =
            {
                1,
                2,
                99
            };

            while (true)
            {
                int opCode = intCode[currentPos];

                if (!validOpCodes.Contains(opCode))
                {
                    throw new InvalidOperationException($"Invalid OpCode: {opCode}");
                }

                if (opCode == 99)
                {
                    break;
                }

                int pos1 = intCode[currentPos + 1];
                int pos2 = intCode[currentPos + 2];
                int posOutput = intCode[currentPos + 3];

                int a = intCode[pos1];
                int b = intCode[pos2];

                int output = opCode switch
                {
                    1 => (a + b),
                    2 => (a * b),
                    _ => throw new InvalidOperationException($"Invalid OpCode: {opCode}")
                };

                intCode[posOutput] = output;

                currentPos += 4;
            }

            return intCode;
        }

        private static (int noun, int verb) FindInputs()
        {
            List<int> cleanIntCode = GetIntCode().ToList();

            foreach (int noun in Enumerable.Range(0, 100))
            {
                foreach (int verb in Enumerable.Range(0, 100))
                {
                    var intCode = new List<int>(cleanIntCode)
                    {
                        [1] = noun,
                        [2] = verb
                    };

                    List<int> result = Compute(intCode);

                    if (result[0] == 19690720)
                    {
                        return (noun, verb);
                    }
                }
            }

            throw new Exception("No matching inputs found");
        }
    }
}