using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day15.Extensions;
using AdventOfCode.Day15.Models;

namespace AdventOfCode.Day15;

public static class Part1
{
    public static int Solve(Warehouse warehouse)
    {
        // move robot
        WarehouseService.MakeAllRobotMoves(warehouse);

        // get GPS
        var result = warehouse.Boxes.GetGPSSum();

        return result;
    }
}
