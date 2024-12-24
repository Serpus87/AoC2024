using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day15.Models;
using AdventOfCode.Day15.Enums;

namespace AdventOfCode.Day15.Extensions;

public static class WideBoxExtensions
{
    public static void InitializeMoveDirections(this List<WideBox> wideBoxes, Map map)
    {
        // set moveDirections for boxes next to walls
        wideBoxes.SelectMany(x => x.Boxes).ToList().InitializeMoveDirections(map);

        foreach (var wideBox in wideBoxes)
        {
            if (wideBox.Boxes.Any(x => x.PossibleMovesEnum == PossibleMovesEnum.WillChange))
            {
                wideBox.UpdatePossibleMovesEnum(PossibleMovesEnum.WillChange);
            }
        }

        UpdateMoveDirections(wideBoxes, map);
    }

    public static void UpdateMoveDirections(this List<WideBox> wideBoxes, Map map)
    {
        var hasWideBoxWillChange = wideBoxes.Any(x => x.PossibleMovesEnum == PossibleMovesEnum.WillChange);

        while (hasWideBoxWillChange)
        {
            var boxes = wideBoxes.SelectMany(x => x.Boxes).ToList();
            boxes.UpdateMoveDirections(map);
            //var hasBoxWillChange = boxes.Any(x => x.PossibleMovesEnum == PossibleMovesEnum.WillChange);

            var wideBoxToCheckChange = wideBoxes.First(x => x.PossibleMovesEnum == PossibleMovesEnum.WillChange);

            var movesToRemove = wideBoxToCheckChange.GetMovesToRemove(map, boxes);
            var movesToAdd = wideBoxToCheckChange.GetMovesToAdd(map, boxes);

            if (movesToRemove.Count > 0 || movesToAdd.Count > 0)
            {
                var newPossibleMoves = wideBoxToCheckChange.PossibleMoves.Without(movesToRemove);
                newPossibleMoves.AddRange(movesToAdd);

                wideBoxToCheckChange.UpdatePossibleMoves(newPossibleMoves);
                wideBoxToCheckChange.UpdatePossibleMovesEnum(PossibleMovesEnum.HasChanged);
            }
            else
            {
                wideBoxToCheckChange.UpdatePossibleMovesEnum(PossibleMovesEnum.NotHasChanged);
            }

            if (wideBoxToCheckChange.PossibleMovesEnum == PossibleMovesEnum.HasChanged)
            {
                var adjacentWideBoxes = wideBoxToCheckChange.GetAdjacentWideBoxes(wideBoxes);

                foreach (var adjacentWideBox in adjacentWideBoxes)
                {
                    adjacentWideBox.UpdatePossibleMovesEnum(PossibleMovesEnum.WillChange);
                }
            }

            hasWideBoxWillChange = wideBoxes.Any(x => x.PossibleMovesEnum == PossibleMovesEnum.WillChange);
        }
    }

    public static void MoveFrom(this List<WideBox> wideBoxes, WideBox wideBoxToMoveFrom, Move move, Map map)
    {
        // get/set all wideBoxes that should move
        wideBoxToMoveFrom.MovingEnum = MovingEnum.PrepareToMove;

        var isDonePreparing = false;

        while (!isDonePreparing) 
        {
            var wideBox = wideBoxes.First(x=>x.MovingEnum == MovingEnum.PrepareToMove);

            var adjacentWideBoxes = wideBox.GetAdjacentWideBoxesFromMoveDirection(move, wideBoxes);

            foreach(var adjacentWideBox in adjacentWideBoxes)
            {
                if (adjacentWideBox.MovingEnum != MovingEnum.ReadyToMove)
                {
                    adjacentWideBox.MovingEnum = MovingEnum.PrepareToMove;
                }
            }

            wideBox.MovingEnum = MovingEnum.ReadyToMove;
            isDonePreparing = wideBoxes.Any(x => x.MovingEnum == MovingEnum.PrepareToMove);
        }

        // move
        var isDoneMoving = false;

        while (!isDoneMoving)
        {
            var wideBox = wideBoxes.First(x => x.MovingEnum == MovingEnum.ReadyToMove);

            wideBox.LeftBox.Position = new Position(wideBox.LeftBox.Position.Row + move.Vertical, wideBox.LeftBox.Position.Column + move.Horizontal);
            wideBox.RightBox.Position = new Position(wideBox.RightBox.Position.Row + move.Vertical, wideBox.RightBox.Position.Column + move.Horizontal);

            wideBox.MovingEnum = MovingEnum.HasMoved;
            isDonePreparing = wideBoxes.Any(x => x.MovingEnum == MovingEnum.ReadyToMove);
        }

    }

    public static List<Move> GetMovesToAdd(this WideBox wideBox, Map map, List<Box> boxes)
    {
        var currentMoves = wideBox.PossibleMoves;
        var overlappingBoxMoves = wideBox.RightBox.PossibleMoves.Where(wideBox.RightBox.PossibleMoves.Contains).ToList();
        var movesToAdd = overlappingBoxMoves.Where(x=>!currentMoves.Contains(x)).ToList();

        return movesToAdd;
    }

    public static List<Move> GetMovesToRemove(this WideBox wideBox, Map map, List<Box> boxes)
    {
        var currentMoves = wideBox.PossibleMoves;
        var movesToRemove = new List<Move>();

        foreach(Move currentMove in currentMoves)
        {
            if (wideBox.LeftBox.PossibleMoves.Contains(currentMove) && wideBox.RightBox.PossibleMoves.Contains(currentMove))
            {
                continue;
            } 
            movesToRemove.Add(currentMove);
        }

        return movesToRemove;
    }

    public static int GetGPSSum(this List<WideBox> wideBoxes)
    {
        var gps = 0;

        foreach (var wideBox in wideBoxes)
        {
            gps += wideBox.GetGps();
        }

        return gps;
    }
}
