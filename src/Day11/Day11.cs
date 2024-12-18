using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day11;

public static class Day11
{
    public static void Solve()
    {
        // read file
        var fileName = "PuzzleInput.txt";
        var input = File.ReadAllText($"Day11\\{fileName}");
        var stoneNumbers = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
        var numberOfBlinks = 25;

        var solution = Part1.Solve(stoneNumbers, numberOfBlinks);
        Console.WriteLine($"Day11 Part1 Solution: {solution}");

        numberOfBlinks = 75;
        var solutionPart2 = Part2.Solve(stoneNumbers, numberOfBlinks);
        Console.WriteLine($"Day11 Part2 Solution: {solutionPart2}");
    }
}
