using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day10.Models;
using AdventOfCode.Day4;

namespace AdventOfCode.Day10;

public static class MapService
{
    public static Map GetMap(string[] input)
    {
        var nRows = input.Length;
        var nColumns = input[0].Length;

        var trailHeads = new List<TrailHead>();
        var trailTails = new List<Position>();

        var map = new Map(nRows, nColumns);

        for (var row = 0; row < nRows; row++)
        {
            for (var column = 0; column < nColumns; column++)
            {
                map.Fields[row, column] = new Field(new Position(row, column), (int)Char.GetNumericValue(input[row][column]));

                if (map.Fields[row, column].Height == 0)
                {
                    trailHeads.Add(new TrailHead(new Position(row, column)));
                }
                if (map.Fields[row, column].Height == 9)
                {
                    trailTails.Add(new Position(row, column));
                }
            }
        }

        map.TrailHeads = trailHeads;
        map.TrailTails = trailTails;

        return map;
    }

    public static void GetTrailHeadsScores(Map map)
    {
        foreach (var trailHead in map.TrailHeads)
        {
            GetTrailHeadScores(trailHead, map);
        }
    }

    private static void GetTrailHeadScores(TrailHead trailHead, Map map)
    {
        var hasAvailableTrails = true;
        while (hasAvailableTrails)
        {
            hasAvailableTrails = WalkTrails(trailHead.Position, trailHead, map);
        }
    }

    private static bool WalkTrails(Position position, TrailHead trailHead, Map map)
    {
        var height = map.Fields[position.Row, position.Column].Height;
        if (height == 9)
        {
            trailHead.AddTrailTailIfNew(new Position(position.Row, position.Column));

            if (trailHead.TrailTails.Count == map.TrailTails.Count)
            {
                return false;
            }

            trailHead.AddTrailEndIfNew(new Position(position.Row, position.Column));
            return WalkTrails(trailHead.Position, trailHead, map);
        }

        var availablePositions = GetAvailablePositions(map, trailHead, position);

        if (availablePositions.Count == 0)
        {
            //map.Fields[position.Row, position.Column].IsPassable = false; // todo imprive this -- split HasPassablePositions and HasAvailablePositions
            trailHead.AddTrailEndIfNew(new Position(position.Row, position.Column));
            return false;
        }

        foreach (var availablePosition in availablePositions)
        {
            return WalkTrails(availablePosition, trailHead, map);
        }

        return false;
    }

    private static List<Position> GetAvailablePositions(Map map, TrailHead trailHead, Position position)
    {
        var availablePositions = new List<Position>();
        var directions = new List<(int, int)> { (-1, 0), (1, 0), (0, -1), (0, 1) };

        foreach (var direction in directions)
        {
            var newPosition = new Position(position.Row + direction.Item1, position.Column + direction.Item2);
            var isNewPositionAvailable = IsNewPositionAvailable(position, newPosition, trailHead, map);

            if (isNewPositionAvailable)
            {
                availablePositions.Add(newPosition);
            }
        }

        return availablePositions;
    }

    private static bool IsNewPositionAvailable(Position currentPosition, Position newPosition, TrailHead trailhead, Map map)
    {
        if (newPosition.Row < 0 || newPosition.Row >= map.NRows || newPosition.Column < 0 || newPosition.Column >= map.NColumns)
        {
            return false;
        }

        if (!map.Fields[newPosition.Row, newPosition.Column].IsPassable)
        {
            return false;
        }

        if (trailhead.HasTrailEnd(newPosition))
        {
            return false;
        }

        var newHeight = map.Fields[newPosition.Row, newPosition.Column].Height;
        var currentHeight = map.Fields[currentPosition.Row, currentPosition.Column].Height;

        return (newHeight - currentHeight) == 1;
    }
}
