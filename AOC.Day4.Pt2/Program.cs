using AOC.Helpers;

using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AOC.Day4.Pt2
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
            return (ByrValid()
                && IyrValid()
                && EyrValid()
                && HgtValid()
                && HclValid()
                && EclValid()
                && PidValid());
        }
        private bool ByrValid()
        {
            if (string.IsNullOrEmpty(Byr)) return false;
            var parsed = int.TryParse(Byr, out int result);
            return parsed && result >= 1920 && result <= 2002;
        }
        private bool IyrValid()
        {
            if (string.IsNullOrEmpty(Iyr)) return false;
            var parsed = int.TryParse(Iyr, out int result);
            return parsed && result >= 2010 && result <= 2020;
        }
        private bool EyrValid()
        {
            if (string.IsNullOrEmpty(Eyr)) return false;
            var parsed = int.TryParse(Eyr, out int result);
            return parsed && result >= 2020 && result <= 2030;
        }
        private bool HgtValid()
        {
            if (string.IsNullOrEmpty(Hgt)) return false;
            var suffix = Hgt[^2..];
            var parsed = int.TryParse(Hgt.Remove(Hgt.Length - 2, 2), out int result);
            if (!parsed) return false;
            if (suffix == "cm" && result > 149 && result < 194) return true;
            if (suffix == "in" && result > 58 && result < 77) return true;
            return false;
        } //checked
        private bool HclValid()
        {
            if (string.IsNullOrEmpty(Hcl)) return false;
            return (new Regex(@"^[#][a-f0-9]{6}$")).Match(Hcl).Success;
        } //checked
        private bool EclValid()
        {
            if (string.IsNullOrEmpty(Ecl)) return false;
            var variants = new string[]
            {
                "amb", "blu", "brn", "gry" ,"grn", "hzl" ,"oth"
            };
            return variants.Contains(Ecl);
        } //checked
        private bool PidValid()
        {
            if (string.IsNullOrEmpty(Pid)) return false;
            return (new Regex(@"^\d{9}$").Match(Pid).Success);
        } //checked

    }
}
