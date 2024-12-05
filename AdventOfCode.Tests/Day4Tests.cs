using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day4;

namespace AdventOfCode.Tests;

[TestClass]
public class Day4Tests
{
    [TestMethod]
    public void Part1Solve_Example_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = 18;
        var fileName = "Example.txt";

        // Act
        var actualSolution = Part1.Solve(fileName);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }

    [TestMethod]
    public void Part2Solve_Example_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = 9;
        var fileName = "Example.txt";

        // Act
        var actualSolution = Part2.Solve(fileName);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }
}
