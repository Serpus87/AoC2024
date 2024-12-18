using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day11;
using FluentAssertions;
using FluentAssertions.Execution;

namespace AdventOfCode.Tests;

[TestClass]
public class Day11Tests
{
    [TestMethod]
    public void Part1Solve_Example1_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = 7;
        var fileName = "Part1Example1.txt";
        var input = File.ReadAllText($"Day11\\{fileName}");
        var stones = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
        var numberOfBlinks = 1;

        // Act
        var actualSolution = Part1.Solve(stones, numberOfBlinks);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }

    [TestMethod]
    public void Part1Solve_Example2_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = 55312;
        var fileName = "Part1Example2.txt";
        var input = File.ReadAllText($"Day11\\{fileName}");
        var stones = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
        var numberOfBlinks = 25;

        // Act
        var actualSolution = Part1.Solve(stones, numberOfBlinks);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }

    [TestMethod]
    public void Part2Solve_Example1_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = 7;
        var fileName = "Part1Example1.txt";
        var input = File.ReadAllText($"Day11\\{fileName}");
        var stones = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
        var numberOfBlinks = 1;

        // Act
        var actualSolution = Part2.Solve(stones, numberOfBlinks);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }

    [TestMethod]
    public void Part2Solve_Example2_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = 55312;
        var fileName = "Part1Example2.txt";
        var input = File.ReadAllText($"Day11\\{fileName}");
        var stones = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
        var numberOfBlinks = 25;

        // Act
        var actualSolution = Part2.Solve(stones, numberOfBlinks);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }
}
