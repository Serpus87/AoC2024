using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day17;
using AdventOfCode.Day17.Models;
using AdventOfCode.Day17.Services;
using FluentAssertions;
using FluentAssertions.Execution;

namespace AdventOfCode.Tests;

[TestClass]
public class Day17Tests
{
    [TestMethod]
    public void ProcessInput_SmallExample1_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedRegisterBValue = 1u;

        var registers = new List<Register>
        {
            new Register("A",0),
            new Register("B",0),
            new Register("C",9),
        };

        var programInput = new List<int>
        {
            2,6
        };

        // Act
        var processedInput = ComputerService.ProcessInput(programInput, registers);

        // Assert
        using (new AssertionScope())
        {
            processedInput.Length.Should().Be(0);
            registers.First(x => x.Name == "B").Value.Should().Be(expectedRegisterBValue);
        }
    }

    [TestMethod]
    public void ProcessInput_SmallExample2_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedResult = "0,1,2";

        var registers = new List<Register>
        {
            new Register("A",10),
            new Register("B",0),
            new Register("C",0),
        };

        var programInput = new List<int>
        {
            5,0,5,1,5,4
        };

        // Act
        var result = ComputerService.ProcessInput(programInput, registers);

        // Assert
        using (new AssertionScope())
        {
            result.Should().Be(expectedResult);
        }
    }

    [TestMethod]
    public void ProcessInput_SmallExample3_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedResult = "4,2,5,6,7,7,7,7,3,1,0";
        var expectedRegisterAValue = 0u;

        var registers = new List<Register>
        {
            new Register("A",2024),
            new Register("B",0),
            new Register("C",0),
        };

        var programInput = new List<int>
        {
            0,1,5,4,3,0
        };

        // Act
        var result = ComputerService.ProcessInput(programInput, registers);

        // Assert
        using (new AssertionScope())
        {
            result.Should().Be(expectedResult);
            registers.First(x => x.Name == "A").Value.Should().Be(expectedRegisterAValue);
        }
    }


    [TestMethod]
    public void ProcessInput_SmallExample4_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedRegisterBValue = 26u;

        var registers = new List<Register>
        {
            new Register("A",0),
            new Register("B",29),
            new Register("C",0),
        };

        var programInput = new List<int>
        {
            1,7
        };

        // Act
        var processedInput = ComputerService.ProcessInput(programInput, registers);

        // Assert
        using (new AssertionScope())
        {
            processedInput.Length.Should().Be(0);
            registers.First(x => x.Name == "B").Value.Should().Be(expectedRegisterBValue);
        }
    }

    [TestMethod]
    public void ProcessInput_SmallExample5_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedRegisterBValue = 44354u;

        var registers = new List<Register>
        {
            new Register("A",0),
            new Register("B",2024),
            new Register("C",43690),
        };

        var programInput = new List<int>
        {
            4,0
        };

        // Act
        var processedInput = ComputerService.ProcessInput(programInput, registers);

        // Assert
        using (new AssertionScope())
        {
            processedInput.Length.Should().Be(0);
            registers.First(x => x.Name == "B").Value.Should().Be(expectedRegisterBValue);
        }
    }

    [TestMethod]
    public void Part1Solve_Example_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = "4,6,3,5,6,3,5,2,1,0";

        var fileName = "Example.txt";
        var input = File.ReadAllLines($"Day17\\{fileName}");
        var splitInput = ComputerService.SplitInput(input);

        var registers = ComputerService.GetRegisters(splitInput.First());
        var programInput = ComputerService.GetProgramInput(splitInput.Last());

        // Act
        var actualSolution = Part1.Solve(registers, programInput);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }

    [TestMethod]
    public void Part2Solve_Example_ReturnsExpectedSolution()
    {
        // Arrange
        var expectedSolution = 117440u;

        var fileName = "ExamplePart2.txt";
        var input = File.ReadAllLines($"Day17\\{fileName}");
        var splitInput = ComputerService.SplitInput(input);

        var registers = ComputerService.GetRegisters(splitInput.First());
        var programInput = ComputerService.GetProgramInput(splitInput.Last());

        // Act
        var actualSolution = Part2.Solve(registers, programInput);

        // Assert
        Assert.AreEqual(expectedSolution, actualSolution);
    }
}
