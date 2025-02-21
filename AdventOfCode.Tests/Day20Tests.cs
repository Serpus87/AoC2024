using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day20;
using AdventOfCode.Day20.Models;
using AdventOfCode.Shared.Models;
using AdventOfCode.Day20.Services;
using FluentAssertions;
using FluentAssertions.Execution;

namespace AdventOfCode.Tests;

[TestClass]
public class Day20Tests
{
    [TestMethod]
    public void Part1_Example_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = 0;

        var fileName = "Example.txt";
        var input = File.ReadAllLines($"Day20\\{fileName}");

        var map = MapService.GetMap(input);

        // Act
        var actualSolution = Part1.Solve(map);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }

    [TestMethod]
    public void GetCheatFromPosition_Example_ReturnsExpectedCheat()
    {
        // Arrange
        var expectedCheatStart = new Position(1, 8);
        var expectedCheatEnd = new Position(1, 9);
        var expectedTimeSaved = 12;

        var fileName = "Example.txt";
        var input = File.ReadAllLines($"Day20\\{fileName}");

        var map = MapService.GetMap(input);
        MapService.RunWithoutCheating(map);

        var positionFromRunWithoutCheat = new Position(1, 7);

        // Act
        var cheat = MapService.GetCheatsFromPosition(map, positionFromRunWithoutCheat);

        // Assert
        using (new AssertionScope())
        {
            cheat.Count.Should().Be(1);
            cheat.First().OriginalRunStartPosition.Row.Should().Be(positionFromRunWithoutCheat.Row);
            cheat.First().OriginalRunStartPosition.Column.Should().Be(positionFromRunWithoutCheat.Column);
            cheat.First().Start.Row.Should().Be(expectedCheatStart.Row);
            cheat.First().Start.Column.Should().Be(expectedCheatStart.Column);
            cheat.First().End.Row.Should().Be(expectedCheatEnd.Row);
            cheat.First().End.Column.Should().Be(expectedCheatEnd.Column);
            cheat.First().TimeSaved.Should().Be(expectedTimeSaved);
        }
    }

    [TestMethod]
    public void GetCheats_Example_ReturnsExpectedCheats()
    {
        // Arrange
        var expectedNumberOfCheats = 44;

        var fileName = "Example.txt";
        var input = File.ReadAllLines($"Day20\\{fileName}");

        var map = MapService.GetMap(input);
        MapService.RunWithoutCheating(map);

        // Act
        var cheats = MapService.GetCheats(map);

        // Assert
        using (new AssertionScope())
        {
            cheats.Count.Should().Be(expectedNumberOfCheats);
            cheats.Count(x=>x.TimeSaved == 2).Should().Be(14);
            cheats.Count(x=>x.TimeSaved == 4).Should().Be(14);
            cheats.Count(x=>x.TimeSaved == 6).Should().Be(2);
            cheats.Count(x=>x.TimeSaved == 8).Should().Be(4);
            cheats.Count(x=>x.TimeSaved == 10).Should().Be(2);
            cheats.Count(x=>x.TimeSaved == 12).Should().Be(3);
            cheats.Count(x=>x.TimeSaved == 20).Should().Be(1);
            cheats.Count(x=>x.TimeSaved == 36).Should().Be(1);
            cheats.Count(x=>x.TimeSaved == 38).Should().Be(1);
            cheats.Count(x=>x.TimeSaved == 40).Should().Be(1);
            cheats.Count(x=>x.TimeSaved == 64).Should().Be(1);
        }
    }

    [TestMethod]
    public void RunWithoutCheating_Example_ReturnsExpectedStartAndEndScore()
    {
        // Arrange
        var expectedMapStart = 0;
        var expectedMapEnd = 84;

        var fileName = "Example.txt";
        var input = File.ReadAllLines($"Day20\\{fileName}");

        // Act
        var map = MapService.GetMap(input);
        MapService.RunWithoutCheating(map);

        // Assert
        using (new AssertionScope())
        {
            map.Fields[map.Start.Row, map.Start.Column].PicoSecondsFromStart.Should().Be(expectedMapStart);
            map.Fields[map.End.Row, map.End.Column].PicoSecondsFromStart.Should().Be(expectedMapEnd);
        }
    }

    [TestMethod]
    public void GetCheatsPart2_Example_ReturnsExpectedCheats()
    {
        // Arrange
        var expectedNumberOfCheats = 285;

        var fileName = "Example.txt";
        var input = File.ReadAllLines($"Day20\\{fileName}");

        var map = MapService.GetMap(input);
        MapService.RunWithoutCheating(map);

        // Act
        var cheats = MapService.GetCheats(map,20);

        // Assert
        using (new AssertionScope())
        {
            cheats.Count(x => x.TimeSaved >= 50).Should().Be(expectedNumberOfCheats);
            cheats.Count(x => x.TimeSaved == 50).Should().Be(32);
            cheats.Count(x => x.TimeSaved == 52).Should().Be(31);
            cheats.Count(x => x.TimeSaved == 54).Should().Be(29);
            cheats.Count(x => x.TimeSaved == 56).Should().Be(39);
            cheats.Count(x => x.TimeSaved == 58).Should().Be(25);
            cheats.Count(x => x.TimeSaved == 60).Should().Be(23);
            cheats.Count(x => x.TimeSaved == 62).Should().Be(20);
            cheats.Count(x => x.TimeSaved == 64).Should().Be(19);
            cheats.Count(x => x.TimeSaved == 66).Should().Be(12);
            cheats.Count(x => x.TimeSaved == 68).Should().Be(14);
            cheats.Count(x => x.TimeSaved == 70).Should().Be(12);
            cheats.Count(x => x.TimeSaved == 72).Should().Be(22);
            cheats.Count(x => x.TimeSaved == 74).Should().Be(4);
            cheats.Count(x => x.TimeSaved == 76).Should().Be(3);
        }
    }
}
