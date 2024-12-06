using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day5;

namespace AdventOfCode.Tests;

[TestClass]
public class Day5Tests
{
    [TestMethod]
    public void Part1Solve_Example_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = 143;
        var fileName = "Example.txt";

        var input = File.ReadAllLines($"Day5\\{fileName}");
        var orderingRules = InputReader.GetOrderingRules(input);
        var updates = InputReader.GetUpdates(input);

        // Act
        var actualSolution = Part1.Solve(orderingRules, updates);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }

    [TestMethod]
    public void Part2Solve_Example_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = 123;
        var fileName = "Example.txt";

        var input = File.ReadAllLines($"Day5\\{fileName}");
        var orderingRules = InputReader.GetOrderingRules(input);
        var updates = InputReader.GetUpdates(input);

        // Act
        var actualSolution = Part2.Solve(orderingRules, updates);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }
}
