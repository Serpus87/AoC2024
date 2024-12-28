using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day17.Services.Instructions;

public static class Jnz
{
    public static int Opcode { get; set; } = 3;

    public static int Execute(int operand, int registerAValue, int i)
    {
        if (registerAValue == 0)
        {
            return i;
        }

        return operand;
    }
}
