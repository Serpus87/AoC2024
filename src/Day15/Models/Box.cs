using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day15.Models;

public class Box
{
    public Position Position { get; set; }
    public List<Move> PossibleMoves { get; set; } = new List<Move>();

    public Box(Position position)
    {
        Position = position;
    }
}
