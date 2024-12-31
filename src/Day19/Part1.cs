using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day19.Models;

namespace AdventOfCode.Day19
{
    public class Part1
    {
        public static int Solve(List<Design> designs, List<Pattern> patterns)
        {
            var allPossibleDesigns = TowelService.FindDesignsThatCanBeMade(designs, patterns);

            var result = allPossibleDesigns.Count;

            return result;
        }
    }
}
