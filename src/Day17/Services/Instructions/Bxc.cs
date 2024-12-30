﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day17.Services.Instructions;

public static class Bxc
{
    public static int Opcode { get; set; } = 4;

    public static uint Execute(uint registerBValue, uint registerCValue)
    {
        var bitWiseXOR = BinaryService.CalculateBitWiseXOR(registerBValue, registerCValue);

        return (uint)bitWiseXOR;
    }
}