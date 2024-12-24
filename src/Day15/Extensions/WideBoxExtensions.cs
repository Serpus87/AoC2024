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
            if (wideBox.Boxes.Any(x => x.PossibleMovesEnum == PossibleMovesEnum.HasChanged))
            {
                wideBox.UpdatePossibleMovesEnum(PossibleMovesEnum.WillChange);
            }
        }

        UpdateMoveDirections(wideBoxes, map);
    }

    public static void UpdateMoveDirections(this List<WideBox> wideBoxes, Map map)
    {
        // todo fix this

        var hasWideBoxWillChange = wideBoxes.Any(x => x.PossibleMovesEnum == PossibleMovesEnum.WillChange);

        //// update move directions of individual boxes
        //var boxes = wideBoxes.SelectMany(x => x.Boxes).ToList();
        //boxes.UpdateMoveDirections(map);

        // update wideboxes with box information
        while (hasWideBoxWillChange)
        {
            var wideBoxToCheckChange = wideBoxes.First(x => x.PossibleMovesEnum == PossibleMovesEnum.WillChange);

            var movesToRemove = wideBoxToCheckChange.GetMovesToRemove(map, wideBoxes);
            var movesToAdd = wideBoxToCheckChange.GetMovesToAdd(map, wideBoxes);

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
            var wideBox = wideBoxes.First(x => x.MovingEnum == MovingEnum.PrepareToMove);

            var adjacentWideBoxes = wideBox.GetAdjacentWideBoxesFromMoveDirection(move, wideBoxes);

            foreach (var adjacentWideBox in adjacentWideBoxes.Where(x => x.MovingEnum != MovingEnum.ReadyToMove))
            {
                adjacentWideBox.MovingEnum = MovingEnum.PrepareToMove;
            }

            wideBox.MovingEnum = MovingEnum.ReadyToMove;
            isDonePreparing = !wideBoxes.Any(x => x.MovingEnum == MovingEnum.PrepareToMove);
        }

        // move
        var isDoneMoving = false;

        while (!isDoneMoving)
        {
            var wideBox = wideBoxes.First(x => x.MovingEnum == MovingEnum.ReadyToMove);
            wideBox.UpdatePossibleMovesEnum(PossibleMovesEnum.WillChange);

            wideBox.LeftBox.Position = new Position(wideBox.LeftBox.Position.Row + move.Vertical, wideBox.LeftBox.Position.Column + move.Horizontal);
            wideBox.RightBox.Position = new Position(wideBox.RightBox.Position.Row + move.Vertical, wideBox.RightBox.Position.Column + move.Horizontal);

            wideBox.MovingEnum = MovingEnum.HasMoved;
            isDoneMoving = !wideBoxes.Any(x => x.MovingEnum == MovingEnum.ReadyToMove);
        }

    }

    public static List<Move> GetMovesToAdd(this WideBox wideBox, Map map, List<WideBox> wideBoxes)
    {
        var movesToAdd = new List<Move>();

        var currentImpossibleMoves = MoveList.GetCurrentImpossibleMoves(wideBox.PossibleMoves);

        foreach (var move in currentImpossibleMoves)
        {
            var oppositeMove = move.GetOppositeMove();
            var fields = wideBox.Boxes.Select(x => x.GetAdjacentField(map, move));
            var oppositeFields = wideBox.Boxes.Select(x => x.GetAdjacentField(map, oppositeMove));

            if (fields.Any(x => x.IsWall) || oppositeFields.Any(x => x.IsWall))
            {
                continue;
            }

            var adjacentBoxes = wideBox.GetAdjacentWideBoxesFromMoveDirection(move, wideBoxes);
            var oppositeAdjacentBoxes = wideBox.GetAdjacentWideBoxesFromMoveDirection(oppositeMove, wideBoxes);

            if (adjacentBoxes.Count == 0 && oppositeAdjacentBoxes.Count == 0)
            {
                movesToAdd.AddIfNew(move);
                movesToAdd.AddIfNew(oppositeMove);
                continue;
            }

            var allWideBoxesInMoveDirection = wideBox.GetAllWideBoxesInDirection(move, wideBoxes);
            var allWideBoxesInOppositeMoveDirection = wideBox.GetAllWideBoxesInDirection(oppositeMove, wideBoxes);

            var allBoxesInMoveDirection = allWideBoxesInMoveDirection.SelectMany(x => x.Boxes).ToList();
            var allBoxesInOppositeMoveDirection = allWideBoxesInOppositeMoveDirection.SelectMany(x => x.Boxes).ToList();

            if (allBoxesInMoveDirection.Any(x => x.GetAdjacentField(map, move).IsWall) || allBoxesInOppositeMoveDirection.Any(x => x.GetAdjacentField(map, oppositeMove).IsWall))
            {
                continue;
            }

            movesToAdd.AddIfNew(move);
            movesToAdd.AddIfNew(oppositeMove);
        }

        return movesToAdd;
    }

    public static List<Move> GetMovesToRemove(this WideBox wideBox, Map map, List<WideBox> wideBoxes)
    {
        var movesToRemove = new List<Move>();

        //-- temp debug
        if (wideBox.LeftBox.Position.Row == 1 || wideBox.LeftBox.Position.Row == 48 || wideBox.LeftBox.Position.Column == 2 || wideBox.RightBox.Position.Column == 97)
        {
            var debug = true;
        }
        //-- end-temp debug

        foreach (var move in wideBox.PossibleMoves)
        {
            //var oppositeMove = move.GetOppositeMove();
            var fields = wideBox.Boxes.Select(x => x.GetAdjacentField(map, move));
            //var oppositeFields = wideBox.Boxes.Select(x => x.GetAdjacentField(map, oppositeMove));

            //if (fields.Any(x => x.IsWall) || oppositeFields.Any(x => x.IsWall))
            if (fields.Any(x => x.IsWall))
            {
                movesToRemove.AddIfNew(move);
                //movesToRemove.AddIfNew(oppositeMove);
                continue;
            }

            var adjacentBoxes = wideBox.GetAdjacentWideBoxesFromMoveDirection(move, wideBoxes);
            //var oppositeAdjacentBoxes = wideBox.GetAdjacentWideBoxesFromMoveDirection(oppositeMove, wideBoxes);

            //if (adjacentBoxes.Count == 0 && oppositeAdjacentBoxes.Count == 0)
            if (adjacentBoxes.Count == 0)
            {
                continue;
            }

            var allWideBoxesInMoveDirection = wideBox.GetAllWideBoxesInDirection(move, wideBoxes);
            //var allWideBoxesInOppositeMoveDirection = wideBox.GetAllWideBoxesInDirection(oppositeMove, wideBoxes);

            var allBoxesInMoveDirection = allWideBoxesInMoveDirection.SelectMany(x => x.Boxes).ToList();
            //var allBoxesInOppositeMoveDirection = allWideBoxesInOppositeMoveDirection.SelectMany(x => x.Boxes).ToList();

            //if (allBoxesInMoveDirection.Any(x => x.GetAdjacentField(map, move).IsWall) || allBoxesInOppositeMoveDirection.Any(x => x.GetAdjacentField(map, oppositeMove).IsWall))
            if (allBoxesInMoveDirection.Any(x => x.GetAdjacentField(map, move).IsWall))
            {
                movesToRemove.AddIfNew(move);
                //movesToRemove.AddIfNew(move.GetOppositeMove());
            }
        }

        return movesToRemove;
    }

    public static List<WideBox> GetAllWideBoxesInDirection(this WideBox wideBox, Move move, List<WideBox> wideBoxes)
    {
        var wideBoxesInDirection = new List<WideBox>();
        var wideBoxesToCheck = new List<WideBox> { wideBox };

        while (true)
        {
            var adjacentBoxes = new List<WideBox>();

            foreach (var wideBoxToCheck in wideBoxesToCheck)
            {
                adjacentBoxes.AddRange(wideBoxToCheck.GetAdjacentWideBoxesFromMoveDirection(move, wideBoxes));
            }

            if (adjacentBoxes.Count == 0)
            {
                break;
            }

            wideBoxesInDirection.AddRange(adjacentBoxes);
            wideBoxesToCheck = adjacentBoxes;
        }

        return wideBoxesInDirection;
    }

    public static int GetGPSSum(this List<WideBox> wideBoxes, Map map)
    {
        var gps = 0;

        foreach (var wideBox in wideBoxes)
        {
            //gps += wideBox.GetGps(map);
            gps += wideBox.LeftBox.GetGps();
        }

        return gps;
    }
}
