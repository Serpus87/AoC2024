using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day19.Models;

namespace AdventOfCode.Day19
{
    public static class Part2
    {
        public static int Solve(List<Design> designs, List<Pattern> patterns)
        {
            var designsThatCanBeMade = designs.Where(x => x.CanBeMade).ToList();

            TowelService.FindAlternativeDesignsForDesignsThatCanBeMade(designsThatCanBeMade, patterns);

            var result = designsThatCanBeMade.Sum(x=>x.DesignPatterns.Count);

            // first try: 263258; incorrect -> too low
            return result;
        }
    }
}
