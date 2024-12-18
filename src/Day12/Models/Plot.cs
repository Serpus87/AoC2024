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
    public List<FenceEnum> Fences { get; set; } = new List<FenceEnum> { FenceEnum.LeftFence, FenceEnum.TopFence, FenceEnum.RightFence, FenceEnum.BottomFence };
    public List<FenceEnum> FencesThatHaveBeenCheckedForRegionSides { get; set; } = new List<FenceEnum>();
    public int NumberOfSidesAdjacentToOtherRegion { get; set; }
    public WalkEnum WalkEnum { get; set; } = WalkEnum.HasNotWalked;

    public Plot(Position position, char plant)
    {
        Position = position;
        Plant = plant;
    }
}
