using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day8;

namespace AdventOfCode.Tests;

[TestClass]
public class Day8Tests
{
    [TestMethod]
    public void Part1Solve_Example_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = 14;
        var input = InputReader.GetInput("Example.txt");

        // Act
        var actualSolution = Part1.Solve(input.Map, input.Antennas);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }

    //[TestMethod]
    //public void Part2Solve_Example_ReturnsExpectedSolution()
    //{
    //    // Arrange
    //    var expectedSolution = 11387;
    //    var input = InputReader.GetInput("Example.txt");

    //    // Act
    //    var actualSolution = Part2.Solve(input.Map, input.Antennas);

    //    // Assert
    //    Assert.AreEqual(expectedSolution, actualSolution);
    //}
}
