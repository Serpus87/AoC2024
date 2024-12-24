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
            adjacentFields.Add(map.Fields[Position.Row + move.Vertical, Position.Column + move.Horizontal]);
        }

        return adjacentFields;
    }

    public Field GetAdjacentField(Map map, Move move)
    {
        return map.Fields[Position.Row + move.Vertical, Position.Column + move.Horizontal];
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

    public int GetGps()
    {
        return 100 * Position.Row + Position.Column;
    }

    public WideBox GetWideBox(List<WideBox> wideBoxes)
    {
        return wideBoxes.First(x=>x.Boxes.Contains(this));
    }
}
