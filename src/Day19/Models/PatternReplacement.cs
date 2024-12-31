using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day19.Models
{
    public class PatternReplacement
    {
        public List<Pattern> OriginalPatterns { get; set; } = new List<Pattern>();
        public List<Pattern> NewPatterns { get; set; } = new List<Pattern>();
    }
}
