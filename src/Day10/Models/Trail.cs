using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day10.Models;

public class Trail
{
    public List<Position> Positions { get; set; } = new List<Position>();

    public Trail(Position startingPosition)
    {
        Positions.Add(startingPosition);
    }
}
