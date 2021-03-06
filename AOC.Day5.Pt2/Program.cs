﻿using AOC.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC.Day5.Pt2
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputText = FileReader.GetProgramInput();
            var boardingPasses = FileReader.SplitInputByNewLine(inputText).ToArray();

            List<int> seatIds = new List<int>();

            foreach (var boardingPass in boardingPasses)
            {
                int rowNumber = GetRow(boardingPass.Take(7).ToArray());
                int seatPos = GetSeatPosition(boardingPass.TakeLast(3).ToArray());
                seatIds.Add(rowNumber * 8 + seatPos);
            }

            var orderedList = seatIds.OrderBy(nr => nr).ToArray();
            for (int i = 0; i < orderedList.Length - 2; i++)
            {
                var nextSeatId = orderedList[i + 1];
                var currentSeatId = orderedList[i];
                if (currentSeatId + 2 == nextSeatId)
                {
                    Console.WriteLine($"My seat id: {currentSeatId+1}");
                    break;
                }
            }

            Console.ReadKey();
        }
        static int GetRow(char[] rowBinary)
        {
            List<int> rowRange = Enumerable.Range(0, 128).ToList();
            for (int i = 0; i < rowBinary.Count(); i++)
            {
                if (rowBinary[i] == 'B')
                {
                    if (rowRange.Count() == 2) return rowRange.Max();
                    var intersection = (rowRange.Count() / 2);
                    rowRange = rowRange.GetRange(intersection, intersection).ToList();
                }
                if (rowBinary[i] == 'F')
                {
                    if (rowRange.Count() == 2) return rowRange.Min();
                    var intersection = rowRange.Count() / 2;
                    rowRange = rowRange.GetRange(0, intersection).ToList();
                }
            }
            return -1;
        }
        static int GetSeatPosition(char[] rowBinary)
        {
            List<int> rowRange = Enumerable.Range(0, 8).ToList();
            for (int i = 0; i < rowBinary.Count(); i++)
            {
                if (rowBinary[i] == 'R')
                {
                    if (rowRange.Count() == 2) return rowRange.Max();
                    var intersection = (rowRange.Count() / 2);
                    rowRange = rowRange.GetRange(intersection, intersection).ToList();
                }
                if (rowBinary[i] == 'L')
                {
                    if (rowRange.Count() == 2) return rowRange.Min();
                    var intersection = rowRange.Count() / 2;
                    rowRange = rowRange.GetRange(0, intersection).ToList();
                }
            }
            return -1;
        }

    }
}
