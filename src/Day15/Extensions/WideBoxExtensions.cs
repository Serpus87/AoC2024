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

    public static void MakeMove(this List<WideBox> wideBoxes, Move move)
    {
        foreach (var wideBox in wideBoxes) 
        {
            wideBox.LeftBox.Position = new Position(wideBox.LeftBox.Position.Row + move.Vertical, wideBox.LeftBox.Position.Column + move.Horizontal);
            wideBox.RightBox.Position = new Position(wideBox.RightBox.Position.Row + move.Vertical, wideBox.RightBox.Position.Column + move.Horizontal);
        }
    }




    public static List<WideBox> GetAllWideBoxesInDirection(this WideBox wideBox, Move move, List<WideBox> wideBoxes)
    {
        var wideBoxesInDirection = new List<WideBox>();
        var wideBoxesToCheck = new List<WideBox> { wideBox };

        while (true)
        {
            var adjacentWideBoxes = new List<WideBox>();

            foreach (var wideBoxToCheck in wideBoxesToCheck)
            {
                adjacentWideBoxes.AddRange(wideBoxToCheck.GetAdjacentWideBoxesFromMoveDirection(move, wideBoxes));
            }

            if (adjacentWideBoxes.Count == 0)
            {
                break;
            }

            wideBoxesInDirection.AddRange(adjacentWideBoxes);
            wideBoxesToCheck = adjacentWideBoxes;
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
