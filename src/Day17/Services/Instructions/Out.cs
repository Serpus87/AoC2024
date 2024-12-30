using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day17.Services.Instructions;

public static class Out
{
    public static int Opcode { get; set; } = 5;

    public static int Execute(uint? comboOperand)
    {
        if (comboOperand == null)
        {
            throw new ArgumentException("comboOperand cannot be null");
        }

        return (int)((uint)comboOperand % 8);
    }
}
