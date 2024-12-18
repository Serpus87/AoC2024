using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day12.Models;

public class Plot
{
    public Position Position { get; init; }
    public char Plant { get; init; }
    public int RegionId { get; set; }
    public int NumberOfSidesAdjacentToOtherRegion { get; set; }
    public WalkEnum WalkEnum { get; set; } = WalkEnum.HasNotWalked;

    public Plot(Position position, char plant)
    {
        Position = position;
        Plant = plant;
    }
}
