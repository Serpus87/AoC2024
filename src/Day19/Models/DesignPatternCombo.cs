using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day19.Models;

public class DesignPatternCombo
{
    public List<Pattern> ComboStart { get; set; } = new List<Pattern>();
    public List<Pattern> ComboEnd { get; set; } = new List<Pattern>();

    public DesignPatternCombo(List<Pattern> comboStart, List<Pattern> comboEnd)
    {
        ComboStart = comboStart;
        ComboEnd = comboEnd;
    }
}
