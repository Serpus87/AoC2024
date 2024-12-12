using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day6.Models;
using AdventOfCode.Day10;
using FluentAssertions;
using FluentAssertions.Execution;

namespace AdventOfCode.Tests;

[TestClass]
public class Day10Tests
{
    [TestMethod]
    public void Part1Solve_Example1_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = 1;
        var fileName = "Example1.txt";
        var input = File.ReadAllLines($"Day10\\{fileName}");
        var map = MapService.GetMap(input);

        // Act
        var actualSolution = Part1.Solve(map);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }

    [TestMethod]
    public void Part1Solve_Example2_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = 36;
        var fileName = "Example2.txt";
        var input = File.ReadAllLines($"Day10\\{fileName}");
        var map = MapService.GetMap(input);

        // Act
        var actualSolution = Part1.Solve(map);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }

    [TestMethod]
    public void Part2Solve_Example_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = 36;
        var fileName = "Example1.txt";
        var input = File.ReadAllLines($"Day10\\{fileName}");
        var map = MapService.GetMap(input);

        // Act
        var actualSolution = Part2.Solve(map);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }
}
