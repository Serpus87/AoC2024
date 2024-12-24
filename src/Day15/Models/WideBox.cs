using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day15.Enums;

namespace AdventOfCode.Day15.Models;

public class WideBox
{
    public Box LeftBox { get; init; }
    public Box RightBox { get; init; }
    public List<Box> Boxes { get; init; }
    public List<Move> PossibleMoves { get; set; } = new List<Move> { MoveList.Left, MoveList.Right, MoveList.Up, MoveList.Down };
    public PossibleMovesEnum PossibleMovesEnum { get; set; } = PossibleMovesEnum.NotHasChanged;
    public MovingEnum MovingEnum { get; set; } = MovingEnum.NotHasMoved;

    public WideBox(Box leftBox, Box rightBox)
    {
        LeftBox = leftBox;
        RightBox = rightBox;
        Boxes = new List<Box> { leftBox, rightBox};
    }

    public void UpdatePossibleMovesEnum(PossibleMovesEnum possibleMovesEnum)
    {
        PossibleMovesEnum = possibleMovesEnum;
        LeftBox.PossibleMovesEnum = possibleMovesEnum;
        RightBox.PossibleMovesEnum = possibleMovesEnum;
    }

    public void UpdatePossibleMoves(List<Move> possibleMoves)
    {
        PossibleMoves = possibleMoves;
        LeftBox.PossibleMoves = possibleMoves;
        RightBox.PossibleMoves = possibleMoves;
    }

    public int GetGps(Map map)
    {
        var distanceFromLeftEdge = LeftBox.Position.Column;
        var distanceFromRightEdge = map.NumberOfColumns - RightBox.Position.Column;
        var distanceFromTopEdge = LeftBox.Position.Row;

        var shortestDistanceFromEdge = Math.Min(distanceFromLeftEdge, distanceFromRightEdge);

        return 100 * distanceFromTopEdge + shortestDistanceFromEdge;
    }

    public List<WideBox> GetAdjacentWideBoxesFromMoveDirection(Move move, List<WideBox> wideBoxes)
    {
        var adjacentWideBoxes = new List<WideBox>();

        foreach (var box in Boxes) 
        {
            var adjacentBox = box.GetAdjacentBox(move, wideBoxes.SelectMany(x => x.Boxes).ToList());

            if (adjacentBox != null)
            {
                adjacentWideBoxes.Add(adjacentBox.GetWideBox(wideBoxes));
                adjacentWideBoxes.Remove(this);
            }
        }

        return adjacentWideBoxes.Distinct().ToList();
    }

    public List<WideBox> GetAdjacentWideBoxes(List<WideBox> wideBoxes)
    {
        var adjacentWideBoxes = new List<WideBox>();

        foreach (var move in MoveList.List)
        {
            var adjacentWideBoxesFromMove = GetAdjacentWideBoxesFromMoveDirection(move, wideBoxes);

            if (adjacentWideBoxesFromMove.Count > 0)
            {
                adjacentWideBoxes.AddRange(adjacentWideBoxesFromMove);
            }
        }

        return adjacentWideBoxes.Distinct().ToList();
    }
}
