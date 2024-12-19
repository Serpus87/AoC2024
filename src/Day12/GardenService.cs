using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day12.Models;

namespace AdventOfCode.Day12;

public static class GardenService
{
    public static Garden SetupGarden(string[] input)
    {
        var garden = new Garden(input.Length, input[0].Length);

        for (int row = 0; row < garden.NumberOfRows; row++)
        {
            for (int column = 0; column < garden.NumberOfColumns; column++)
            {
                var plot = new Plot(new Position(row, column), input[row][column]);
                garden.Plots[row, column] = plot;
                garden.PlotsAsList.Add(plot);
            }
        }

        SetupPlots(garden);

        return garden;
    }

    public static void SetupPlots(Garden garden)
    {
        var allPlotsHaveHasWalked = false;
        var regionId = 0;
        var counter = 0;

        while (!allPlotsHaveHasWalked)
        {
            if (garden.HasWillWalk())
            {
                counter++;
                //Console.WriteLine($"Setting up Plot number {counter} out of {garden.PlotsAsList.Count} total number of Plots");
                var plotThatWillBeWalkedFrom = garden.PlotsAsList.First(x => x.WalkEnum == WalkEnum.WillWalk);
                WalkInGarden(garden, plotThatWillBeWalkedFrom);
            }
            else
            {
                regionId++;
                garden.PlotsAsList.First(x => x.WalkEnum == WalkEnum.HasNotWalked).RegionId = regionId;
                garden.PlotsAsList.First(x => x.WalkEnum == WalkEnum.HasNotWalked).WalkEnum = WalkEnum.WillWalk;
            }

            allPlotsHaveHasWalked = garden.AllPlotsHaveHasWalked();
        }
    }

    private static void WalkInGarden(Garden garden, Plot plotThatWillBeWalkedFrom)
    {
        var plot1ToTheLeft = garden.GetPlotIfInGarden(plotThatWillBeWalkedFrom.Position.Row, plotThatWillBeWalkedFrom.Position.Column - 1);
        var plot1ToTheAbove = garden.GetPlotIfInGarden(plotThatWillBeWalkedFrom.Position.Row - 1, plotThatWillBeWalkedFrom.Position.Column);
        var plot1ToTheRight = garden.GetPlotIfInGarden(plotThatWillBeWalkedFrom.Position.Row, plotThatWillBeWalkedFrom.Position.Column + 1);
        var plot1ToTheBelow = garden.GetPlotIfInGarden(plotThatWillBeWalkedFrom.Position.Row + 1, plotThatWillBeWalkedFrom.Position.Column);

        var regiondId = plotThatWillBeWalkedFrom.RegionId;
        var numberOfSidesAdjacentToOtherRegion = 4;

        if (plotThatWillBeWalkedFrom.Plant == plot1ToTheLeft?.Plant)
        {
            plot1ToTheLeft.RegionId = regiondId;
            numberOfSidesAdjacentToOtherRegion--;
            plotThatWillBeWalkedFrom.Fences.Remove(FenceEnum.LeftFence);
            if (plot1ToTheLeft.WalkEnum == WalkEnum.HasNotWalked)
            {
                plot1ToTheLeft.WalkEnum = WalkEnum.WillWalk;
            }
        }

        if (plotThatWillBeWalkedFrom.Plant == plot1ToTheAbove?.Plant)
        {
            plot1ToTheAbove.RegionId = regiondId;
            numberOfSidesAdjacentToOtherRegion--;
            plotThatWillBeWalkedFrom.Fences.Remove(FenceEnum.TopFence);
            if (plot1ToTheAbove.WalkEnum == WalkEnum.HasNotWalked)
            {
                plot1ToTheAbove.WalkEnum = WalkEnum.WillWalk;
            }
        }

        if (plotThatWillBeWalkedFrom.Plant == plot1ToTheRight?.Plant)
        {
            plot1ToTheRight.RegionId = regiondId;
            numberOfSidesAdjacentToOtherRegion--;
            plotThatWillBeWalkedFrom.Fences.Remove(FenceEnum.RightFence);
            if (plot1ToTheRight.WalkEnum == WalkEnum.HasNotWalked)
            {
                plot1ToTheRight.WalkEnum = WalkEnum.WillWalk;
            }
        }

        if (plotThatWillBeWalkedFrom.Plant == plot1ToTheBelow?.Plant)
        {
            plot1ToTheBelow.RegionId = regiondId;
            numberOfSidesAdjacentToOtherRegion--;
            plotThatWillBeWalkedFrom.Fences.Remove(FenceEnum.BottomFence);
            if (plot1ToTheBelow.WalkEnum == WalkEnum.HasNotWalked)
            {
                plot1ToTheBelow.WalkEnum = WalkEnum.WillWalk;
            }
        }

        plotThatWillBeWalkedFrom.WalkEnum = WalkEnum.HasWalked;
        plotThatWillBeWalkedFrom.NumberOfSidesAdjacentToOtherRegion = numberOfSidesAdjacentToOtherRegion;
    }

