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

    internal List<int> GetPlotRegionIds()
    {
        var plotRegionIds = new List<int>();

        for (int i = 0; i < NumberOfRows; i++)
        {
            for (int j = 0; j < NumberOfColumns; j++)
            {
                plotRegionIds.Add((int)Plots[i,j].RegionId!);
            }
        }

        return plotRegionIds.Distinct().ToList();
    }

    internal List<Plot> GetPlots(int regionId)
    {
        var plots = new List<Plot>();

        for (int i = 0; i < NumberOfRows; i++)
        {
            for (int j = 0; j < NumberOfColumns; j++)
            {
                if (Plots[i,j].RegionId == regionId)
                {
                    plots.Add(Plots[i, j]);
                };
            }
        }

        return plots;
    }

    internal bool HasWillWalk()
    {
        for (int i = 0; i < NumberOfRows; i++)
        {
            for (int j = 0; j < NumberOfColumns; j++)
            {
                if (Plots[i, j].WalkEnum == WalkEnum.WillWalk)
                {
                    return true;
                };
            }
        }

        return false;
    }

    internal bool AllPlotsHaveHasWalked()
    {
        for (int i = 0; i < NumberOfRows; i++)
        {
            for (int j = 0; j < NumberOfColumns; j++)
            {
                if (Plots[i, j].WalkEnum != WalkEnum.HasWalked)
                {
                    return false;
                };
            }
        }

        return true;
    }
}
