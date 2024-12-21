using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day14;
using FluentAssertions;
using FluentAssertions.Execution;

namespace AdventOfCode.Tests;

[TestClass]
public class Day14Tests
{
    [TestMethod]
    public void Part1Solve_Example_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = 12;
        var fileName = "Example.txt";
        var input = File.ReadAllLines($"Day14\\{fileName}");

        var exampleMapNRows = 7;
        var exampleMapNColumns = 11;

        var robots = MapService.GetRobotsFromFile(input);
        var map = MapService.SetupMap(exampleMapNRows, exampleMapNColumns, robots);

        // Act
        var actualSolution = Part1.Solve(map, robots);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }

    [TestMethod]
    public void Part2Solve_Example_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = 0;
        var fileName = "Example.txt";
        var input = File.ReadAllLines($"Day14\\{fileName}");

        var exampleMapNRows = 7;
        var exampleMapNColumns = 11;

        var robots = MapService.GetRobotsFromFile(input);
        var map = MapService.SetupMap(exampleMapNRows, exampleMapNColumns, robots);

        // Act
        var actualSolution = Part2.Solve(map, robots);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }
}
