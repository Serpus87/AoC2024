using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day17.Models;

namespace AdventOfCode.Day17.Services.Instructions;

public static class Adv
{
    public static int Opcode { get; set; } = 0;

    public static int Execute(int comboOperant, int registerAValue)
    {
        var numerator = registerAValue;
        var denominator = Math.Pow(comboOperant, 2);

        return (int)(numerator / denominator);
    }
}
