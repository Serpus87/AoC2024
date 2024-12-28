using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day16;
using FluentAssertions;
using FluentAssertions.Execution;

namespace AdventOfCode.Tests;

[TestClass]
public class Day16Tests
{
    [TestMethod]
    public void Part1Solve_Example1_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = 7036;
        var fileName = "Example1.txt";
        var input = File.ReadAllLines($"Day16\\{fileName}");
        var maze = MazeService.GetMaze(input);

        // Act
        var actualSolution = Part1.Solve(maze);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }

    [TestMethod]
    public void Part1Solve_Example2_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = 11048;
        var fileName = "Example2.txt";
        var input = File.ReadAllLines($"Day16\\{fileName}");
        var maze = MazeService.GetMaze(input);

        // Act
        var actualSolution = Part1.Solve(maze);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }

    [TestMethod]
    public void Part2Solve_Example1_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = 45;
        var fileName = "Example1.txt";
        var input = File.ReadAllLines($"Day16\\{fileName}");
        var maze = MazeService.GetMaze(input);

        // Act
        var actualSolution = Part2.Solve(maze);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }

    [TestMethod]
    public void Part2Solve_Example2_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = 64;
        var fileName = "Example2.txt";
        var input = File.ReadAllLines($"Day16\\{fileName}");
        var maze = MazeService.GetMaze(input);

        // Act
        var actualSolution = Part2.Solve(maze);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }
}
