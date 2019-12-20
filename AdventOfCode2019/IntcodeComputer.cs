using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019
{
    public class IntcodeComputer
    {
        private readonly List<int> _program;

        public List<int> Output { get; private set; }
        public List<int> Memory { get; private set; }

        public IntcodeComputer(List<int> intcode)
        {
            _program = intcode;
            Reset();
        }

        public void Reset()
        {
            Memory = new List<int>(_program);
            Output = new List<int>();
        }

        public static IEnumerable<int> ParseIntcode(string txt)
        {
            IEnumerable<int> intCode = txt.Split(',').Select(int.Parse);
            return intCode;
        }

        public void Compute()
        {
            int currentPos = 0;

            while (true)
            {
                int opCodeInt = Memory[currentPos];

                if (!Enum.IsDefined(typeof(OpCode), opCodeInt))
                {
                    throw new InvalidOperationException($"Invalid OpCode: {opCodeInt}");
                }

                var opCode = (OpCode) opCodeInt;

                if (opCode == OpCode.Halt)
                {
                    break;
                }

                int pos1 = Memory[currentPos + 1];
                int pos2 = Memory[currentPos + 2];
                int posOutput = Memory[currentPos + 3];

                int a = Memory[pos1];
                int b = Memory[pos2];

                int output = opCode switch
                {
                    OpCode.Add => (a + b),
                    OpCode.Multiply => (a * b),
                    OpCode.Halt => throw new Exception("Something went wrong, should have caught the halt earlier"),
                    _ => throw new InvalidOperationException($"Invalid OpCode: {opCode}")
                };

                Memory[posOutput] = output;

                currentPos += 4;
            }
        }

        private enum OpCode
        {
            Add = 1,
            Multiply = 2,
            Input = 3,
            Output = 4,
            Halt = 99
        }
    }
}