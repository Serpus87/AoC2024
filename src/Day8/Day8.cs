using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day8;

public static class Day8
{
    public static void Solve()
    {
        // read file
        var fileName = "PuzzleInput.txt";
        var input = InputReader.GetInput(fileName);

        var map = input.Map;
        var antennas = input.Antennas;

        var solution = Part1.Solve(map, antennas);
        Console.WriteLine($"Day8 Part1 Solution: {solution}");

        solution = Part2.Solve(map, antennas);
        Console.WriteLine($"Day8 Part2 Solution: {solution}");
    }
}
