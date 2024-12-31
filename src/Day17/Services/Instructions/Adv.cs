﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day17.Models;

namespace AdventOfCode.Day17.Services.Instructions;

public static class Adv
{
    public static int Opcode { get; set; } = 0;

    public static int Execute(int? comboOperand, int registerAValue)
    {
        if (comboOperand == null)
        {
            throw new ArgumentException("comboOperand cannot be null");
        }

        var numerator = registerAValue;
        var denominator = Math.Pow(2, (int)comboOperand);

        return (int)(numerator / denominator);
    }
}
