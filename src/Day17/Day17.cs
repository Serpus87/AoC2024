using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day17.Services;

namespace AdventOfCode.Day17;

public static class Day17
{
    public static void Solve()
    {
        // read file
        var fileName = "PuzzleInput.txt";
        var input = File.ReadAllLines($"Day17\\{fileName}");
        var splitInput = ComputerService.SplitInput(input);

        var registers = ComputerService.GetRegisters(splitInput.First());
        var programInput = ComputerService.GetProgramInput(splitInput.Last());

        var solution = Part1.Solve(registers, programInput);
        Console.WriteLine($"Day17 Part1 Solution: {solution}");

        var newRegisters = ComputerService.GetRegisters(splitInput.First());
        var partTwoSolution = Part2.Solve(newRegisters, programInput);
        Console.WriteLine($"Day17 Part2 Solution: {partTwoSolution}");
    }
}
