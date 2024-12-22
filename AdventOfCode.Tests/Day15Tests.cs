using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day15;
using AdventOfCode.Day15.Models;
using FluentAssertions;
using FluentAssertions.Execution;

namespace AdventOfCode.Tests;

[TestClass]
public class Day15Tests
{
    //[TestMethod]
    //public void Part1Solve_Example_ReturnsExpectedSolution()
    //{
    //    // Arrange
    //    var expectedSolution = 12;
    //    var fileName = "Example.txt";
    //    var input = File.ReadAllLines($"Day14\\{fileName}");

    //    var exampleMapNRows = 7;
    //    var exampleMapNColumns = 11;

    //    var robots = MapService.GetRobotsFromFile(input);
    //    var map = MapService.SetupMap(exampleMapNRows, exampleMapNColumns, robots);

    //    // Act
    //    var actualSolution = Part1.Solve(map, robots);

    //    // Assert
    //    Assert.AreEqual(expectedSolution, actualSolution);
    //}

    //[DataTestMethod]
    //[DataRow(2,2,1,1,0,0)]
    //[DataRow(0,0,-1,-1,2,2)]
    //[DataRow(0,2,-1,1,2,0)]
    //[DataRow(2,0,1,-1,0,2)]
    //public void GetNextLocation_NextLocationNeedsTeleport_ReturnsExpectedLocation(int startRow, int startColumn, int moveRow, int moveColumn, int expectedRow, int expectedColumn)
    //{
    //    // Arrange
    //    var expectedSolution = new Location(expectedRow, expectedColumn);

    //    var map = new Day14.Models.Map(3, 3);
    //    var robot = new Robot(new Location(startRow, startColumn), new Location(moveRow, moveColumn));

    //    // Act
    //    var actualSolution = map.GetNextLocation(robot);

    //    // Assert
    //    using (new AssertionScope())
    //    {
    //        actualSolution.Row.Should().Be(expectedSolution.Row);
    //        actualSolution.Column.Should().Be(expectedSolution.Column);
    //    }
    //}


    //[TestMethod]
    //public void Part2Solve_Example_ReturnsExpectedSolution()
    //{
    //    // Arrange
    //    var expectedSolution = 0;
    //    var fileName = "Example.txt";
    //    var input = File.ReadAllLines($"Day14\\{fileName}");

    //    var exampleMapNRows = 7;
    //    var exampleMapNColumns = 11;

    //    var robots = MapService.GetRobotsFromFile(input);
    //    var map = MapService.SetupMap(exampleMapNRows, exampleMapNColumns, robots);

    //    // Act
    //    var actualSolution = Part2.Solve(map, robots);

    //    // Assert
    //    Assert.AreEqual(expectedSolution, actualSolution);
    //}
}
