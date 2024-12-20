using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day13;

namespace AdventOfCode.Day13;

public static class Day13
{
    public static void Solve()
    {
        // read file
        var fileName = "PuzzleInput.txt";
        var input = File.ReadAllLines($"Day13\\{fileName}");
        var machines = ArcadeService.SetupMachines(input);

        var solution = Part1.Solve(machines);
        Console.WriteLine($"Day10 Part1 Solution: {solution}");

        var longSolution = Part2.Solve(machines);
        Console.WriteLine($"Day10 Part2 Solution: {longSolution}");
    }
}
