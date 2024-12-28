using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day17.Services.Instructions;

public static class Bst
{
    public static int Opcode { get; set; } = 2;

    public static int Execute(int comboOperant)
    {
        return comboOperant % 8;
    }
}
