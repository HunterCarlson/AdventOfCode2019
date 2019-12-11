using System;
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
            //Part2();
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
            Console.WriteLine("Part 2:");
            Console.WriteLine("");
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
                int output;

                switch (opCode)
                {
                    case 1:
                        output = a + b;
                        break;
                    case 2:
                        output = a * b;
                        break;
                    default:
                        throw new InvalidOperationException($"Invalid OpCode: {opCode}");
                }

                intCode[posOutput] = output;

                currentPos += 4;
            }

            return intCode;
        }
    }
}