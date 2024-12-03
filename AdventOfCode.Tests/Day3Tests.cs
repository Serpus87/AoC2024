using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day3;

namespace AdventOfCode.Tests;

[TestClass]
public class Day3Tests
{
    [TestMethod]
    public void Part1Solve_Example_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = 161;
        var input = InputReader.Read("Example.txt");

        // Act
        var actualSolution = Part1.Solve(input);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }

    [TestMethod]
    public void Part2Solve_Example_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = 48;
        var input = InputReader.Read("ExamplePart2.txt");

        // Act
        var actualSolution = Part2.Solve(input);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }
}
