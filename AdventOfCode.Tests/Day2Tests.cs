using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day2;

namespace AdventOfCode.Tests;

[TestClass]
public class Day2Tests
{
    [TestMethod]
    public void Part1Solve_Example_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = 2;
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
        var expectedSolution = 4;
        var input = InputReader.Read("Example.txt");

        // Act
        var actualSolution = Part2.Solve(input);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }

    [DataTestMethod]
    [DataRow(4,1,2,3,true)]
    [DataRow(1,4,2,3,true)]
    [DataRow(1,2,4,3,true)]
    [DataRow(2,3,4,1,true)]
    [DataRow(4,1,3,2,false)]
    [DataRow(2,1,4,3,false)]
    public void Part2Solve_PrivateExample_ReturnsExpectedSolution(int level1, int level2, int level3, int level4, bool isSafe)
    {
        // Arrange
        var expectedResult = isSafe ? 1 : 0;
        var levels = new List<int> { level1, level2, level3, level4 };
        var input = new Input();
        input.Reports.Add(new Report(levels));

        // Act
        var result = Part2.Solve(input);

        // Assert
        Assert.AreEqual(result, expectedResult);
    }
}
