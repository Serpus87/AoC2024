using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day19;
using AdventOfCode.Day19.Models;
using FluentAssertions;

namespace AdventOfCode.Tests
{
    [TestClass]
    public class Day19Tests
    {
        [TestMethod]
        public void Part1_Example_ReturnsExpectedSolution()
        {
            // Arrange
            var expectedSolution = 6;

            var fileName = "Example.txt";
            var input = File.ReadAllLines($"Day19\\{fileName}");
            var splitInput = TowelService.SplitInput(input);

            var patterns = TowelService.GetPatterns(splitInput.First()[0]);
            var designs = TowelService.GetDesigns(splitInput.Last());

            // Act
            var actualSolution = Part1.Solve(designs, patterns);

            // Assert
            Assert.AreEqual(expectedSolution, actualSolution);
        }

        [TestMethod]
        public void Part2_Example_ReturnsExpectedSolution()
        {
            // Arrange
            var expectedSolution = 16;

            var fileName = "Example.txt";
            var input = File.ReadAllLines($"Day19\\{fileName}");
            var splitInput = TowelService.SplitInput(input);

            var patterns = TowelService.GetPatterns(splitInput.First()[0]);
            var designs = TowelService.GetDesigns(splitInput.Last());

            var part1Solution = Part1.Solve(designs, patterns);

            // Act
            var actualSolution = Part2.Solve(designs, patterns);

            // Assert
            Assert.AreEqual(expectedSolution, actualSolution);
        }

        [TestMethod]
        public void Part2_Example1_ReturnsExpectedSolution()
        {
            // Arrange
            var minimumExpectedSolution = 841;

            var fileName = "Part2Example1.txt";
            var input = File.ReadAllLines($"Day19\\{fileName}");
            var splitInput = TowelService.SplitInput(input);

            var patterns = TowelService.GetPatterns(splitInput.First()[0]);
            var designs = TowelService.GetDesigns(splitInput.Last());

            var part1Solution = Part1.Solve(designs, patterns);

            // Act
            var actualSolution = Part2.Solve(designs, patterns);

            // Assert
            actualSolution.Should().BeGreaterThan(minimumExpectedSolution);
        }

        [TestMethod]
        public void Part2_ExtraExample1_ReturnsExpectedSolution()
        {
            // Arrange
            var expectedSolution = 4;

            var patterns = new List<string> { "r", "rr", "rrr", };
            var designs = new List<Design> { new Design("rrr") };

            var part1Solution = Part1.Solve(designs, patterns);

            // Act
            var actualSolution = Part2.Solve(designs, patterns);

            // Assert
            Assert.AreEqual(expectedSolution, actualSolution);
        }

        [TestMethod]
        public void Part2_ExtraExample2_ReturnsExpectedSolution()
        {
            // Arrange
            var expectedSolution = 7;

            var patterns = new List<string> { "r", "rr", "rrr", };
            var designs = new List<Design> { new Design("rrrr") };

            var part1Solution = Part1.Solve(designs, patterns);

            // Act
            var actualSolution = Part2.Solve(designs, patterns);

            // Assert
            Assert.AreEqual(expectedSolution, actualSolution);
        }

        [TestMethod]
        public void Part2_ExtraExample3_ReturnsExpectedSolution()
        {
            // Arrange
            var expectedSolution = 4;

            var patterns = new List<string> { "r", "b", "rb", "br", "rbr", };
            var designs = new List<Design> { new Design("rbr") };

            var part1Solution = Part1.Solve(designs, patterns);

            // Act
            var actualSolution = Part2.Solve(designs, patterns);

            // Assert
            Assert.AreEqual(expectedSolution, actualSolution);
        }
    }
}
