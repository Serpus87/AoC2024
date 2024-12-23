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

    internal void MakeMove(Move move, Map map)
    {
        map.Fields[Position.Row, Position.Column].Fill = '.';
        Position = new Position(Position.Row + move.Vertical, Position.Column + move.Horizontal);
        map.Fields[Position.Row, Position.Column].Fill = '@';
    }
}
