using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC.Helpers
{
    public static class FileReader
    {
        public static string GetProgramInput() =>
            System.IO.File.ReadAllText($@"{System.IO.Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName}\Input.txt");

        public static IEnumerable<string> SplitInputByNewLine(
            string inputText) =>
            inputText.Split("\r\n").Select(str => str.Trim());

    }
}