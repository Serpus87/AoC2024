using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day18.Services;
using AdventOfCode.Day18.Models;

namespace AdventOfCode.Day18
{
    public static class Day18
    {
        public static void Solve()
        {
            var fileName = "PuzzleInput.txt";
            var input = File.ReadAllLines($"Day18\\{fileName}");

            var map = new Map(71, 71);
            var corruptedLocations = MapService.GetCorruptedLocations(input);

            var result = Part1.Solve(map, corruptedLocations.Take(1024).ToList());
            Console.WriteLine($"Part1 solution = {result}");

            var resultPart2 = Part2.Solve(map, corruptedLocations, 1024);
            Console.WriteLine($"Part1 solution = {resultPart2.Column},{resultPart2.Row}");
        }
    }
}
