using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day17.Models;
using AdventOfCode.Day17.Services;

namespace AdventOfCode.Day17;

public static class Part1
{
    public static string Solve(List<Register> registers, List<int> programInput)
    {
        var result = ComputerService.ProcessInput(programInput,registers);

        return result;
    }
}
