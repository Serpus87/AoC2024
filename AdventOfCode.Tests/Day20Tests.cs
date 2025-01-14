using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day20;
using AdventOfCode.Day20.Models;
using AdventOfCode.Day20.Services;
using FluentAssertions;
using FluentAssertions.Execution;

namespace AdventOfCode.Tests
{
    [TestClass]
    public class Day20Tests
    {
        [TestMethod]
        public void Part1_Example_ReturnsExpectedSolution()
        {
            // Arrange
            var expectedSolution = 0;

            var fileName = "PuzzleInput.txt";
            var input = File.ReadAllLines($"Day20\\{fileName}");

            var map = MapService.GetMap(input);

            var actualSolution = Part1.Solve(map);

            // Assert
            Assert.AreEqual(expectedSolution, actualSolution);
        }
    }
}
