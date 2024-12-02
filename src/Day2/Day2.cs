using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day2;

namespace AdventOfCode.Day2;

public class Day2
{
    public static void Solve()
    {
        // readInput
        var fileName = "PuzzleInput.txt";
        var input = InputReader.Read(fileName);

        var solution = Part1.Solve(input);
        Console.WriteLine($"Day2 Part1 Solution: {solution}");

        //solution = Part2.Solve(input);
        //Console.WriteLine($"Day1 Part2 Solution: {solution}");
    }
}
