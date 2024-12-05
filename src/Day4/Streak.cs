using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day4;

public class Streak
{
    public Coordinate XCoordinate { get; set; }
    public Coordinate MCoordinate { get; set; }
    public Coordinate ACoordinate { get; set; }
    public Coordinate SCoordinate { get; set; }

    public Streak(Coordinate xCoordinate, Coordinate mCoordinate, Coordinate aCoordinate, Coordinate sCoordinate)
    {
        XCoordinate = xCoordinate;
        MCoordinate = mCoordinate;
        ACoordinate = aCoordinate;
        SCoordinate = sCoordinate;
    }

    public bool StreakSpellsXmas()
    {
        return MCoordinate.IsActualMarkExpectedMark() && ACoordinate.IsActualMarkExpectedMark() && SCoordinate.IsActualMarkExpectedMark(); 
    }
}
