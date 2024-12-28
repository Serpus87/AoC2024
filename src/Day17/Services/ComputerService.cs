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
        var programString = string.Empty;
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
                programString = line.Trim(',');
                continue;
            }
        }

        splitInput.Add(registerStrings.ToArray());
        splitInput.Add(programString.ToCharArray().Select(x => x.ToString()).ToArray());

        return splitInput;
    }

    public static List<Register> GetRegisters(string[] input)
    {
        var registers = new List<Register>();

        foreach (var line in input)
        {
            var name = line.Substring(line.IndexOf(' ') + 1, 1);
            var value = int.Parse(line.Substring(line.IndexOf(':') + 2));
            registers.Add(new Register(name, value));
        }

        return registers;
    }

    public static List<int> GetProgramInput(string[] input)
    {
        var programInput = new List<int>();

        for (var i = 0; i < input[0].Length; i++)
        {
            programInput.Add(input[0][i]);
        }

        return programInput;
    }

    public static string ProcessInput(List<int> programInput, List<Register> registers)
    {
        var output = string.Empty;

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
                        shouldJump = false;
                    }
                    break;
                case 4:
                    registerB.Value = Bxc.Execute(operand, registerB.Value, registerC.Value);
                    break;
                case 5:
                    output += Out.Execute(comboOperand);
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

            if (shouldJump)
            {
                i++;
            }
        }

        return output;
    }
}
