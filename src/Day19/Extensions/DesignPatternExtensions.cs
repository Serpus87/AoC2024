using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day19.Models;

namespace AdventOfCode.Day19.Extensions
{
    public static class DesignPatternExtensions
    {
        public static List<DesignPattern> AddIfNew(this List<DesignPattern> designPatterns, DesignPattern designPatternToAdd)
        {
            if (!designPatterns.Includes(designPatternToAdd))
            {
                designPatterns.Add(designPatternToAdd);
            }

            return designPatterns;
        }

        public static List<DesignPattern> AddIfNew(this List<DesignPattern> designPatterns, List<DesignPattern> designPatternsToAdd)
        {
            foreach (var designPatternToAdd in designPatternsToAdd)
            {
                if (!designPatterns.Includes(designPatternToAdd))
                {
                    designPatterns.Add(designPatternToAdd);
                }
            }

            return designPatterns;
        }

        public static bool Includes(this List<DesignPattern> designPatterns, DesignPattern designPatternToAdd)
        {
            return designPatterns.Any(x => x.Patterns.IsEqual(designPatternToAdd.Patterns));
        }
    }
}
