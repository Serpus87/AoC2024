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
        public List<Pattern> PatternStart { get; set; } = new List<Pattern>();
        public string PatternStartDesign { get; set; }

        public DesignPatternStart(List<Pattern> patternStart)
        {
            PatternStart = patternStart;
            PatternStartDesign = patternStart.Design();
        }
    }
}
