using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day19.Extensions;

namespace AdventOfCode.Day19.Models
{
    public class DesignPatternStart
    {
        public List<string> PatternStart { get; set; } = new List<string>();
        public string PatternStartDesign { get; set; }

        public DesignPatternStart(List<string> patternStart)
        {
            PatternStart = patternStart;
            PatternStartDesign = patternStart.Design();
        }
    }
}
