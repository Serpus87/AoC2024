using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day15.Models;

namespace AdventOfCode.Day15;

public static class WarehouseService
{
    public static List<string[]> SplitInput(string[] input)
    {
        var splitInput = new List<string[]>();
        var warehouseStrings = new List<string>();
        var moveListStrings = new List<string>();
        var newLineIsFound = false;

        foreach (var line in input)
        {
            if (line.Length == 0)
            {
                newLineIsFound = true;
                continue;
            }
            if (!newLineIsFound)
            {
                warehouseStrings.Add(line);
                continue;
            }
            if (newLineIsFound)
            {
                moveListStrings.Add(line);
                continue;
            }
        }

        splitInput.Add(warehouseStrings.ToArray());
        splitInput.Add(moveListStrings.ToArray());

        return splitInput;
    }
    public static Warehouse GetWarehouse(string[] input)
    {
        var nRows = input.Length;
        var nColumns = input[0].Length;
        var map = new Map(nRows, nColumns);

        var robots = new List<Robot>();
        var boxes = new List<Box>();

        for (var row = 0; row < nRows; row++)
        {
            for (var column = 0; column < nColumns; column++)
            {
                var position = new Position(row, column);
                var fill = input[row][column];

                map.Fields[row, column] = new Field(position, fill, fill == '#');

                if (fill == 'O')
                {
                    boxes.Add(new Box(position));
                }
                if (fill == '@')
                {
                    robots.Add(new Robot(position));
                }
            }
        }

        return new Warehouse(map, robots.Single(), boxes);
    }

    public static List<Move> GetRobotMoveList(string[] lines)
    {
        var moves = new List<Move>();

        foreach (var line in lines)
        {
            for (int i = 0; i < line.Length; i++)
            {
                switch (line[i])
                {
                    case '<':
                        moves.Add(new Move(0, -1));
                        break;
                    case '>':
                        moves.Add(new Move(0, 1));
                        break;
                    case '^':
                        moves.Add(new Move(-1, 0));
                        break;
                    case 'v':
                        moves.Add(new Move(1, 0));
                        break;
                    default:
                        break;
                }
            }
        }

        return moves;
    }
}
