using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day13;
using FluentAssertions;
using FluentAssertions.Execution;

namespace AdventOfCode.Tests;

[TestClass]
public class Day13Tests
{
    [TestMethod]
    public void Part1Solve_ExampleMachine1_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = 280;
        var fileName = "ExampleMachine1.txt";
        var input = File.ReadAllLines($"Day13\\{fileName}");
        var garden = ArcadeService.SetupMachines(input);

        // Act
        var actualSolution = Part1.Solve(garden);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }

    [TestMethod]
    public void Part1Solve_ExampleMachine2_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = 0;
        var fileName = "ExampleMachine2.txt";
        var input = File.ReadAllLines($"Day13\\{fileName}");
        var garden = ArcadeService.SetupMachines(input);

        // Act
        var actualSolution = Part1.Solve(garden);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }

    [TestMethod]
    public void Part1Solve_ExampleMachine3_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = 200;
        var fileName = "ExampleMachine3.txt";
        var input = File.ReadAllLines($"Day13\\{fileName}");
        var garden = ArcadeService.SetupMachines(input);

        // Act
        var actualSolution = Part1.Solve(garden);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }

    [TestMethod]
    public void Part1Solve_ExampleMachine4_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = 0;
        var fileName = "ExampleMachine4.txt";
        var input = File.ReadAllLines($"Day13\\{fileName}");
        var garden = ArcadeService.SetupMachines(input);

        // Act
        var actualSolution = Part1.Solve(garden);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }

    [TestMethod]
    public void Part1Solve_Example_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = 480;
        var fileName = "Example.txt";
        var input = File.ReadAllLines($"Day13\\{fileName}");
        var garden = ArcadeService.SetupMachines(input);

        // Act
        var actualSolution = Part1.Solve(garden);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }

    [TestMethod]
    public void Part2Solve_ExampleMachine1_ReturnsExpectedSolution()
    {
        // Arrange
        ulong expectedSolution = 0;
        var fileName = "ExampleMachine1.txt";
        var input = File.ReadAllLines($"Day13\\{fileName}");
        var garden = ArcadeService.SetupMachines(input);

        // Act
        var actualSolution = Part2.Solve(garden);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }

    [TestMethod]
    public void Part2Solve_ExampleMachine2_ShouldBeGreaterThanZero()
    {
        // Arrange
        var fileName = "ExampleMachine2.txt";
        var input = File.ReadAllLines($"Day13\\{fileName}");
        var garden = ArcadeService.SetupMachines(input);

        // Act
        var actualSolution = Part2.Solve(garden);

        // Assert
        actualSolution.Should().BeGreaterThan(0);
    }

    [TestMethod]
    public void Part2Solve_ExampleMachine3_ReturnsExpectedSolution()
    {
        // Arrange
        ulong expectedSolution = 0;
        var fileName = "ExampleMachine3.txt";
        var input = File.ReadAllLines($"Day13\\{fileName}");
        var garden = ArcadeService.SetupMachines(input);

        // Act
        var actualSolution = Part2.Solve(garden);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }

    [TestMethod]
    public void Part2Solve_ExampleMachine4_ShouldBeGreaterThanZero()
    {
        // Arrange
        var expectedSolution = 0;
        var fileName = "ExampleMachine4.txt";
        var input = File.ReadAllLines($"Day13\\{fileName}");
        var garden = ArcadeService.SetupMachines(input);

        // Act
        var actualSolution = Part2.Solve(garden);

        // Assert
        actualSolution.Should().BeGreaterThan(0);
    }
}
