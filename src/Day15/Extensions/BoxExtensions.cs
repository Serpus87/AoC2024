using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day15.Models;
using AdventOfCode.Day15.Enums;

namespace AdventOfCode.Day15.Extensions;

public static class BoxExtensions
{
    public static void InitializeMoveDirections(this List<Box> boxes, Map map)
    {
        // set moveDirections for boxes next to walls
        foreach (Box box in boxes) 
        {
            var adjacentFields = box.GetAdjacentFields(map);

            if (!adjacentFields.Any(x=>x.IsWall))
            {
                continue;
            }

            box.PossibleMovesEnum = PossibleMovesEnum.WillChange;
        }

        boxes.UpdateMoveDirections(map);
    }

    public static void UpdateMoveDirections(this List<Box> boxes, Map map)
    {
        var hasBoxWillChange = boxes.Any(x=>x.PossibleMovesEnum == PossibleMovesEnum.WillChange);

        while (hasBoxWillChange) 
        {
            var boxToCheckChange = boxes.First(x=>x.PossibleMovesEnum == PossibleMovesEnum.WillChange);

            var movesToRemove = boxToCheckChange.GetMovesToRemove(map, boxes);
            var movesToAdd = boxToCheckChange.GetMovesToAdd(map, boxes);

            if (movesToRemove.Count > 0 || movesToAdd.Count > 0)
            {
                boxToCheckChange.PossibleMoves = boxToCheckChange.PossibleMoves.Without(movesToRemove);
                boxToCheckChange.PossibleMoves.AddRange(movesToAdd);
                boxToCheckChange.PossibleMovesEnum = PossibleMovesEnum.HasChanged;
            }
            else 
            {
                boxToCheckChange.PossibleMovesEnum = PossibleMovesEnum.NotHasChanged;
            }

            if (boxToCheckChange.PossibleMovesEnum == PossibleMovesEnum.HasChanged) {
                var adjacentBoxes = boxToCheckChange.GetAdjacentBoxes(boxes);

                foreach (var adjacentBox in adjacentBoxes)
                {
                    adjacentBox.PossibleMovesEnum = PossibleMovesEnum.WillChange;
                }
            }

            hasBoxWillChange = boxes.Any(x => x.PossibleMovesEnum == PossibleMovesEnum.WillChange);
        }
    }

    public static void MoveFrom(this List<Box> boxes, Box boxToMoveFrom, Move move, Map map)
    {
        var adjacentBoxes = boxToMoveFrom.GetAdjacentBoxes(boxes);

        foreach (var box in adjacentBoxes) 
        {
            box.PossibleMovesEnum = PossibleMovesEnum.WillChange;
        }

        boxToMoveFrom.PossibleMovesEnum = PossibleMovesEnum.WillChange;
        var adjacentBox = boxToMoveFrom.GetAdjacentBox(move,boxes);

        if (adjacentBox == null) 
        {
            boxToMoveFrom.Position = new Position(boxToMoveFrom.Position.Row + move.Vertical, boxToMoveFrom.Position.Column + move.Horizontal);
            adjacentBoxes = boxToMoveFrom.GetAdjacentBoxes(boxes);

            foreach (var box in adjacentBoxes)
            {
                box.PossibleMovesEnum = PossibleMovesEnum.WillChange;
            }
        }

        while (adjacentBox != null) 
        {
            boxToMoveFrom.Position = new Position(boxToMoveFrom.Position.Row + move.Vertical, boxToMoveFrom.Position.Column + move.Horizontal);
            adjacentBoxes = boxToMoveFrom.GetAdjacentBoxes(boxes);

            foreach (var box in adjacentBoxes)
            {
                box.PossibleMovesEnum = PossibleMovesEnum.WillChange;
            }

            boxToMoveFrom = adjacentBox;
            boxToMoveFrom.PossibleMovesEnum = PossibleMovesEnum.WillChange;
            adjacentBox = boxToMoveFrom.GetAdjacentBox(move, boxes);

            if (adjacentBox == null)
            {
                boxToMoveFrom.Position = new Position(boxToMoveFrom.Position.Row + move.Vertical, boxToMoveFrom.Position.Column + move.Horizontal);
                adjacentBoxes = boxToMoveFrom.GetAdjacentBoxes(boxes);

                foreach (var box in adjacentBoxes)
                {
                    box.PossibleMovesEnum = PossibleMovesEnum.WillChange;
                }
            }
        }

        map.Fields[boxToMoveFrom.Position.Row, boxToMoveFrom.Position.Column].Fill = 'O';
    }

