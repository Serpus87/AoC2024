﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day1;

public static class InputReader
{
    public static Input Read(string fileName)
    {
        var input = new Input();
        string[] lines = File.ReadAllLines($"Day1\\{fileName}");

        foreach (var line in lines)
        {
            var locationIds = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            input.FirstLocationIds.Add(int.Parse(locationIds.First()));
            input.SecondLocationIds.Add(int.Parse(locationIds.Last()));
        }

        return input;
    }
}
