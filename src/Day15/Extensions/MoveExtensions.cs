using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day15.Models;

namespace AdventOfCode.Day15.Extensions;

public static class MoveExtensions
{
    public static List<Move> AddIfNew(this List<Move> moves, Move moveToAdd)
    {
        if (!moves.Any(x => x.Horizontal == moveToAdd.Horizontal && x.Vertical == moveToAdd.Vertical))
        {
            moves.Add(moveToAdd);
        }

        return moves;
    }

    public static List<Move> Without(this List<Move> moves, List<Move> movesToRemove)
    {
        var movesWithoutMovesToRemove = new List<Move>();

        foreach (var move in moves) 
        {
            if (!movesToRemove.Any(x=>x.Horizontal == move.Horizontal && x.Vertical == move.Vertical))
            {
                movesWithoutMovesToRemove.Add(move);
            }
        }

        return movesWithoutMovesToRemove;
    }

    public static bool Includes(this List<Move> moves, Move moveToCheck)
    {
        return moves.Any(x=>x.Horizontal == moveToCheck.Horizontal && x.Vertical == moveToCheck.Vertical);
    }
}
