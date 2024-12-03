using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Shared;

namespace AdventOfCode.Day1;

public class Day1 : IDay
{
    public void Solve()
    {
        // readInput
        var fileName = "PuzzleInput.txt";
        var input = InputReader.ReadDay1File(fileName);

        var solution = Part1.Solve(input);
        Console.WriteLine($"Day1 Part1 Solution: {solution}");

        solution = Part2.Solve(input);
        Console.WriteLine($"Day1 Part2 Solution: {solution}");
    }
}
