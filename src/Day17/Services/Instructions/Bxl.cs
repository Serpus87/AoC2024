using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day17.Services.Instructions;

public static class Bxl
{
    public static int Opcode { get; set; } = 1;

    public static int Execute(int operant, int registerBValue)
    {
        var bitWiseXOR = CalculateBitWiseXOR(registerBValue, operant);

        return bitWiseXOR;
    }
}
