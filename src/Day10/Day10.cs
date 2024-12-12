using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day10;
using AdventOfCode.Day9;

namespace AdventOfCode.Day10;

public static class Day10
{
    public static void Solve()
    {
        // read file
        var fileName = "PuzzleInput.txt";
        var input = File.ReadAllLines($"Day10\\{fileName}");
        var map = MapService.GetMap(input);

        var solution = Part1.Solve(map);
        Console.WriteLine($"Day9 Part1 Solution: {solution}");

        solution = Part2.Solve(map);
        Console.WriteLine($"Day9 Part2 Solution: {solution}");
    }
}
