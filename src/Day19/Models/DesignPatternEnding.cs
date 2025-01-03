using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day19.Extensions;

namespace AdventOfCode.Day19.Models
{
    public class DesignPatternEnding
    {
        public List<Pattern> PatternEnd { get; set; } = new List<Pattern>();
        public string PatternEndDesign { get; set; }

        public DesignPatternEnding(List<Pattern> patternEnd)
        {
            PatternEnd = patternEnd;
            PatternEndDesign = patternEnd.Design();
        }
    }
}
