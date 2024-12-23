using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day15;
using AdventOfCode.Day15.Extensions;
using FluentAssertions;
using FluentAssertions.Execution;

namespace AdventOfCode.Tests;

[TestClass]
public class Day15Tests
{
    [TestMethod]
    public void Part1Solve_Example2_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = 2028;
        var fileName = "Example2.txt";
        var input = File.ReadAllLines($"Day15\\{fileName}");
        var splitInput = WarehouseService.SplitInput(input);

        var warehouse = WarehouseService.GetWarehouse(splitInput.First());
        var robotMoveList = WarehouseService.GetRobotMoveList(splitInput.Last());
        warehouse.Robot.Moves = robotMoveList;

        // Act
        var actualSolution = Part1.Solve(warehouse);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }

    [TestMethod]
    public void Part1Solve_Example1_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = 10092;
        var fileName = "Example1.txt";
        var input = File.ReadAllLines($"Day15\\{fileName}");
        var splitInput = WarehouseService.SplitInput(input);

        var warehouse = WarehouseService.GetWarehouse(splitInput.First());
        var robotMoveList = WarehouseService.GetRobotMoveList(splitInput.Last());
        warehouse.Robot.Moves = robotMoveList;
        warehouse.Boxes.InitializeMoveDirections(warehouse.Map);

        // Act
        var actualSolution = Part1.Solve(warehouse);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }

    [TestMethod]
    public void Part2Solve_Example_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = 0;
        var fileName = "Example1.txt";
        var input = File.ReadAllLines($"Day15\\{fileName}");
        var splitInput = WarehouseService.SplitInput(input);

        var warehouse = WarehouseService.GetWarehouse(splitInput.First());
        var robotMoveList = WarehouseService.GetRobotMoveList(splitInput.Last());
        warehouse.Robot.Moves = robotMoveList;

        // Act
        var actualSolution = Part2.Solve(warehouse);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }
}
