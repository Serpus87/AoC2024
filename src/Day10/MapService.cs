using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day10.Extensions;
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

    public static void GetTrailHeadsRatings(Map map)
    {
        foreach (var trailHead in map.TrailHeads)
        {
            GetTrailHeadRatings(trailHead, map);
        }
    }

    private static void GetTrailHeadScores(TrailHead trailHead, Map map)
    {
        var hasAvailableTrails = true;
        while (hasAvailableTrails)
        {
            hasAvailableTrails = WalkTrails(trailHead.Position, trailHead, map, new Trail());
        }
    }

    // todo refactor this; it is way too complex and very bad
    private static void GetTrailHeadRatings(TrailHead trailHead, Map map)
    {
        var totalNumberOfAlternativeRoutes = 4 * Math.Pow(3, 8);
        trailHead.TrailEnds = new List<Position>();

        var haveAllAlternativesBeenChecked = false;

        while (!haveAllAlternativesBeenChecked)
        {
            foreach (var trail in trailHead.Trails.Where(x => !x.HasAlternativesBeenChecked).ToList())
            {
                var startingTrail = new Trail();
                for (var i = 0; i < (trail.Positions.Count - 1); i++)
                {
                    startingTrail.Positions.Add(trail.Positions[i]);

                    var trailsWithSameStartingTrail = trailHead.Trails.GetTrailsWithSimilarStartingTrail(startingTrail, trail.Positions.Last());

                    var alternativeNextPositions = GetAvailablePositionsForAlternativeRoute(map, trailHead, trail.Positions[i], trailsWithSameStartingTrail.Select(x => x.Positions[i + 1]).ToList(), trail.Positions.Last());

                    foreach (var position in alternativeNextPositions)
                    {
                        var hasAlternativeTrails = true;

                        while (hasAlternativeTrails)
                        {
                            var currentTrail = new Trail(startingTrail.Positions);
                            trailHead.TrailEnds = new List<Position>();
                            hasAlternativeTrails = WalkAlternativeTrails(position, position, trail.Positions.Last(), trailHead, map, currentTrail, startingTrail);
                        }
                    }
                }

                trail.HasAlternativesBeenChecked = true;
            }

            haveAllAlternativesBeenChecked = trailHead.Trails.All(x => x.HasAlternativesBeenChecked);
        }


        for (int j = 0; j < totalNumberOfAlternativeRoutes; j++)
        {
            foreach (var trail in trailHead.Trails.Where(x=>!x.HasAlternativesBeenChecked).ToList())
            {
                var startingTrail = new Trail();
                for (var i = 0; i < (trail.Positions.Count - 1); i++)
                {
                    startingTrail.Positions.Add(trail.Positions[i]);

                    var trailsWithSameStartingTrail = trailHead.Trails.GetTrailsWithSimilarStartingTrail(startingTrail, trail.Positions.Last());

                    var alternativeNextPositions = GetAvailablePositionsForAlternativeRoute(map, trailHead, trail.Positions[i], trailsWithSameStartingTrail.Select(x => x.Positions[i+1]).ToList(), trail.Positions.Last());

                    foreach (var position in alternativeNextPositions)
                    {
                        var hasAlternativeTrails = true;
                        
                        while (hasAlternativeTrails)
                        {
                            var currentTrail = new Trail(startingTrail.Positions);
                            trailHead.TrailEnds = new List<Position>();
                            hasAlternativeTrails = WalkAlternativeTrails(position, position, trail.Positions.Last(), trailHead, map, currentTrail, startingTrail);
                        }
                    }
                }

                trail.HasAlternativesBeenChecked = true;
            }
        } 
    }

    private static bool WalkAlternativeTrails(Position currentPosition, Position startingPosition, Position trailTail, TrailHead trailHead, Map map, Trail currentTrail, Trail startingTrail)
    {
        currentTrail.Positions.Add(currentPosition);

        if (currentTrail.Positions.Count>10)
        {
            var temp = true;
        }

        if (currentPosition.IsEqual(trailTail))
        {
            trailHead.AddTrailIfNew(currentTrail);

            return false;
        }

        var availablePositions = GetAvailablePositions(map, trailHead, currentPosition, null, trailTail);

        if (availablePositions.Count == 0 && currentPosition.Row == startingPosition.Row && currentPosition.Column == startingPosition.Column)
        {
            return false;
        }

        if (availablePositions.Count == 0)
        {
            //map.Fields[position.Row, position.Column].IsPassable = false; // todo imprive this -- split HasPassablePositions and HasAvailablePositions
            trailHead.AddTrailEndIfNew(new Position(currentPosition.Row, currentPosition.Column));
            return WalkAlternativeTrails(startingPosition, startingPosition, trailTail, trailHead, map, new Trail(startingTrail.Positions), startingTrail);
        }

        foreach (var availablePosition in availablePositions)
        {
            return WalkAlternativeTrails(availablePosition, startingPosition, trailTail, trailHead, map, currentTrail, startingTrail);
        }

        return WalkAlternativeTrails(startingPosition, startingPosition, trailTail, trailHead, map, new Trail(startingTrail.Positions), startingTrail);
    }

    private static bool WalkTrails(Position position, TrailHead trailHead, Map map, Trail trail)
    {
        trail.Positions.Add(position);

        var height = map.Fields[position.Row, position.Column].Height;
        if (height == 9)
        {
            trailHead.AddTrailTailIfNew(new Position(position.Row, position.Column));

            //if (trailHead.TrailTails.Count == map.TrailTails.Count)
            //{
            //    return false;
            //}

            if (trailHead.Trails.HasTrail(trail))
            {
                trailHead.AddTrailEndIfNew(new Position(position.Row, position.Column));
            }

            trailHead.AddTrailIfNew(trail);

            return WalkTrails(trailHead.Position, trailHead, map, new Trail());
        }

        var availablePositions = GetAvailablePositions(map, trailHead, position);

        if (availablePositions.Count == 0 && position.Row == trailHead.Position.Row && position.Column == trailHead.Position.Column)
        {
            return false;
        }

        if (availablePositions.Count == 0)
        {
            //map.Fields[position.Row, position.Column].IsPassable = false; // todo imprive this -- split HasPassablePositions and HasAvailablePositions
            trailHead.AddTrailEndIfNew(new Position(position.Row, position.Column));
            return WalkTrails(trailHead.Position, trailHead, map, new Trail());
        }

        foreach (var availablePosition in availablePositions)
        {
            return WalkTrails(availablePosition, trailHead, map, trail);
        }

        return WalkTrails(trailHead.Position, trailHead, map, new Trail());
    }

    private static List<Position> GetAvailablePositions(Map map, TrailHead trailHead, Position position, Position? excludingPosition = null, Position? trailTail = null)
    {
        var availablePositions = new List<Position>();
        var directions = new List<(int, int)> { (-1, 0), (1, 0), (0, -1), (0, 1) };

        foreach (var direction in directions)
        {
            var newPosition = new Position(position.Row + direction.Item1, position.Column + direction.Item2);
            var isNewPositionAvailable = IsNewPositionAvailable(position, newPosition, trailHead, map, trailTail);

            if (isNewPositionAvailable)
            {
                availablePositions.Add(newPosition);
            }
        }

        if (excludingPosition != null)
        {
            availablePositions = availablePositions.Where(x => x.Row != excludingPosition.Row && x.Column != excludingPosition.Column).ToList();
        }

        return availablePositions;
    }

    private static List<Position> GetAvailablePositionsForAlternativeRoute(Map map, TrailHead trailHead, Position position, List<Position>? excludingPositions = null, Position? trailTail = null)
    {
        var availablePositions = new List<Position>();
        var directions = new List<(int, int)> { (-1, 0), (1, 0), (0, -1), (0, 1) };

        foreach (var direction in directions)
        {
            var newPosition = new Position(position.Row + direction.Item1, position.Column + direction.Item2);
            var isNewPositionAvailable = IsNewPositionAvailable(position, newPosition, trailHead, map, trailTail);

            if (isNewPositionAvailable)
            {
                availablePositions.Add(newPosition);
            }
        }

        if (excludingPositions != null)
        {
            foreach (var excludingPosition in excludingPositions) 
            {
                availablePositions = availablePositions.Where(x => x.Row != excludingPosition.Row || x.Column != excludingPosition.Column).ToList();
            }
        }

        return availablePositions;
    }

    private static bool IsNewPositionAvailable(Position currentPosition, Position newPosition, TrailHead trailhead, Map map, Position? trailTail = null)
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

        if (trailTail != null)
        {
            var rowDifference = Math.Abs(newPosition.Row - trailTail.Row);
            var columnDifference = Math.Abs(newPosition.Column - trailTail.Column);
            var totalDistance = rowDifference + columnDifference;

            var heightDifference = 9 - newHeight;

            if (totalDistance > heightDifference)
            {
                return false;
            }
        }

        return (newHeight - currentHeight) == 1;
    }
}
