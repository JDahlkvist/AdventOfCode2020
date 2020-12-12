using AOC.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC.Day8.Pt1
{
    class Program
    {
        static int accumulator = 0;
        static List<Instruction> instructions;
        static void Main(string[] args)
        {
            var inputText = FileReader.GetProgramInput();
            instructions = FileReader.SplitInputByNewLine(inputText)
                .Select(inst => inst.ToInstruction()).ToList();

            ExecuteInstruction(instructions.First(), 0);

            Console.WriteLine($"Accumulator value: {accumulator} ");
            Console.ReadKey();
        }


        static void ExecuteInstruction(Instruction inst, int currentIndex)
        {
            int nextIndex = -1;
            if (!inst.HasBeenRun)
            {
                if (inst.Operation == "acc")
                {
                    accumulator += inst.Value;
                    nextIndex = 1;
                }
                else if (inst.Operation == "nop")
                {
                    nextIndex = 1;
                }
                else if (inst.Operation == "jmp")
                {
                    nextIndex = inst.Value; 
                }
                else
                {
                    throw new Exception("Something went wrong");
                }
                inst.HasBeenRun = true;

                ExecuteInstruction(
                    instructions.ElementAt(currentIndex + nextIndex),
                    currentIndex + nextIndex);
            }
        }
    }

    public class Instruction
    {
        public Instruction(string operation, string value)
        {
            Operation = operation;
            if (value.StartsWith('+')) Value = int.Parse(value[1..]);
            else Value = int.Parse(value);
        }
        public string Operation { get; set; }
        public int Value { get; set; }
        public bool HasBeenRun { get; set; }
    }
    public static class InstExtensions
    {
        public static Instruction ToInstruction(this string instructionString)
        {
            var inst = instructionString.Split(' ');
            return new Instruction(inst[0], inst[1]);
        }
    }
}
