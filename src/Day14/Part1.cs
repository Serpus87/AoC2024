using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day14.Models;

namespace AdventOfCode.Day14;

public static class Part1
{
    public static int Solve(Map map, List<Robot> robots)
    {
        // move robots 100 times
        MapService.MoveRobots(map, robots, 100);

        // get safetyFactor
        var result = map.GetSafetyFactor();

        return result;
    }
}
