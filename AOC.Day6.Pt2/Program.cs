using AOC.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC.Day6.Pt2
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputText = FileReader.GetProgramInput();
            var answers = FileReader.SplitInputByNewLine(inputText).ToArray();

            int yesAnswers = 0;
            List<char[]> groupAnswers = new List<char[]>();
            foreach (var answer in answers)
            {
                if (string.IsNullOrEmpty(answer))
                {
                    yesAnswers += UnanimousAnswers(groupAnswers);
                    groupAnswers.Clear();
                }
                else
                {
                    groupAnswers.Add(answer.ToCharArray());
                }
            }
            if(groupAnswers.Count() != 0) yesAnswers += UnanimousAnswers(groupAnswers);

            Console.WriteLine($"Number of yes answers {yesAnswers}");
            Console.ReadKey();
        }
        static int UnanimousAnswers(List<char[]> groupAnswers)
        {
            int unanimousAnswers = 0;
            foreach (var grouping in groupAnswers.SelectMany(ga => ga).ToList().GroupBy(ans => new { ans }))
            {
                if (grouping.Count() == groupAnswers.Count()) unanimousAnswers++;
            }
            return unanimousAnswers;
        }

    }
}
