using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day4;

public class Cross
{
    public List<Streak> Streaks { get; set; }

    public Cross(List<Streak> streaks)
    {
        Streaks = streaks;
    }

    public bool HasOnlyWordOfInterest(string wordOfInterest)
    {
        return Streaks.All(x=>x.HasWordOfInterest(wordOfInterest));
    }
}
