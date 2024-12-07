using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day6.Models;
using AdventOfCode.Day6.Services;

namespace AdventOfCode.Day6;

public static class Day6
{
    public static void Solve()
    {
        // read file
        var input = File.ReadAllLines($"Day6\\PuzzleInput.txt");
        var mapService = new MapService();
        var map = mapService.SetUpMap(input);
        mapService.Print(map);

        var solution = Part1.Solve(map);
        Console.WriteLine($"Day6 Part1 Solution: {solution}");

        solution = Part2.Solve(map);
        Console.WriteLine($"Day6 Part2 Solution: {solution}");
    }
}
