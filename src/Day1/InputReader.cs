using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day1;

internal static class InputReader
{
    internal static Input Read()
    {
        var input = new Input();
        string[] lines = File.ReadAllLines("Day1\\PuzzleInput.txt");

        foreach (var line in lines)
        {
            var locationIds = line.Split(' ');
            input.FirstLocationIds.Add(int.Parse(locationIds.First()));
            input.SecondLocationIds.Add(int.Parse(locationIds.Last()));
        }

        return input;
    }
}
