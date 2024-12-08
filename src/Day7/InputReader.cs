using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day7;

public static class InputReader
{
    public static Input GetInput(string fileName)
    {
        var input = new Input();
        string[] lines = File.ReadAllLines($"Day7\\{fileName}");

        foreach (var line in lines)
        {
            var equationString = line.Split(':');
            var testValue = int.Parse(equationString[0]);
            var numbersString = equationString[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var numbers = numbersString.Select(int.Parse).ToList();

            var equation = new Equation(testValue, numbers);
            input.Equations.Add(equation);
        }

        return input;
    }
}
