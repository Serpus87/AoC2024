using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day6.Models;
using AdventOfCode.Day9;
using FluentAssertions;
using FluentAssertions.Execution;

namespace AdventOfCode.Tests;

[TestClass]
public class Day9Tests
{
    [TestMethod]
    public void Part1Solve_Example_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = 1928;
        var fileName = "Example.txt";
        var input = File.ReadAllText($"Day9\\{fileName}");
        var diskMap = DiskMapService.GetDiskMap(input);

        // Act
        var actualSolution = Part1.Solve(diskMap);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }

    [TestMethod]
    public void Part2Solve_Example_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = 2858;
        var fileName = "Example.txt";
        var input = File.ReadAllText($"Day9\\{fileName}");
        var diskMap = DiskMapService.GetDiskMap(input);

        // Act
        var actualSolution = Part2.Solve(diskMap);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }
}
