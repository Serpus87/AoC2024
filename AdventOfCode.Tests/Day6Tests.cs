using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day6;
using AdventOfCode.Day6.Services;

namespace AdventOfCode.Tests;

[TestClass]
public class Day6Tests
{
    [TestMethod]
    public void Part1Solve_Example_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = 41;
        var fileName = "Example.txt";

        var input = File.ReadAllLines($"Day6\\{fileName}");

        var gameService = new GameService();
        var game = gameService.SetUpGame(input);

        // Act
        var actualSolution = Part1.Solve(game, gameService);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }

    [TestMethod]
    public void Part2Solve_Example_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = 6;
        var fileName = "Example.txt";

        var input = File.ReadAllLines($"Day6\\{fileName}");

        var gameService = new GameService();
        var game = gameService.SetUpGame(input);

        // Act
        var actualSolution = Part2.Solve(game, gameService);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }
}
