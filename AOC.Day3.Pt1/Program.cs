using AOC.Helpers;

using System;
using System.Linq;
using System.Text;

namespace AOC.Day3.Pt1
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputText = FileReader.GetProgramInput();
            var mapStrip = FileReader.SplitInputByNewLine(inputText).ToArray();

            int stepLength = 3;
            
            int stripsNeeded = 
                (int)Math.Ceiling((double)(mapStrip.Count() * stepLength)/(double)mapStrip[0].Length);
            
            string[] map = BuildMap(stripsNeeded, mapStrip);

            int mapLengthIndex = map[0].Count() - 1;
            int mapHeightIndex = map.Count() - 1;
            int treeCount = 0, height = 0;

            for (int steps = 0; steps <= mapLengthIndex;)
            {
                steps += stepLength;
                height++;
                if (height > mapHeightIndex) break;

                if (map[height].ToArray()[steps] == '#') treeCount++;
            }
            Console.WriteLine($"Number of trees encountered: {treeCount}");
            Console.ReadKey();
        }
        static string[] BuildMap (int stripsNeeded, string[] mapStrip)
        {
            for (int i = 0; i < mapStrip.Count(); i++)
            {
                var mapRow = String.Concat(Enumerable.Repeat(mapStrip[i], stripsNeeded));
                mapStrip[i] = mapRow;
            }
            return mapStrip;
        }
    }
}
