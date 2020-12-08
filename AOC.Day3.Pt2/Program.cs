using AOC.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC.Day3.Pt2
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputText = FileReader.GetProgramInput();
            var mapStrip = FileReader.SplitInputByNewLine(inputText).ToArray();

            var paths = new List<(int, int)> {
                ( 1, 1 ),
                ( 3, 1 ),
                ( 5, 1 ),
                ( 7, 1 ),
                ( 1, 2 )
            };

            var trees = new List<int>();
            foreach (var path in paths)
            {
                trees.Add(FindTrees(path.Item1, path.Item2, mapStrip));
            }

            long product = 0;
            trees.ForEach(tree => { product = product == 0 ? tree : product * tree; });

            Console.WriteLine($"Product of trees encountered: {product}");
            Console.ReadKey();
        }
        static string[] BuildMap(int stripsNeeded, string[] mapStrip)
        {
            for (int i = 0; i < mapStrip.Count(); i++)
            {
                var mapRow = String.Concat(Enumerable.Repeat(mapStrip[i], stripsNeeded));
                mapStrip[i] = mapRow;
            }
            return mapStrip;
        }
        static int FindTrees(int stepLength, int stepHeight, string[] mapStrip)
        {
            int stripsNeeded =
                (int)Math.Ceiling((double)(mapStrip.Count() * stepLength) / (double)mapStrip[0].Length);

            string[] map = BuildMap(stripsNeeded, mapStrip);
            int mapLengthIndex = map[0].Count() - 1;
            int mapHeightIndex = map.Count() - 1;

            int treeCount = 0;
            int height = 0;

            for (int steps = 0; steps <= mapLengthIndex;)
            {
                steps += stepLength;
                height += stepHeight;
                if (height > mapHeightIndex) break;

                if (map[height].ToArray()[steps] == '#') treeCount++;
            }
            return treeCount;
        }
    }
}
