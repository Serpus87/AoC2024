using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day17.Models;

namespace AdventOfCode.Day17.Services;

public static class ComboOperandService
{
    public static int GetComboOperant(int operand, List<Register> registers)
    {
        return operand switch
        {
            0 => 0,
            1 => 1,
            2 => 2,
            3 => 3,
            4 => registers.First(x => x.Name == "A").Value,
            5 => registers.First(x => x.Name == "B").Value,
            6 => registers.First(x => x.Name == "C").Value,
            _ => throw new NotImplementedException(),
        };
    }
}