    public static List<Move> GetMovesToAdd(this Box box, Map map, List<Box> boxes)
    {
        var movesToAdd = new List<Move>();

        var currentImpossibleMoves = MoveList.GetCurrentImpossibleMoves(box.PossibleMoves);

        foreach (var move in currentImpossibleMoves)
        {
            var oppositeMove = move.GetOppositeMove();
            var field = box.GetAdjacentField(map, move);
            var oppositeField = box.GetAdjacentField(map, oppositeMove);

            if (field.IsWall || oppositeField.IsWall)
            {
                continue;
            }

            var adjacentBox = box.GetAdjacentBox(move, boxes);
            var oppositeAdjacentBox = box.GetAdjacentBox(oppositeMove, boxes);

            if (adjacentBox == null && oppositeAdjacentBox == null)
            {
                movesToAdd.AddIfNew(move);
                movesToAdd.AddIfNew(oppositeMove);
                continue;
            }

            var lastBoxInMoveDirection = box.GetLastBoxInDirection(move, boxes);
            var lastBoxInOppositeMoveDirection = box.GetLastBoxInDirection(oppositeMove, boxes);

            var fieldAdjacentToLastBoxInMoveDirection = lastBoxInMoveDirection.GetAdjacentField(map, move);
            var fieldAdjacentToLastBoxInOppositeMoveDirection = lastBoxInOppositeMoveDirection.GetAdjacentField(map, oppositeMove);

            if (fieldAdjacentToLastBoxInMoveDirection.IsWall || fieldAdjacentToLastBoxInOppositeMoveDirection.IsWall)
            {
                continue;
            }

            movesToAdd.AddIfNew(move);
            movesToAdd.AddIfNew(oppositeMove);
        }

        return movesToAdd;
    }

    public static List<Move> GetMovesToRemove(this Box box, Map map, List<Box> boxes)
    {
        var movesToRemove = new List<Move>();

        foreach (var move in box.PossibleMoves)
        {
            var oppositeMove = move.GetOppositeMove();
            var field = box.GetAdjacentField(map, move);
            var oppositeField = box.GetAdjacentField(map, oppositeMove);

            if (field.IsWall || oppositeField.IsWall)
            {
                movesToRemove.AddIfNew(move);
                movesToRemove.AddIfNew(oppositeMove);
                continue;
            }

            var adjacentBox = box.GetAdjacentBox(move, boxes);
            var oppositeAdjacentBox = box.GetAdjacentBox(oppositeMove, boxes);

            if (adjacentBox == null && oppositeAdjacentBox == null)
            {
                continue;
            }

            var lastBoxInMoveDirection = box.GetLastBoxInDirection(move, boxes);
            var lastBoxInOppositeMoveDirection = box.GetLastBoxInDirection(oppositeMove, boxes);

            var fieldAdjacentToLastBoxInMoveDirection = lastBoxInMoveDirection.GetAdjacentField(map, move);
            var fieldAdjacentToLastBoxInOppositeMoveDirection = lastBoxInOppositeMoveDirection.GetAdjacentField(map, oppositeMove);

            if (fieldAdjacentToLastBoxInMoveDirection.IsWall || fieldAdjacentToLastBoxInOppositeMoveDirection.IsWall)
            {
                movesToRemove.AddIfNew(move);
                movesToRemove.AddIfNew(move.GetOppositeMove());
            }
        }

        return movesToRemove;
    }

    public static Box GetLastBoxInDirection(this Box box, Move move, List<Box> boxes)
    {
        var isLastBox = false;
        var boxToCheck = box;

        while (true)
        {
            var nextBoxToCheck = boxToCheck.GetAdjacentBox(move, boxes);
            if (nextBoxToCheck != null)
            {
                boxToCheck = nextBoxToCheck;
                continue;
            }
            break;
        }

        return boxToCheck;
    }

    public static int GetGPSSum(this List<Box> boxes)
    {
        var gps = 0;

        foreach (var box in boxes) 
        {
            gps += box.GetGps();
        }

        return gps;
    }
}
