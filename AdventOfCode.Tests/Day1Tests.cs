using AdventOfCode.Day1;

namespace AdventOfCode.Tests;

[TestClass]
public sealed class Day1Tests
{
    [TestMethod]
    public void Part1Solve_Example_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = 11;
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
        var expectedSolution = 31;
        var input = InputReader.Read("Example.txt");

        // Act
        var actualSolution = Part2.Solve(input);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }
}
