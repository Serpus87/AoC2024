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
        public static List<string> Copy(this List<string> patterns)
        {
            return patterns.Select(x => new string(x)).ToList();
        }

        public static bool IsEqual(this List<string> patterns, List<string> patternsToCheck)
        {
            return patterns.SequenceEqual(patternsToCheck);
        }

        public static string Design(this List<string> patterns)
        {
            var stringBuilder = new StringBuilder();

            foreach (var pattern in patterns)
            {
                stringBuilder.Append(pattern);

            }

            return stringBuilder.ToString();
        }
    }
}
