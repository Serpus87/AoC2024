using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day7;

public static class Day7
{
    public static void Solve()
    {
        // read file
        var fileName = "PuzzleInput.txt";
        var input = InputReader.GetInput(fileName);

        var solution = Part1.Solve(input);
        Console.WriteLine($"Day7 Part1 Solution: {solution}");

        solution = Part2.Solve(input);
        Console.WriteLine($"Day7 Part2 Solution: {solution}");
    }

}
