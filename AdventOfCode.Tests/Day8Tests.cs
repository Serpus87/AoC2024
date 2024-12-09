using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day6.Models;
using AdventOfCode.Day8;
using FluentAssertions;
using FluentAssertions.Execution;

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

    [TestMethod]
    public void Part1Solve_Example_ReturnsExpectedMap()
    {
        // Arrange
        var expectedMap = new List<string>();
        expectedMap.Add("......#....#");
        expectedMap.Add("...#....0...");
        expectedMap.Add("....#0....#.");
        expectedMap.Add("..#....0....");
        expectedMap.Add("....0....#..");
        expectedMap.Add(".#....A.....");
        expectedMap.Add("...#........");
        expectedMap.Add("#......#....");
        expectedMap.Add("........A...");
        expectedMap.Add(".........A..");
        expectedMap.Add("..........#.");
        expectedMap.Add("..........#.");


        var input = InputReader.GetInput("Example.txt");

        // Act
        var actualSolution = Part1.Solve(input.Map, input.Antennas);

        // Assert
        using(new AssertionScope())
        {
            for(int i = 0; i < input.Map.NRows; i++)
            {
                for (int j = 0; j < input.Map.NColumns; j++)
                {
                    input.Map.Fields[i, j].Fill.Should().Be(expectedMap[i][j]);
                }
            }
        }
    }

    [TestMethod]
    public void Part2Solve_Example_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = 34;
        var input = InputReader.GetInput("Example.txt");

        // Act
        var actualSolution = Part2.Solve(input.Map, input.Antennas);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }

    [TestMethod]
    public void Part2Solve_Example_ReturnsExpectedMap()
    {
        // Arrange
        var expectedMap = new List<string>();
        expectedMap.Add("##....#....#");
        expectedMap.Add(".#.#....0...");
        expectedMap.Add("..#.#0....#.");
        expectedMap.Add("..##...0....");
        expectedMap.Add("....0....#..");
        expectedMap.Add(".#...#A....#");
        expectedMap.Add("...#..#.....");
        expectedMap.Add("#....#.#....");
        expectedMap.Add("..#.....A...");
        expectedMap.Add("....#....A..");
        expectedMap.Add(".#........#.");
        expectedMap.Add("...#......##");


        var input = InputReader.GetInput("Example.txt");

        // Act
        var actualSolution = Part2.Solve(input.Map, input.Antennas);

        // Assert
        using (new AssertionScope())
        {
            for (int i = 0; i < input.Map.NRows; i++)
            {
                for (int j = 0; j < input.Map.NColumns; j++)
                {
                    input.Map.Fields[i, j].Fill.Should().Be(expectedMap[i][j]);
                }
            }
        }
    }
}
