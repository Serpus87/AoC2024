using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day9;

public static class Day9
{
    public static void Solve()
    {
        // read file
        var fileName = "PuzzleInput.txt";
        var input = File.ReadAllText($"Day9\\{fileName}");
        var diskMap = DiskMapService.GetDiskMap(input);

        var solution = Part1.Solve(diskMap);
        Console.WriteLine($"Day9 Part1 Solution: {solution}");

        solution = Part2.Solve(diskMap);
        Console.WriteLine($"Day9 Part2 Solution: {solution}");
    }
}
