using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day15.Extensions;
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

    public static WideWarehouse GetWideWarehouse(string[] input)
    {
        var nRows = input.Length;
        var lineLength = input[0].Length;
        var nColumns = lineLength * 2;
        var map = new Map(nRows, nColumns);

        var robots = new List<Robot>();
        var wideBoxes = new List<WideBox>();

        for (var row = 0; row < nRows; row++)
        {
            for (var i = 0; i < lineLength; i++)
            {
                var column1 = i * 2;
                var column2 = column1 + 1;

                var position1 = new Position(row, column1);
                var position2 = new Position(row, column2);

                var inputFill = input[row][i];

                if (inputFill == 'O')
                {
                    map.Fields[row, column1] = new Field(position1, '[', inputFill == '#');
                    map.Fields[row, column2] = new Field(position2, ']', inputFill == '#');
                    wideBoxes.Add(new WideBox(new Box(position1), new Box(position2)));
                }
                if (inputFill == '@')
                {
                    map.Fields[row, column1] = new Field(position1, inputFill, inputFill == '#');
                    map.Fields[row, column2] = new Field(position2, '.', inputFill == '#');
                    robots.Add(new Robot(position1));
                }
                if (inputFill != 'O' && inputFill != '@')
                {
                    map.Fields[row, column1] = new Field(position1, inputFill, inputFill == '#');
                    map.Fields[row, column2] = new Field(position2, inputFill, inputFill == '#');
                }
            }
        }

        return new WideWarehouse(map, robots.Single(), wideBoxes);
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
                        moves.Add(MoveList.Left);
                        break;
                    case '>':
                        moves.Add(MoveList.Right);
                        break;
                    case '^':
                        moves.Add(MoveList.Up);
                        break;
                    case 'v':
                        moves.Add(MoveList.Down);
                        break;
                    default:
                        break;
                }
            }
        }

        return moves;
    }

    public static void MakeAllRobotMoves(Warehouse warehouse)
    {
        var robot = warehouse.Robot;
        var map = warehouse.Map;
        var boxes = warehouse.Boxes;
        var moveCounter = 0;

        foreach (var move in warehouse.Robot.Moves)
        {
            moveCounter++;
            var possibleNewLocation = new Position(robot.Position.Row + move.Vertical, robot.Position.Column + move.Horizontal);

            if (map.Fields[possibleNewLocation.Row, possibleNewLocation.Column].IsWall)
            {
                continue;
            }

            var newLocationBox = boxes.SingleOrDefault(x=>x.Position.Row == possibleNewLocation.Row && x.Position.Column == possibleNewLocation.Column);

            if (newLocationBox != null && !newLocationBox.PossibleMoves.Includes(move)) 
            { 
                continue; 
            }

            robot.MakeMove(move,map);

            if (newLocationBox != null && newLocationBox.PossibleMoves.Includes(move))
            {
                boxes.MoveFrom(newLocationBox, move, map);
            }

            boxes.UpdateMoveDirections(map);
            //map.Print(move, moveCounter, warehouse.Robot.Moves.Count);
            //Console.WriteLine($"Move number {moveCounter} out of {warehouse.Robot.Moves.Count} total number of Moves");
        }
    }

    public static void MakeAllRobotMovesInWideWarehouse(WideWarehouse warehouse)
    {
        var robot = warehouse.Robot;
        var map = warehouse.Map;
        var wideBoxes = warehouse.WideBoxes;
        var moveCounter = 0;

        foreach (var move in warehouse.Robot.Moves)
        {
            if (moveCounter > 4275)
            {
                //map.Print(move, moveCounter, warehouse.Robot.Moves.Count);
            }

            moveCounter++;
            var possibleNewLocation = new Position(robot.Position.Row + move.Vertical, robot.Position.Column + move.Horizontal);

            if (map.Fields[possibleNewLocation.Row, possibleNewLocation.Column].IsWall)
            {
                continue;
            }

            var newLocationBox = wideBoxes.SingleOrDefault(x => x.Boxes.Any(y=>y.Position.Row == possibleNewLocation.Row && y.Position.Column == possibleNewLocation.Column));

            if (newLocationBox != null && !newLocationBox.PossibleMoves.Includes(move))
            {
                continue;
            }

            robot.MakeMove(move, map);

            if (newLocationBox != null && newLocationBox.PossibleMoves.Includes(move))
            {
                //if (move.Vertical != 0)
                //{
                //    if (robot.Position.Column == newLocationBox.LeftBox.Position.Column)
                //    {
                //        map.Fields[newLocationBox.RightBox.Position.Row, newLocationBox.RightBox.Position.Column].Fill = '.';
                //    }
                //    else
                //    {
                //        map.Fields[newLocationBox.LeftBox.Position.Row, newLocationBox.LeftBox.Position.Column].Fill = '.';
                //    }
                        
                //}
                wideBoxes.MoveFrom(newLocationBox, move, map);
            }

            wideBoxes.UpdateMoveDirections(map);
            map.Update(robot, wideBoxes);
            //map.Print(move, moveCounter, warehouse.Robot.Moves.Count);
            //Console.WriteLine($"Move number {moveCounter} out of {warehouse.Robot.Moves.Count} total number of Moves");
        }
    }
}
