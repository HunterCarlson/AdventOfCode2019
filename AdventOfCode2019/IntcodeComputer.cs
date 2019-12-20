using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019
{
    public class IntcodeComputer
    {
        public static IEnumerable<int> ParseIntcode(string txt)
        {
            IEnumerable<int> intCode = txt.Split(',').Select(int.Parse);
            return intCode;
        }

        public static List<int> Compute(List<int> intcode)
        {
            int currentPos = 0;

            while (true)
            {
                int opCodeInt = intcode[currentPos];

                if (!Enum.IsDefined(typeof(OpCode), opCodeInt))
                {
                    throw new InvalidOperationException($"Invalid OpCode: {opCodeInt}");
                }

                var opCode = (OpCode) opCodeInt;

                if (opCode == OpCode.Halt)
                {
                    break;
                }

                int pos1 = intcode[currentPos + 1];
                int pos2 = intcode[currentPos + 2];
                int posOutput = intcode[currentPos + 3];

                int a = intcode[pos1];
                int b = intcode[pos2];

                int output = opCode switch
                {
                    OpCode.Add => (a + b),
                    OpCode.Multiply => (a * b),
                    OpCode.Halt => throw new Exception("Something went wrong, should have caught the halt earlier"),
                    _ => throw new InvalidOperationException($"Invalid OpCode: {opCode}")
                };

                intcode[posOutput] = output;

                currentPos += 4;
            }

            return intcode;
        }

        private enum OpCode
        {
            Add = 1,
            Multiply = 2,
            Halt = 99
        }
    }
}