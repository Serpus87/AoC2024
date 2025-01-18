using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day20.Models;
using AdventOfCode.Day20.Services;

namespace AdventOfCode.Day20
{
    public static class Part1
    {
        public static int Solve(Map map)
        {
            MapService.RunWithoutCheating(map);

            var cheats = MapService.GetCheats(map);

            var result = cheats.Count(x=>x.TimeSaved >= 100);


            // first try 1378; answer is too high
            return result;
        }
    }
}
