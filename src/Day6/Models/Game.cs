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
}
