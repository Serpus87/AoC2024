using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day4;

public class Cross
{
    public Streak Streak1 { get; set; }
    public Streak Streak2 { get; set; }

    public Cross(Streak streak1, Streak streak2)
    {
        Streak1 = streak1;
        Streak2 = streak2;
    }

    public bool HasOnlyWordOfInterest(string wordOfInterest)
    {
        return Streak1.HasWordOfInterest(wordOfInterest) && Streak2.HasWordOfInterest(wordOfInterest);
    }
}
