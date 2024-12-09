using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day8.Models;

namespace AdventOfCode.Day8.Services;

public static class MapService
{
    public static void SetAntiNodes(Map map, List<Position> antinodes)
    {
        foreach (var position in antinodes)
        {
            var isOnMap = position.Row >= 0 && position.Row < map.NRows && position.Column >= 0 && position.Column < map.NColumns;

            if (isOnMap) 
            {
                map.Fields[position.Row, position.Column].HasAntiNode = true;

                if (map.Fields[position.Row, position.Column].Fill == '.')
                {
                    map.Fields[position.Row, position.Column].Fill = '#';
                }
            }
        }
    }

    // improve this
    public static int CountAntiNodes(Map map)
    {
        var counter = 0;

        for (int i = 0; i < map.NRows; i++)
        {
            for (int j = 0; j < map.NColumns; j++)
            {
                if(map.Fields[i, j].HasAntiNode)
                {
                    counter++;
                }
            }
        }

        return counter;
    }
}
