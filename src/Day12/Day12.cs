using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day12;

public static class Day12
{
    public static void Solve()
    { 
        // read file
        var fileName = "PuzzleInput.txt";
        var input = File.ReadAllLines($"Day12\\{fileName}");
        var garden = GardenService.SetupGarden(input);

        var solution = Part1.Solve(garden);
        Console.WriteLine($"Day10 Part1 Solution: {solution}");

        solution = Part2.Solve(garden);
        Console.WriteLine($"Day10 Part2 Solution: {solution}");

    }
}
