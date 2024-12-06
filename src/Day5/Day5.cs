using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day5;

public class Day5
{
    public static void Solve()
    {
        // read file
        var input = File.ReadAllLines($"Day5\\PuzzleInput.txt");
        var orderingRules = InputReader.GetOrderingRules(input);
        var pagesToUpdate = InputReader.GetUpdates(input);

        var solution = Part1.Solve(orderingRules, pagesToUpdate);
        Console.WriteLine($"Day5 Part1 Solution: {solution}");

        solution = Part2.Solve(orderingRules, pagesToUpdate);
        Console.WriteLine($"Day5 Part2 Solution: {solution}");
    }
}
