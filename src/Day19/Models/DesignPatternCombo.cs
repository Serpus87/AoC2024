using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day19.Extensions;

namespace AdventOfCode.Day19.Models;

public class DesignPatternCombo
{
    public List<Pattern> ComboStart { get; set; } = new List<Pattern>();
    public string ComboStartDesign { get; set; }
    public List<Pattern> ComboEnd { get; set; } = new List<Pattern>();
    public string ComboEndDesign { get; set; }

    public DesignPatternCombo(List<Pattern> comboStart, List<Pattern> comboEnd)
    {
        ComboStart = comboStart;
        ComboStartDesign = comboStart.Design();
        ComboEnd = comboEnd;
        ComboEndDesign = comboEnd.Design();
    }
}
