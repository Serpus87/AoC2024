using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day17.Services.Instructions;

public static class Bxl
{
    public static int Opcode { get; set; } = 1;

    public static uint Execute(int operand, uint registerBValue)
    {
        var bitWiseXOR = BinaryService.CalculateBitWiseXOR(registerBValue, (uint)operand);

        return bitWiseXOR;
    }
}
