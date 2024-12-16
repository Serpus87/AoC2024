using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day10.Models;

namespace AdventOfCode.Day10.Extensions;

public static class PositionExtensions
{
    public static bool IsEqual(this Position position, Position positionToCompare)
    {
        return position.Row == positionToCompare.Row && position.Column == positionToCompare.Column;
    }

    public static bool AreEqual(this List<Position> positions, List<Position> positionsToCompare)
    {
        var areEqual = new List<bool>();

        for (var i = 0; i < positions.Count; i++) 
        {
            areEqual.Add(positions[i].IsEqual(positionsToCompare[i]));
        }

        return areEqual.All(x => x == true);
    }

    public static bool HasSimilarStart(this List<Position> positions, List<Position> positionsToCompare)
    {
        var areEqual = new List<bool>();

        for (var i = 0; i < positionsToCompare.Count; i++)
        {
            areEqual.Add(positions[i].IsEqual(positionsToCompare[i]));
        }

        return areEqual.All(x => x == true);
    }
}
