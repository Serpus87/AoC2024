using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day18.Models;
using AdventOfCode.Day18.Services;

namespace AdventOfCode.Day18
{
    public static class Part1
    {
        public static int Solve(Map map, List<Position> corruptedLocations)
        {
            // initialize map
            MapService.InitializeMap(map, corruptedLocations);

            // walk
            var pathFindersThatReachedTheEnd = MapService.FindShortestPaths(map);

            var result = pathFindersThatReachedTheEnd.Min(x => x.MoveCounter);

            return result;
        }
    }
}
