using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day2;

public static class InputReader
{
    public static Input Read(string fileName)
    {
        var input = new Input();
        string[] lines = File.ReadAllLines($"Day2\\{fileName}");

        foreach (var line in lines)
        {
            var levels = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            input.Reports.Add(new Report(levels.Select(int.Parse).ToList()));
        }

        return input;
    }
}
