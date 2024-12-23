using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day15.Extensions;

namespace AdventOfCode.Day15.Models;

public static class MoveList
{
    public static Move Left { get; set; } = new Move(0, -1);
    public static Move Right { get; set; } = new Move(0, 1);
    public static Move Up { get; set; } = new Move(-1, 0);
    public static Move Down { get; set; } = new Move(1, 0);
    public static List<Move> List { get; set; } = new List<Move> { Left, Right, Up, Down};

    public static List<Move> GetCurrentImpossibleMoves(List<Move> currentPossibleMoves)
    {
        var moveList = new List<Move> { Left, Right, Up, Down };

        return moveList.Where(x => !currentPossibleMoves.Includes(x)).ToList();
    }
}
