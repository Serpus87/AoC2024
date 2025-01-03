using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day19.Models;

namespace AdventOfCode.Day19.Extensions
{
    public static class PatternExtensions
    {
        public static List<Pattern> Copy(this List<Pattern> patterns)
        {
            return patterns.Select(x => new Pattern(x.Colors)).ToList();

            var copy = new List<Pattern>();

            foreach (var pattern in patterns)
            {
                copy.Add(new Pattern(pattern.Colors));
            }

            return copy;
        }

        public static bool IsEqual(this List<Pattern> patterns, List<Pattern> patternsToCheck)
        {
            return patterns.Select(x => x.Colors).SequenceEqual(patternsToCheck.Select(x => x.Colors));
        }

        public static string Design(this List<Pattern> patterns)
        {
            var design = string.Empty;

            foreach (var pattern in patterns) 
            {
                design += pattern.Colors;
            }

            return design;
        }
    }
}