    public static void SetupRegions(Garden garden)
    {
        var regions = new List<Region>();

        var regionIds = garden.PlotsAsList.Select(x => x.RegionId).Distinct().ToList();
        var plotsOfRegion = new List<Plot>();

        foreach (var regionId in regionIds)
        {
            //Console.WriteLine($"Setting up Region {regionId} out of {regionIds.Count} total number of regions");
            plotsOfRegion = garden.PlotsAsList.Where(x => x.RegionId == regionId).ToList();
            regions.Add(new Region(regionId, plotsOfRegion));
        }

        garden.Regions = regions;
    }

    public static void SetRegionsSides(Garden garden)
    {
        var regionCounter = 0;
        foreach (var region in garden.Regions)
        {
            regionCounter++;
            Console.WriteLine($"Setting Sides for Region number {regionCounter} out of {garden.Regions.Count} total number of Regions");
            region.ResetWalking();
            SetRegionSides(region);
        }
    }

    private static void SetRegionSides(Region region)
    {
        var allPlotsHaveHasWalked = false;
        var regionId = 0;
        var sidesCounter = 0;

        while (!allPlotsHaveHasWalked)
        {
            if (region.HasWillWalk())
            {
                var plotThatWillBeWalkedFrom = region.Plots.First(x => x.WalkEnum == WalkEnum.WillWalk);
                sidesCounter += WalkInRegion(region, plotThatWillBeWalkedFrom);
            }
            else
            {
                region.Plots.First(x => x.WalkEnum == WalkEnum.HasNotWalked).WalkEnum = WalkEnum.WillWalk;
            }

            allPlotsHaveHasWalked = region.AllPlotsHaveHasWalked();
        }

        region.SetSides(sidesCounter);
    }

    private static int WalkInRegion(Region region, Plot plotThatWillBeWalkedFrom)
    {
        var surroundingPlotsInRegion = region.GetSurroundingPlots(plotThatWillBeWalkedFrom);
        var plotFences = plotThatWillBeWalkedFrom.Fences;
        var sidesCounter = plotFences.Count;

        foreach ( var surroundingPlot in surroundingPlotsInRegion)
        {
            if(surroundingPlot.WalkEnum == WalkEnum.HasNotWalked)
            {
                surroundingPlot.WalkEnum = WalkEnum.WillWalk;
            }

            if (surroundingPlot.WalkEnum == WalkEnum.HasWalked)
            {
                sidesCounter -= plotFences.Count(x=>surroundingPlot.Fences.Contains(x));
            }
        }

        plotThatWillBeWalkedFrom.WalkEnum = WalkEnum.HasWalked;
        return sidesCounter > 0 ? sidesCounter : 0;

    }

    private static Plot GetNextPlotToCheck(Region region, Plot currentPlot, FenceEnum firstSideToCheck)
    {
        var nextRow = currentPlot.Position.Row;
        var nextColumn = currentPlot.Position.Column;

        switch (firstSideToCheck)
        {
            case FenceEnum.LeftFence:
                nextColumn--;
                break;
            case FenceEnum.TopFence:
                nextRow--;
                break;
            case FenceEnum.RightFence:
                nextColumn++;
                break;
            case FenceEnum.BottomFence:
                nextRow++;
                break;
        }

        return  region.Plots.First(x=>x.Position.Row == nextRow && x.Position.Column == nextColumn);
    }

    private static FenceEnum GetNextSideToCheck(FenceEnum lastSideChecked)
    {
        return lastSideChecked switch
        {
            FenceEnum.LeftFence => FenceEnum.TopFence,
            FenceEnum.TopFence => FenceEnum.RightFence,
            FenceEnum.RightFence => FenceEnum.BottomFence,
            FenceEnum.BottomFence => FenceEnum.LeftFence,
        };
    }

    private static FenceEnum GetPreviousSideToCheck(FenceEnum lastSideChecked)
    {
        return lastSideChecked switch
        {
            FenceEnum.LeftFence => FenceEnum.BottomFence,
            FenceEnum.BottomFence => FenceEnum.RightFence,
            FenceEnum.RightFence => FenceEnum.TopFence,
            FenceEnum.TopFence => FenceEnum.LeftFence,
        };
    }
}
