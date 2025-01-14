using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day19.Models;

namespace AdventOfCode.Day19
{
    public static class Day19
    {
        public static void Solve()
        {
            var fileName = "PuzzleInput.txt";
            var input = File.ReadAllLines($"Day19\\{fileName}");
            var splitInput = TowelService.SplitInput(input);

            var patterns = TowelService.GetPatterns(splitInput.First()[0]);
            var designs = TowelService.GetDesigns(splitInput.Last());

            var result = Part1.Solve(designs, patterns);
            Console.WriteLine($"Part1 solution = {result}");

            // todo make this work
            result = Part2.Solve(designs, patterns);
            Console.WriteLine($"Part2 solution = {result}");
        }
    }
}
