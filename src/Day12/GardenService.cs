using System;
using System.Collections.Generic;
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
                garden.Plots[row, column] = new Plot(new Position(row, column), input[row][column]);
            }
        }

        SetupPlots(garden);

        return garden;
    }

    public static void SetupPlots(Garden garden)
    {
        var allPlotsHaveHasWalked = false;
        var gardenHasWillWalk = false;
        var regionId = 0;

        while (!allPlotsHaveHasWalked)
        {
            for (int row = 0; row < garden.NumberOfRows; row++)
            {
                for (int column = 0; column < garden.NumberOfColumns; column++)
                {
                    if (garden.Plots[row, column].WalkEnum == WalkEnum.HasWalked) 
                    {
                        continue;
                    }

                    if (!garden.HasWillWalk())
                    {
                        regionId++;
                        garden.Plots[row, column].WalkEnum = WalkEnum.WillWalk;
                        garden.Plots[row, column].RegionId = regionId;
                    }
                    if (garden.Plots[row, column].WalkEnum == WalkEnum.WillWalk)
                    {
                        WalkInRegion(garden, row, column);
                    }
                }
            }

            allPlotsHaveHasWalked = garden.AllPlotsHaveHasWalked();
        }
    }

    private static void WalkInRegion(Garden garden, int row, int column)
    {
        var currentPlot = garden.Plots[row, column];
        var plot1ToTheLeft = garden.GetPlotIfInGarden(row, column - 1);
        var plot1ToTheAbove = garden.GetPlotIfInGarden(row - 1, column);
        var plot1ToTheRight = garden.GetPlotIfInGarden(row, column + 1);
        var plot1ToTheBelow = garden.GetPlotIfInGarden(row + 1, column);

        var regiondId = currentPlot.RegionId;
        var numberOfSidesAdjacentToOtherRegion = 4;

        if (currentPlot.Plant == plot1ToTheLeft?.Plant)
        {
            plot1ToTheLeft.RegionId = regiondId;
            numberOfSidesAdjacentToOtherRegion--;
            if (plot1ToTheLeft.WalkEnum == WalkEnum.HasNotWalked)
            {
                plot1ToTheLeft.WalkEnum = WalkEnum.WillWalk;
            }
        }

        if (currentPlot.Plant == plot1ToTheAbove?.Plant)
        {
            plot1ToTheAbove.RegionId = regiondId;
            numberOfSidesAdjacentToOtherRegion--;
            if (plot1ToTheAbove.WalkEnum == WalkEnum.HasNotWalked)
            {
                plot1ToTheAbove.WalkEnum = WalkEnum.WillWalk;
            }
        }

        if (currentPlot.Plant == plot1ToTheRight?.Plant)
        {
            plot1ToTheRight.RegionId = regiondId;
            numberOfSidesAdjacentToOtherRegion--;
            if (plot1ToTheRight.WalkEnum == WalkEnum.HasNotWalked)
            {
                plot1ToTheRight.WalkEnum = WalkEnum.WillWalk;
            }
        }

        if (currentPlot.Plant == plot1ToTheBelow?.Plant)
        {
            plot1ToTheBelow.RegionId = regiondId;
            numberOfSidesAdjacentToOtherRegion--;
            if (plot1ToTheBelow.WalkEnum == WalkEnum.HasNotWalked)
            {
                plot1ToTheBelow.WalkEnum = WalkEnum.WillWalk;
            }
        }

        currentPlot.WalkEnum = WalkEnum.HasWalked;
        currentPlot.NumberOfSidesAdjacentToOtherRegion = numberOfSidesAdjacentToOtherRegion;
    }

    public static void SetupRegions(Garden garden)
    {
        var regions = new List<Region>();

        var regionIds = garden.GetPlotRegionIds();
        var plotsOfRegion = new List<Plot>();

        foreach (var regionId in regionIds)
        {
            plotsOfRegion = garden.GetPlots(regionId);
            regions.Add(new Region(regionId, plotsOfRegion));
        }

        garden.Regions = regions;
    }
}
