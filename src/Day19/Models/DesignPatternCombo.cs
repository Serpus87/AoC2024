using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day19.Extensions;

namespace AdventOfCode.Day19.Models;

public class DesignPatternCombo
{
    public List<string> ComboStart { get; set; } = new List<string>();
    public string ComboStartDesign { get; set; }
    public List<string> ComboEnd { get; set; } = new List<string>();
    public string ComboEndDesign { get; set; }

    public DesignPatternCombo(List<string> comboStart, List<string> comboEnd)
    {
        ComboStart = comboStart;
        ComboStartDesign = comboStart.Design();
        ComboEnd = comboEnd;
        ComboEndDesign = comboEnd.Design();
    }
}
