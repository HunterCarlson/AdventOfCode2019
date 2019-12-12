using System;
using System.Collections.Generic;
using System.Linq;
using static MathTools.MathTools;

namespace AdventOfCode2019
{
    /*
     * Day 4: Secure Container
     */
    public class Day4
    {
        private const int Min = 236491;
        private const int Max = 713787;

        public static void Solve()
        {
            Console.WriteLine("Day 4: Secure Container");
            Console.WriteLine();

            Part1();
            Part2();
        }

        private static void Part1()
        {
            List<int> validPasswords = Enumerable.Range(Min, Max - Min).Where(i => HasTwoSameAdjacentDigits(i) && DigitsNeverDecrease(i)).ToList();

            Console.WriteLine("Part 1:");
            Console.WriteLine($"Valid Passwords: {validPasswords.Count}");
            Console.WriteLine();
        }

        private static void Part2()
        {
            List<int> validPasswords = Enumerable.Range(Min, Max - Min).Where(i => HasExactlyTwoSameAdjacentDigits(i) && DigitsNeverDecrease(i))
                .ToList();

            Console.WriteLine("Part 2:");
            Console.WriteLine($"Valid Passwords: {validPasswords.Count}");
            Console.WriteLine();
        }

        private static bool HasTwoSameAdjacentDigits(int n)
        {
            string str = n.ToString();

            for (int i = 0; i < str.Length - 1; i++)
            {
                if (str[i] == str[i + 1])
                {
                    return true;
                }
            }

            return false;
        }

        private static bool HasExactlyTwoSameAdjacentDigits(int n)
        {
            string str = n.ToString();

            return str.GroupBy(c => c).Select(g => g.Count()).Any(count => count == 2);
        }

        private static bool DigitsNeverDecrease(int n)
        {
            int[] digits = IntToDigitArray(n);

            for (int i = 0; i < digits.Length - 1; i++)
            {
                if (digits[i] > digits[i + 1])
                {
                    return false;
                }
            }

            return true;
        }
    }
}