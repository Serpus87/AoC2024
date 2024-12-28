using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day16.Models;

namespace AdventOfCode.Day16.Extensions;

public static class ListExtensions
{
    public static List<Position> Copy(this List<Position> positions)
    {
        var copy = new List<Position>();

        foreach (var position in positions)
        {
            copy.Add(new Position(position.Row, position.Column));
        }

        return copy;
    }

    public static int CountDistinct(this List<Position> positions)
    {
        var distinctPositions = new List<Position>();

        foreach (var position in positions)
        {
            if (!distinctPositions.Any(x => x.Row == position.Row && x.Column == position.Column))
            {
                distinctPositions.Add(position);
            };
        }

        return distinctPositions.Count;
    }
}
