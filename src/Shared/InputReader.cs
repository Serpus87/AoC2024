using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day1;
using AdventOfCode.Day2;

namespace AdventOfCode.Shared;

public static class InputReader
{
    public static AdventOfCode.Day1.Input ReadDay1File(string fileName)
    {
        var input = new Day1.Input();
        string[] lines = File.ReadAllLines($"Day1\\{fileName}");

        foreach (var line in lines)
        {
            var locationIds = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            input.FirstLocationIds.Add(int.Parse(locationIds.First()));
            input.SecondLocationIds.Add(int.Parse(locationIds.Last()));
        }

        return input;
    }

    public static AdventOfCode.Day2.Input ReadDay2File(string fileName)
    {
        var input = new Day2.Input();
        string[] lines = File.ReadAllLines($"Day2\\{fileName}");

        foreach (var line in lines)
        {
            var levels = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            input.Reports.Add(new Report(levels.Select(int.Parse).ToList()));
        }

        return input;
    }
}
