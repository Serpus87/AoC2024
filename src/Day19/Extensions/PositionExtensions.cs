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
            var copy = new List<Pattern>();

            foreach (var pattern in patterns)
            {
                copy.Add(new Pattern(pattern.Colors));
            }

            return copy;
        }
    }
}
