using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day12;
using FluentAssertions;
using FluentAssertions.Execution;

namespace AdventOfCode.Tests;

[TestClass]
public class Day12Tests
{
    [TestMethod]
    public void Part1Solve_Example1_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = 140;
        var fileName = "Example1.txt";
        var input = File.ReadAllLines($"Day12\\{fileName}");
        var garden = GardenService.SetupGarden(input);

        // Act
        var actualSolution = Part1.Solve(garden);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }

    [TestMethod]
    public void Part1Solve_Example2_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = 772;
        var fileName = "Example2.txt";
        var input = File.ReadAllLines($"Day12\\{fileName}");
        var garden = GardenService.SetupGarden(input);

        // Act
        var actualSolution = Part1.Solve(garden);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }

    [TestMethod]
    public void Part1Solve_Example3_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = 1930;
        var fileName = "Example3.txt";
        var input = File.ReadAllLines($"Day12\\{fileName}");
        var garden = GardenService.SetupGarden(input);

        // Act
        var actualSolution = Part1.Solve(garden);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }

    [TestMethod]
    public void Part2Solve_Example1_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = 80;
        var fileName = "Example1.txt";
        var input = File.ReadAllLines($"Day12\\{fileName}");
        var garden = GardenService.SetupGarden(input);

        // Act
        var actualSolution = Part2.Solve(garden);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }
}
