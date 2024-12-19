using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day13.Models;

namespace AdventOfCode.Day13;

public static class Part1
{
    public static int Solve(List<ClawMachine> machines)
    {
        var result = ArcadeService.PlayMachines(machines);

        return result;
    }
}
