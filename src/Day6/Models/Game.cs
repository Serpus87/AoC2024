using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day6.Models;

public class Game
{
    public Map Map { get; set; }
    public Guard Guard { get; set; }

    public Game(Map map, Guard guard)
    {
        Map = map;
        Guard = guard;
    }

    public Game Copy()
    {
        var map = new Map(Map.NRows, Map.NColumns);
        var guard = new Guard(Guard.Position);

        for (int row = 0; row < Map.NRows; row++) {
            for (int column = 0; column < map.NColumns; column++)
            {
                map.Fields[row,column] = new Field(new Position(row, column), Map.Fields[row,column].Fill);
            }
        }

        return new Game(map, guard);
    }
}
