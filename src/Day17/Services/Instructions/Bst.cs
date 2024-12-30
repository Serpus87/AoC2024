using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day17.Services.Instructions;

public static class Bst
{
    public static int Opcode { get; set; } = 2;

    public static uint Execute(uint? comboOperand)
    {
        if (comboOperand == null)
        {
            throw new ArgumentException("comboOperand cannot be null");
        }

        return (uint)comboOperand % 8;
    }
}
