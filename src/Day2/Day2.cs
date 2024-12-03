using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Shared;

namespace AdventOfCode.Day2;

public class Day2 : IDay
{
    public void Solve()
    {
        // readInput
        var fileName = "PuzzleInput.txt";
        var input = InputReader.ReadDay2File(fileName);


        var solution = Part1.Solve(input);
        Console.WriteLine($"Day2 Part1 Solution: {solution}");

        solution = Part2.Solve(input);
        Console.WriteLine($"Day2 Part2 Solution: {solution}");
    }
}
