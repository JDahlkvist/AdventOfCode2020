using AOC.Helpers;

using System;
using System.Linq;

namespace AOC.Day2.Pt2
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputText = FileReader.GetProgramInput();
            var inputValues = FileReader.SplitInputByNewLine(inputText);

            int numberOfValidPasswords = 0;
            foreach (var policy in inputValues.Select(policy => policy.ParseToPasswordPolicy()))
            {
                if (policy == null) continue;
                if (PasswordIsValid(policy)) numberOfValidPasswords++;
            }

            Console.WriteLine($"Number of valid passwords: {numberOfValidPasswords}");
            Console.ReadKey();
        }
        static bool PasswordIsValid(PasswordPolicy pp)
        {
            int occurances = 0;

            if (pp.Password[pp.MinChars - 1] == pp.Letter) occurances++;
            if (pp.Password[pp.MaxChars - 1] == pp.Letter) occurances++;

            return occurances == 1;
        }
    }
    public static class Helpers
    {
        public static PasswordPolicy ParseToPasswordPolicy(this string policyString)
        {
            var values = policyString.Split(' ');
            var minMax = values[0].Split('-');
            var min = int.Parse(minMax[0]);
            if (min <= 0) return null;
            return new PasswordPolicy
              (
                  min,
                  int.Parse(minMax[1]),
                  values[1][0],
                  values.Last().ToCharArray()
              );
        }
    }

    public class PasswordPolicy
    {
        public PasswordPolicy(int min, int max, char letter, char[] password)
        {
            MinChars = min;
            MaxChars = max;
            Letter = letter;
            Password = password;
        }
        public int MinChars { get; set; }
        public int MaxChars { get; set; }
        public char Letter { get; set; }
        public char[] Password { get; set; }
    }
}
