using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day15.Extensions;

namespace AdventOfCode.Day15;

public static class Day15
{
    public static void Solve()
    {
        // read file
        var fileName = "PuzzleInput.txt";
        var input = File.ReadAllLines($"Day15\\{fileName}");
        var splitInput = WarehouseService.SplitInput(input);

        var warehouse = WarehouseService.GetWarehouse(splitInput.First());
        var robotMoveList = WarehouseService.GetRobotMoveList(splitInput.Last());
        warehouse.Robot.Moves = robotMoveList;
        warehouse.Boxes.SetMoveDirections(warehouse.Map);

        var solution = Part1.Solve(warehouse);
        Console.WriteLine($"Day14 Part1 Solution: {solution}");

        solution = Part2.Solve(warehouse);
        Console.WriteLine($"Day14 Part2 Solution: {solution}");
    }
}
