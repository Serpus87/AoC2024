using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day14;

namespace AdventOfCode.Day14;

public static class Day14
{
    public static void Solve()
    {
        // read file
        //var fileName = "PuzzleInput.txt";
        var fileName = "PuzzleInput.txt";
        var input = File.ReadAllLines($"Day14\\{fileName}");

        var robots = MapService.GetRobotsFromFile(input);
        var mapNRows = 103;
        var mapNColumns = 101;
        var map = MapService.SetupMap(mapNRows, mapNColumns, robots);

        var solution = Part1.Solve(map,robots);
        Console.WriteLine($"Day14 Part1 Solution: {solution}");

        var part2Robots = MapService.GetRobotsFromFile(input);
        var part2Map = MapService.SetupMap(mapNRows, mapNColumns, part2Robots);
        var longSolution = Part2.Solve(part2Map, part2Robots);
        Console.WriteLine($"Day14 Part2 Solution: {longSolution}");
    }
}
