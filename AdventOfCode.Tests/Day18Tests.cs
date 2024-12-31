using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day18;
using AdventOfCode.Day18.Models;
using AdventOfCode.Day18.Services;

namespace AdventOfCode.Tests
{
    [TestClass]
    public class Day18Tests
    {
        [TestMethod]
        public void Part1_Example_ReturnsExpectedSolution()
        {
            // Arrange
            var expectedSolution = 22;

            var fileName = "Example.txt";
            var input = File.ReadAllLines($"Day18\\{fileName}");

            var map = new Map(7, 7);
            var corruptedLocations = MapService.GetCorruptedLocations(input);
            corruptedLocations = corruptedLocations.Take(12).ToList();

            // Act
            var actualSolution = Part1.Solve(map, corruptedLocations);

            // Assert
            Assert.AreEqual(expectedSolution, actualSolution);
        }

        [TestMethod]
        public void Part2_Example_ReturnsExpectedSolution()
        {
            // Arrange
            var expectedSolution = new Position(1, 6);

            var fileName = "Example.txt";
            var input = File.ReadAllLines($"Day18\\{fileName}");

            var map = new Map(7, 7);
            var corruptedLocations = MapService.GetCorruptedLocations(input);

            // Act
            var actualSolution = Part2.Solve(map, corruptedLocations, 12);

            // Assert
            Assert.AreEqual(expectedSolution.Row, actualSolution.Row);
            Assert.AreEqual(expectedSolution.Column, actualSolution.Column);
        }
    }
}
