using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day20.Services;

namespace AdventOfCode.Day20
{
    public static class Day20
    {
        public static void Solve()
        {
            var fileName = "PuzzleInput.txt";
            //var fileName = "Example.txt";

            var input = File.ReadAllLines($"Day20\\{fileName}");

            var map = MapService.GetMap(input);

            var result = Part1.Solve(map);
            Console.WriteLine($"Part1 solution = {result}");

            map = MapService.GetMap(input);
            result = Part2.Solve(map);
            Console.WriteLine($"Part1 solution = {result}");
        }
    }
}
