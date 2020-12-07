using AOC.Helpers;

using System;
using System.Linq;

namespace AOC.Day3.Pt1
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputText = FileReader.GetProgramInput();
            var inputValues = FileReader.SplitInputByNewLine(inputText).ToArray();

            int mapWidth = inputValues[0].Length-1;
            int mapLength = inputValues.Length-1;
            int treeCount = 0, height = 0;

            for (int steps = 0; steps <= mapLength;)
            {
                steps += 3;
                height++;
                if (height > mapWidth) break;

                if (inputValues[steps].ToArray()[height] == '#') treeCount++;

            }
            Console.WriteLine($"Number of trees encountered: {treeCount}");
            Console.ReadKey();
        }
    }
}
