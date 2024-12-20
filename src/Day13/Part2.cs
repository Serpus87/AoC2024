using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day13.Models;

namespace AdventOfCode.Day13;

public static class Part2
{
    public static ulong Solve(List<ClawMachine> machines)
    {
        // recalibrate prize locations
        ArcadeService.RecalibratePrizeLocations(machines);


        var result = ArcadeService.SolveWithoutPlayingMachines(machines);

        return result;
    }
}
