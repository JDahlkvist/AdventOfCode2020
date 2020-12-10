using AOC.Helpers;

using System;
using System.Linq;
using System.Reflection;

namespace AOC.Day4.Pt1
{
    class Program
    {
        static string[] _separators = new string[]
                    {
                    "byr",
                    "iyr",
                    "eyr",
                    "hgt",
                    "hcl",
                    "ecl",
                    "pid",
                    "cid"};

        static void Main(string[] args)
        {
            var inputText = FileReader.GetProgramInput();
            var batch = FileReader.SplitInputByNewLine(inputText).ToArray();

            Console.WriteLine($"Number of valid passports: {ValidPassports(batch)}");
            Console.ReadKey();
        }
        static int ValidPassports(string[] batch)
        {
            int validPassports = 0;
            Passport passport = new Passport();
            foreach (var ppLine in batch)
            {
                if (string.IsNullOrEmpty(ppLine) || string.IsNullOrWhiteSpace(ppLine))
                {
                    if (passport.IsValid()) validPassports++;
                    passport = new Passport();
                    continue;
                }

                var properties = ppLine.Split(" ");
                for (int i = 0; i < properties.Length; i++)
                {
                    foreach (var separator in _separators)
                    {
                        if (properties[i].Contains(separator))
                        {
                            var value = properties[i].Split(separator, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault()?.Substring(1);
                            if (value != null)
                            {
                                var propName =
                                    separator
                                    .First()
                                    .ToString()
                                    .ToUpper() + separator.Substring(1);

                                typeof(Passport).GetProperty(propName).SetValue(passport, value);

                            }
                        }
                    }
                }
            }
            if (passport.IsValid()) validPassports++;
            return validPassports;
        }
    }
    class Passport
    {
        public string Byr { get; set; }
        public string Iyr { get; set; }
        public string Eyr { get; set; }
        public string Hgt { get; set; }
        public string Hcl { get; set; }
        public string Ecl { get; set; }
        public string Pid { get; set; }
        public string Cid { get; set; }
        public bool IsValid()
        {
            return (!string.IsNullOrEmpty(Byr)
                && !string.IsNullOrEmpty(Iyr)
                && !string.IsNullOrEmpty(Eyr)
                && !string.IsNullOrEmpty(Hgt)
                && !string.IsNullOrEmpty(Hcl)
                && !string.IsNullOrEmpty(Ecl)
                && !string.IsNullOrEmpty(Pid));
        }
    }
}
