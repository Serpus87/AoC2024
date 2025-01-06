using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day19.Extensions;

namespace AdventOfCode.Day19.Models
{
    public class DesignPattern
    {
        public List<string> Patterns { get; set; } = new List<string>();
        public string Design { get; set; } = string.Empty;

        public DesignPattern()
        {
            
        }

        public DesignPattern(List<string> patterns)
        {
            Patterns = patterns;
            Design = patterns.Design();
        }

        public DesignPattern(List<string> patterns, string pattern)
        {
            Patterns = patterns;
            Patterns.Add(pattern);
            Design = Patterns.Design();
        }
    }
}
