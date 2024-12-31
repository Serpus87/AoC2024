using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day18.Models;
using AdventOfCode.Day18.Services;

namespace AdventOfCode.Day18
{
    public static class Part2
    {
        public static Position Solve(Map map, List<Position> corruptedLocations, int startingCorruptedLocationsNumber)
        {
            // initialize map
            MapService.InitializeMap(map, corruptedLocations.Take(startingCorruptedLocationsNumber).ToList());

            // walk
            var result = MapService.FindFirstCorruptedLocationThatBlocksPath(map, corruptedLocations.Skip(startingCorruptedLocationsNumber).ToList());

            return result;
        }
    }
}
