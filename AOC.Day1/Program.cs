using AOC.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC.Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputText = FileReader.GetProgramInput();
            var inputValues = FileReader.SplitInputByNewLine(inputText).Select(str => Convert.ToInt32(str));

            Console.WriteLine("Numbers to add up to 2020: ");
            var numberOfValuesToCombine = int.Parse(Console.ReadLine());

            var combinationList = CreateCombinations(inputValues, numberOfValuesToCombine);

            var found = false;
            foreach (var combination in combinationList)
            {
                int sum = 0;
                foreach (var number in combination)
                {
                    sum += number;
                }

                if (sum == 2020)
                {
                    long product = 0;
                    foreach (var number in combination)
                    {
                        if (product == 0) product = number;
                        else { product *= number; }
                    }

                    Console.WriteLine($"Sum of {string.Join(" + ", combination)} = 2020, product: {product}");
                    found = true;
                    break;
                }
            }
            if (!found) Console.WriteLine($"A {numberOfValuesToCombine} combination to add up to 2020 was not found");
            Console.ReadKey();
        }
        private static IEnumerable<IEnumerable<T>> CreateCombinations<T>(IEnumerable<T> list, int combinationSize)
        {
            int i = 0;
            foreach (var item in list)
            {
                if (combinationSize == 1)
                    yield return new T[] { item };
                else
                {
                    foreach (var result in CreateCombinations(list.Skip(i + 1), combinationSize - 1))
                        yield return new T[] { item }.Concat(result);
                }
                ++i;
            }
        }
    }
}

