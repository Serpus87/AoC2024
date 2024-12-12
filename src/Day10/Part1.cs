using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day10.Models;

namespace AdventOfCode.Day10;

public static class Part1
{

    public static int Solve(Map map)
    {
        // foreach trailHead walk trails
        MapService.GetTrailHeadsScores(map);

        // add scores
        var result = map.TrailHeads.Sum(x => x.Score);

        return result;
    }
}
