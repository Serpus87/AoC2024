using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day6.Models;

namespace AdventOfCode.Day14.Models;

public class Map
{
    public int NumberOfRows { get; set; }
    public int NumberOfColumns { get; set; }
    public Field[,] Fields { get; set; }
    public List<Field> FieldsAsList { get; set; } = new List<Field>();
    public List<Quadrant> Quadrants { get; set; }

    public Map(int numberOfRows, int numberOfColumns)
    {
        NumberOfRows = numberOfRows;
        NumberOfColumns = numberOfColumns;
        Fields = new Field[NumberOfRows, NumberOfColumns];
    }

    public void Print()
    {
        Console.Clear();

        for (var row = 0; row < NumberOfRows; row++)
        {
            var rowToPrint = new List<string>();
            for (var column = 0; column < NumberOfColumns; column++)
            {
                var fill = Fields[row, column].NumberOfRobots == 0 ? "." : Fields[row, column].NumberOfRobots.ToString();
                rowToPrint.Add(fill);
            }

            Console.WriteLine(string.Join(' ', rowToPrint));
        }
    }

    public void CreateEmpty()
    {
        for (var row = 0; row < NumberOfRows; row++)
        {
            for (var column = 0; column < NumberOfColumns; column++)
            {
                var field = new Field(new Location(row, column), 0);
                Fields[row, column] = field;
                FieldsAsList.Add(field);
            }
        }
    }

    public void DefineQuadrants()
    {
        var horizontalLine = (NumberOfRows - 1) / 2;
        var verticalLine = (NumberOfColumns - 1) / 2;

        Quadrants = new List<Quadrant>
        {
            new Quadrant(FieldsAsList
            .Where(x => x.Position.Row >= 0 && x.Position.Row < horizontalLine
            && x.Position.Column >= 0 && x.Position.Column < verticalLine)
            .ToList()),
            new Quadrant(FieldsAsList
            .Where(x => x.Position.Row > horizontalLine && x.Position.Row < NumberOfRows
            && x.Position.Column >= 0 && x.Position.Column < verticalLine)
            .ToList()),
            new Quadrant(FieldsAsList
            .Where(x => x.Position.Row >= 0 && x.Position.Row < horizontalLine
            && x.Position.Column > verticalLine && x.Position.Column < NumberOfColumns)
            .ToList()),
            new Quadrant(FieldsAsList
            .Where(x => x.Position.Row > horizontalLine && x.Position.Row < NumberOfRows
            && x.Position.Column > verticalLine && x.Position.Column < NumberOfColumns)
            .ToList())
        };
    }

    public void PlaceRobots(List<Robot> robots)
    {
        foreach (Robot robot in robots) 
        {
            PlaceRobot(robot.Position);
        }
    }

    public int GetSafetyFactor()
    {
        var safetyFactor = 0;

        foreach (var quadrant in Quadrants)
        {
            safetyFactor += quadrant.GetSafetyFactor();
        }

        return safetyFactor;
    }

    private void PlaceRobot(Location robotLocation)
    {
        Fields[robotLocation.Row, robotLocation.Column].NumberOfRobots++;
    }

    private void RemoveRobot(Location robotLocation)
    {
        Fields[robotLocation.Row, robotLocation.Column].NumberOfRobots--;
    }

    public void MoveRobot(Location currentLocation, Location nextLocation)
    {
        RemoveRobot(currentLocation);
        PlaceRobot(nextLocation);
    }

    public Location GetNextLocation(Robot robot)
    {
        var nextRow = GetNextRow(robot.Position.Row,robot.Velocity.Row);
        var nextColumn = GetNextColumn(robot.Position.Column,robot.Velocity.Column);

        return new Location(nextRow,nextColumn);
    }

    private int GetNextRow(int startingRow, int move)
    {
        var nextRow = startingRow + move;

        if (nextRow < 0)
        {
            return nextRow + NumberOfRows - 1;
        }

        if (nextRow >= NumberOfRows)
        {
            return nextRow - (NumberOfRows - 1);
        }

        return nextRow;
    }

    private int GetNextColumn(int startingColumn, int move)
    {
        var nextColumn = startingColumn + move;

        if (nextColumn < 0)
        {
            return nextColumn + NumberOfColumns - 1;
        }

        if (nextColumn >= NumberOfColumns)
        {
            return nextColumn - (NumberOfColumns - 1);
        }

        return nextColumn;
    }


}
