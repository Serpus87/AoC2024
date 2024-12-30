using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day17.Models;
using AdventOfCode.Day17.Services.Instructions;

namespace AdventOfCode.Day17.Services;

public class ComputerService
{
    public static List<string[]> SplitInput(string[] input)
    {
        var splitInput = new List<string[]>();
        var registerStrings = new List<string>();
        var programString = new List<string>(); ;
        var newLineIsFound = false;

        foreach (var line in input)
        {
            if (line.Length == 0)
            {
                newLineIsFound = true;
                continue;
            }
            if (!newLineIsFound)
            {
                registerStrings.Add(line);
                continue;
            }
            if (newLineIsFound)
            {
                programString.Add(line);
                continue;
            }
        }

        splitInput.Add(registerStrings.ToArray());
        splitInput.Add(programString.ToArray());

        return splitInput;
    }

    public static List<Register> GetRegisters(string[] input)
    {
        var registers = new List<Register>();

        foreach (var line in input)
        {
            var name = line.Substring(line.IndexOf(' ') + 1, 1);
            var value = uint.Parse(line.Substring(line.IndexOf(':') + 2));
            registers.Add(new Register(name, value));
        }

        return registers;
    }

    public static List<int> GetProgramInput(string[] input)
    {
        var programInput = new List<int>();

        var inputSubstring = input[0].Substring(input[0].IndexOf(' ') + 1);
        var programNumbers = inputSubstring.Replace(",","");

        for (var i = 0; i < programNumbers.Length; i++)
        {
            programInput.Add((int)Char.GetNumericValue(programNumbers[i]));
        }

        return programInput;
    }

    public static string ProcessInput(List<int> programInput, List<Register> registers, bool outputShouldMatchInput = false)
    {
        var output = new List<int>();

        for (var i = 0; i < programInput.Count; i++)
        {
            var shouldJump = true;
            var opcodeIndex = i;
            var operandIndex = opcodeIndex + 1;

            var opcode = programInput[opcodeIndex];
            var operand = programInput[operandIndex];
            var comboOperand = ComboOperandService.GetComboOperant(operand, registers);

            var registerA = registers.First(x => x.Name == "A");
            var registerB = registers.First(x => x.Name == "B");
            var registerC = registers.First(x => x.Name == "C");

            switch (opcode)
            {
                case 0:
                    registerA.Value = Adv.Execute(comboOperand, registerA.Value);
                    break;
                case 1:
                    registerB.Value = Bxl.Execute(operand, registerB.Value);
                    break;
                case 2:
                    registerB.Value = Bst.Execute(comboOperand);
                    break;
                case 3:
                    var newIndex = Jnz.Execute(operand, registerA.Value, i);
                    if (newIndex != i)
                    {
                        i = newIndex - 1;
                        shouldJump = false;
                    }
                    break;
                case 4:
                    registerB.Value = Bxc.Execute(registerB.Value, registerC.Value);
                    break;
                case 5:
                    var outResult = Out.Execute(comboOperand);
                    output.Add(outResult);
                    break;
                case 6:
                    registerB.Value = Bdv.Execute(comboOperand, registerA.Value);
                    break;
                case 7:
                    registerC.Value = Cdv.Execute(comboOperand, registerA.Value);
                    break;
                default:
                    break;
            }

            if (outputShouldMatchInput && opcode == 5)
            {
                if (output.Count > programInput.Count || output[output.Count - 1] != programInput[output.Count - 1])
                {
                    break;
                }
            }

            if (shouldJump)
            {
                i++;
            }
        }

        var result = string.Join(",", output);
        return result;
    }

    internal static uint FindInitialAValue(List<int> programInput, List<Register> registers)
    {
        var expectedOutput = string.Join(",", programInput);
        var actualOutput = string.Empty;
        //var newAValue = (uint)int.MaxValue;
        var newAValue = 0u;

        while (actualOutput != expectedOutput)
        {
            newAValue++;

            if (newAValue < 0)
            {
                throw new ArgumentOutOfRangeException("newAValue overflow, try ulong instead");
            }

            if (newAValue == 117440u)
            {
                var temp = true;
            }

            var newRegisters = new List<Register>
            {
                new Register("A",newAValue),
                new Register("B",registers.First(x=>x.Name == "B").Value),
                new Register("C",registers.First(x=>x.Name == "C").Value),
            };

            actualOutput = ProcessInput(programInput, newRegisters, true);
        }

        return newAValue;
    }
}
