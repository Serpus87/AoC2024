using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day4;

public class Day4
{
    public static void Solve()
    {
        // readInput
        var fileName = "PuzzleInput.txt";

        var solution = Part1.Solve(fileName);
        Console.WriteLine($"Day4 Part1 Solution: {solution}");

        //solution = Part2.Solve(fileName);
        //Console.WriteLine($"Day4 Part2 Solution: {solution}");
    }
}
