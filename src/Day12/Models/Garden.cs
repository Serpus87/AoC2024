using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day4;
using static System.Reflection.Metadata.BlobBuilder;

namespace AdventOfCode.Day12.Models;

public class Garden
{
    public int NumberOfRows { get; init; }
    public int NumberOfColumns { get; init; }
    public Plot[,] Plots { get; init; }
    public List<Plot> PlotsAsList {  get; init; } = new List<Plot>();
    public List<Region> Regions { get; set; } = new List<Region>();

    public Garden(int numberOfRows, int numberOfColumns)
    {
        NumberOfRows = numberOfRows;
        NumberOfColumns = numberOfColumns;
        Plots = new Plot[numberOfRows, numberOfColumns]; 
    }

    public Plot? GetPlotIfInGarden(int row, int column)
    {
        if (!IsPlotInGarden(row, column))
        {
            return null;
        }

        return Plots[row, column];
    }

    private bool IsPlotInGarden(int row, int column)
    {
        return row >= 0 && row < NumberOfRows && column >= 0 && column < NumberOfColumns;
    }

    internal bool HasWillWalk()
    {
        return PlotsAsList.Any(x=>x.WalkEnum == WalkEnum.WillWalk);
    }

    internal bool AllPlotsHaveHasWalked()
    {
        return PlotsAsList.All(x => x.WalkEnum == WalkEnum.HasWalked);
    }
}
