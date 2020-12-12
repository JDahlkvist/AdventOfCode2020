using AOC.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC.Day6.Pt1
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputText = FileReader.GetProgramInput();
            var answers = FileReader.SplitInputByNewLine(inputText).ToArray();

            int yesAnswers = 0;
            List<char> groupAnswers = new List<char>();
            foreach (var answer in answers)
            {
                if (string.IsNullOrEmpty(answer))
                {
                    yesAnswers += groupAnswers.Distinct().Count();
                    groupAnswers.Clear();
                }
                else
                {
                    groupAnswers.AddRange(answer.ToCharArray());
                }
            }
            if(groupAnswers.Count() != 0) yesAnswers += groupAnswers.Distinct().Count();

            Console.WriteLine($"Number of yes answers {yesAnswers}");
            Console.ReadKey();
        }


    }
}
