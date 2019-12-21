using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019
{
    public class IntcodeComputer
    {
        private readonly List<int> _input;
        private readonly List<int> _program;
        private Queue<int> _inputQueue;
        private int _instructionPointer;
        private OpCode _opCode;
        private List<int> _parameterModes;


        public IntcodeComputer(List<int> intcode, List<int> input = null)
        {
            _program = intcode;
            _input = input ?? new List<int>();
            Reset();
        }

        public List<int> Output { get; private set; }
        public List<int> Memory { get; private set; }

        public void Reset()
        {
            _instructionPointer = 0;
            Memory = new List<int>(_program);
            Output = new List<int>();
            _inputQueue = new Queue<int>(_input);
            _parameterModes = new List<int>();
        }

        public static IEnumerable<int> ParseIntcode(string txt)
        {
            IEnumerable<int> intCode = txt.Split(',').Select(int.Parse);
            return intCode;
        }

        public void Compute()
        {
            while (true)
            {
                int instruction = Memory[_instructionPointer];
                ParseInstruction(instruction);

                switch (_opCode)
                {
                    case OpCode.Halt:
                        return;
                    case OpCode.Add:
                    {
                        OpAdd();
                        break;
                    }
                    case OpCode.Multiply:
                    {
                        OpMultiply();
                        break;
                    }
                    case OpCode.Input:
                    {
                        OpInput();
                        break;
                    }
                    case OpCode.Output:
                    {
                        OpOutput();
                        break;
                    }
                    default:
                        throw new InvalidOperationException("Invalid OpCode");
                }
            }
        }

        private static OpCode ParseOpCode(int opCodeInt)
        {
            if (!Enum.IsDefined(typeof(OpCode), opCodeInt))
            {
                throw new InvalidOperationException($"Invalid OpCode: {opCodeInt}");
            }

            return (OpCode) opCodeInt;
        }

        private void ParseInstruction(int instruction)
        {
            int opCodeInt = instruction % 100;
            OpCode opCode = ParseOpCode(opCodeInt);

            instruction /= 100;
            var parameterModes = new List<int>();

            while (instruction != 0)
            {
                parameterModes.Add(instruction % 10);
                instruction /= 10;
            }

            _opCode = opCode;
            _parameterModes = parameterModes;
        }

        private int GetParameter(int paramNumber, bool isPos = false)
        {
            if (isPos || _parameterModes.Count >= paramNumber && _parameterModes[paramNumber - 1] == 1)
            {
                // value mode
                return Memory[_instructionPointer + paramNumber];
            }
            else
            {
                // position mode
                int pos = Memory[_instructionPointer + paramNumber];
                return Memory[pos];
            }
        }

        private void OpAdd()
        {
            int a = GetParameter(1);
            int b = GetParameter(2);
            int posResult = GetParameter(3, true);

            int result = a + b;

            Memory[posResult] = result;

            _instructionPointer += 4;
        }

        private void OpMultiply()
        {
            int a = GetParameter(1);
            int b = GetParameter(2);
            int posResult = GetParameter(3, true);

            int result = a * b;

            Memory[posResult] = result;

            _instructionPointer += 4;
        }

        private void OpInput()
        {
            int posResult = GetParameter(1, true);
            int input = _inputQueue.Dequeue();

            Memory[posResult] = input;

            _instructionPointer += 2;
        }

        private void OpOutput()
        {
            int output = GetParameter(1);

            Output.Add(output);

            _instructionPointer += 2;
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