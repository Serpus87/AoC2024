using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day17.Services.Instructions;

public static  class Cdv
{
    public static int Opcode { get; set; } = 7;

    public static uint Execute(uint? comboOperand, uint registerAValue)
    {
        if (comboOperand == null)
        {
            throw new ArgumentException("comboOperand cannot be null");
        }

        var numerator = registerAValue;
        var denominator = Math.Pow(2, (uint)comboOperand);

        return (uint)(int)(numerator / denominator);
    }
}
