using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day12.Models;

public class Region
{
    public int Id { get; set; }
    public List<Plot> Plots { get; set; }
    public int Area { get; set; }
    public int Perimeter { get; set; }
    public int Price { get; set; }
    public int Sides { get; set; }
    public int BulkDiscountPrice { get; set; }

    public Region(int id, List<Plot> plots)
    {
        Id = id;
        Plots = plots;
        Area = plots.Count;
        Perimeter = plots.Sum(x => x.Fences.Count);
        Price = Area * Perimeter;
    }

    public void SetSides(int sides)
    {
        Sides = sides;
        BulkDiscountPrice = Sides * Area;
    }

    public Plot? GetPlotIfInRegion(int row, int column)
    {
        return Plots.FirstOrDefault(x => x.Position.Row == row && x.Position.Column == column);
    }

    internal bool HasWillWalk()
    {
        return Plots.Any(x => x.WalkEnum == WalkEnum.WillWalk);
    }

    internal bool AllPlotsHaveHasWalked()
    {
        return Plots.All(x => x.WalkEnum == WalkEnum.HasWalked);
    }

    internal void ResetWalking()
    {
        foreach (var plot in Plots)
        {
            plot.WalkEnum = WalkEnum.HasNotWalked;
        }
    }

    internal List<Plot> GetSurroundingPlots(Plot plotThatWillBeWalkedFrom)
    {
        var surroundingPlots = new List<Plot>();

        var directions = new List<(int row, int column)> { (0, -1), (-1, 0), (0, 1), (1, 0) };

        foreach (var direction in directions) 
        {
            var newPosition = new Position(plotThatWillBeWalkedFrom.Position.Row + direction.row, plotThatWillBeWalkedFrom.Position.Column + direction.column);
            var plot = GetPlotIfInRegion(newPosition.Row, newPosition.Column);

            if (plot != null)
            {
                surroundingPlots.Add(plot);
            }
        }

        return surroundingPlots;
    }
}
