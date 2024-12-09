using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day8.Models;

namespace AdventOfCode.Day8.Services;

public static class AntennaService
{
    public static List<AntennaPair> GetAntennaPairs(List<List<Antenna>> antennaGroups)
    {
        var antennaPairs = new List<AntennaPair>();

        foreach (var antennaGroup in antennaGroups)
        {
            if (antennaGroup.Count == 0) {
                continue;
            }
            antennaPairs.AddRange(PairAntennas(antennaGroup));
        }

        return antennaPairs;
    }

    internal static List<Position> GetAntinodes(List<AntennaPair> antennaPairs)
    {
        var antiNodes = new List<Position>();

        foreach (var antennaPair in antennaPairs)
        {
            var rowDifference = antennaPair.Antenna1.Position.Row - antennaPair.Antenna2.Position.Row;
            var columnDifference = antennaPair.Antenna1.Position.Column - antennaPair.Antenna2.Position.Column;

            antiNodes.Add(new Position(antennaPair.Antenna1.Position.Row + rowDifference, antennaPair.Antenna1.Position.Column + columnDifference));
            antiNodes.Add(new Position(antennaPair.Antenna2.Position.Row - rowDifference, antennaPair.Antenna2.Position.Column - columnDifference));
        }

        return antiNodes;
    }

    internal static List<Position> GetResonantAntinodes(List<AntennaPair> antennaPairs, Map map)
    {
        var antiNodes = new List<Position>();

        foreach (var antennaPair in antennaPairs)
        {
            var rowDifference = antennaPair.Antenna1.Position.Row - antennaPair.Antenna2.Position.Row;
            var columnDifference = antennaPair.Antenna1.Position.Column - antennaPair.Antenna2.Position.Column;

            var newRow = antennaPair.Antenna1.Position.Row;
            var newColumn = antennaPair.Antenna1.Position.Column;
            var isOnMap = newRow >= 0 && newRow < map.NRows && newColumn >= 0 && newColumn < map.NColumns;

            while (isOnMap)
            {
                antiNodes.Add(new Position(newRow, newColumn));
                newRow = newRow + rowDifference; 
                newColumn = newColumn + columnDifference;
                isOnMap = newRow >= 0 && newRow < map.NRows && newColumn >= 0 && newColumn < map.NColumns;
            }

            newRow = antennaPair.Antenna2.Position.Row;
            newColumn = antennaPair.Antenna2.Position.Column;
            isOnMap = newRow >= 0 && newRow < map.NRows && newColumn >= 0 && newColumn < map.NColumns;

            while (isOnMap)
            {
                antiNodes.Add(new Position(newRow, newColumn));
                newRow = newRow - rowDifference;
                newColumn = newColumn - columnDifference;
                isOnMap = newRow >= 0 && newRow < map.NRows && newColumn >= 0 && newColumn < map.NColumns;
            }
        }

        return antiNodes;
    }

    internal static List<List<Antenna>> MakeAntennaGroups(List<Antenna> antennas)
    {
        var frequencies = antennas.Select(x=>x.Frequency).Distinct();

        var antennagroups = new List<List<Antenna>>();

        foreach (var frequency in frequencies) {
            antennagroups.Add(antennas.Where(x => x.Frequency == frequency).ToList());
        }


        return antennagroups;
    }

    private static IEnumerable<AntennaPair> PairAntennas(List<Antenna> antennaGroup)
    {
        var antennaPairs = new List<AntennaPair>();

        for (int firstNumberIndex = 0; firstNumberIndex < (antennaGroup.Count-1); firstNumberIndex++)
        {
            for (int secondNumberIndex = 1; secondNumberIndex < antennaGroup.Count; secondNumberIndex++)
            {
                if (secondNumberIndex <= firstNumberIndex)
                {
                    continue;
                }

                antennaPairs.Add(new AntennaPair(antennaGroup[firstNumberIndex],antennaGroup[secondNumberIndex]));
                
            }
        }

        return antennaPairs;
    }
}
