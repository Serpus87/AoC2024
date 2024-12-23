using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day15.Enums;
using AdventOfCode.Day15.Extensions;

namespace AdventOfCode.Day15.Models;

public class Box
{
    public Position Position { get; set; }
    public List<Move> PossibleMoves { get; set; } = new List<Move> { MoveList.Left, MoveList.Right, MoveList.Up, MoveList.Down };
    public PossibleMovesEnum PossibleMovesEnum { get; set; } = PossibleMovesEnum.NotHasChanged;

    public Box(Position position)
    {
        Position = position;
    }

    public List<Field> GetAdjacentFields(Map map)
    {
        var adjacentFields = new List<Field>();

        foreach (var move in MoveList.List) 
        {
            adjacentFields.Add(map.Fields[Position.Row + move.Horizontal, Position.Column + move.Vertical]);
        }

        return adjacentFields;
    }

    public Field GetAdjacentField(Map map, Move move)
    {
        return map.Fields[Position.Row + move.Horizontal, Position.Column + move.Vertical];
    }

    public Box? GetAdjacentBox(Move move, List<Box> boxes)
    {
        var moveLocation = new Position(Position.Row + move.Vertical, Position.Column + move.Horizontal);

        return boxes.FirstOrDefault(x=>x.Position.Row == moveLocation.Row && x.Position.Column == moveLocation.Column);
    }

    public List<Box> GetAdjacentBoxes(List<Box> boxes)
    {
        var adjacentBoxes = new List<Box>();

        foreach (var move in MoveList.List)
        {
            var box = GetAdjacentBox(move, boxes);

            if(box != null)
            {
                adjacentBoxes.Add(box);
            }
        }

        return adjacentBoxes;
    }

    public List<Move> GetMovesToRemove(Map map, List<Box> boxes)
    {
        var movesToRemove = new List<Move>();

        foreach (var move in PossibleMoves)
        {
            var field = GetAdjacentField(map, move);

            if (field.IsWall)
            {
                movesToRemove.AddIfNew(move);
                movesToRemove.AddIfNew(move.GetOppositeMove());
                continue;
            }

            var adjacentBox = GetAdjacentBox(move, boxes);

            if (adjacentBox == null)
            {
                continue;
            }

            if (!adjacentBox.PossibleMoves.Any(x => x.Horizontal == move.Horizontal && x.Vertical == move.Vertical))
            {
                movesToRemove.AddIfNew(move);
                movesToRemove.AddIfNew(move.GetOppositeMove());
            }
        }

        return movesToRemove;
    }

    public List<Move> GetMovesToAdd(Map map, List<Box> boxes)
    {
        var movesToAdd = new List<Move>();

        var currentImpossibleMoves = MoveList.GetCurrentImpossibleMoves(PossibleMoves);

        foreach (var move in currentImpossibleMoves)
        {
            var oppositeMove = move.GetOppositeMove();
            var field = GetAdjacentField(map, move);
            var oppositeField = GetAdjacentField(map, oppositeMove);

            if (!field.IsWall && !oppositeField.IsWall)
            {
                movesToAdd.AddIfNew(move);
                movesToAdd.AddIfNew(oppositeMove);
                continue;
            }

            var adjacentBox = GetAdjacentBox(move, boxes);
            var oppositeAdjacentBox = GetAdjacentBox(oppositeMove, boxes);


            if ((adjacentBox != null && !adjacentBox.PossibleMoves.Any(x => x.Horizontal == move.Horizontal && x.Vertical == move.Vertical))
                || (oppositeAdjacentBox != null && !oppositeAdjacentBox.PossibleMoves.Any(x => x.Horizontal == oppositeMove.Horizontal && x.Vertical == oppositeMove.Vertical)))
            {
                continue;
            }

            movesToAdd.AddIfNew(move);
            movesToAdd.AddIfNew(oppositeMove);
        }

        return movesToAdd;
    }

    public int GetGps()
    {
        return 100 * Position.Row + Position.Column;
    }
}
