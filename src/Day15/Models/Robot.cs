using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day15.Models;

public class Robot
{
    public Position Position { get; set; }
    public List<Move> Moves { get; set; } = new List<Move>();

    public Robot(Position position)
    {
        Position = position;
    }
}
