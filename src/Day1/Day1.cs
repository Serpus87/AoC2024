﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day1;

public static class Day1
{
    public static void Solve()
    {
        // readInput
        var fileName = "PuzzleInput.txt";
        var input = InputReader.Read(fileName);

        var solution = Part1.Solve(input);
        Console.WriteLine($"Day1 Part1 Solution: {solution}");

        solution = Part2.Solve(input);
        Console.WriteLine($"Day1 Part2 Solution: {solution}");
    }
}
