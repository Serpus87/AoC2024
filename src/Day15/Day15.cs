﻿using System;
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
        //var fileName = "PuzzleInput.txt";
        //var fileName = "Part2Example1.txt";
        var fileName = "Example1.txt";
        var input = File.ReadAllLines($"Day15\\{fileName}");
        var splitInput = WarehouseService.SplitInput(input);

        var warehouse = WarehouseService.GetWarehouse(splitInput.First());
        var robotMoveList = WarehouseService.GetRobotMoveList(splitInput.Last());
        warehouse.Robot.Moves = robotMoveList;

        warehouse.Boxes.InitializeMoveDirections(warehouse.Map);

        //-- temp
        warehouse.Map.Print();
        //--

        var solution = Part1.Solve(warehouse);
        Console.WriteLine($"Day15 Part1 Solution: {solution}");

        var wideWarehouse = WarehouseService.GetWideWarehouse(splitInput.First());
        //robotMoveList = WarehouseService.GetRobotMoveList(splitInput.Last());
        wideWarehouse.Robot.Moves = robotMoveList;

        wideWarehouse.WideBoxes.InitializeMoveDirections(wideWarehouse.Map);

        //-- temp
        wideWarehouse.Map.Print();
        //--

        solution = Part2.Solve(wideWarehouse);

        wideWarehouse.Map.Print();
        Console.WriteLine($"Day15 Part2 Solution: {solution}");
    }
}
