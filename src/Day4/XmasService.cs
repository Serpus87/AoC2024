using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day4;

public static class XmasService
{
    public static List<Coordinate> GetCoordinates(string[] input, char charOfInterest)
    {
        var coordinates = new List<Coordinate>();

        for (int i = 0; i < input.Length; i++)
        {
            coordinates.AddRange(GetCoordinatesInLine(input[i], i, charOfInterest));
        }

        return coordinates;
    }

    public static List<Streak> GetStreaks(List<Coordinate> coordinates, string[] input, string wordOfInterest, List<Direction> directions)
    {
        var streaks = new List<Streak>();
        var numberOfLines = input.Length;

        foreach (Coordinate coordinate in coordinates)
        {
            var lineLength = input[coordinate.LineIndex].Length;
            streaks.AddRange(GetCoordinateStreaks(coordinate, input, wordOfInterest, directions));
        }

        return streaks;
    }

    public static List<Cross> GetCrosses(List<Coordinate> coordinates, string[] input, string wordOfInterest, List<List<Direction>> listOfDirections)
    {
        var crosses = new List<Cross>();
        var numberOfLines = input.Length;

        foreach (Coordinate coordinate in coordinates)
        {
            var lineLength = input[coordinate.LineIndex].Length;
            var cross = GetCoordinateCross(coordinate, input, wordOfInterest, listOfDirections);

            if (cross != null)
            {
                crosses.Add(cross);
            }
        }

        return crosses;
    }

    public static int ProcessStreaks(List<Streak> streaks, string[] input, string wordOfInterest)
    {
        return streaks.Count(x => x.HasWordOfInterest(wordOfInterest)); ;
    }

    public static int ProcessCrosses(List<Cross> crosses, string[] input, string wordOfInterest)
    {
        return crosses.Count(x => x.HasOnlyWordOfInterest(wordOfInterest)); ;
    }

    private static List<Streak> GetCoordinateStreaks(Coordinate coordinate, string[] input, string wordOfInterest, List<Direction> directions)
    {
        var streaks = new List<Streak>();
        var lengthToCheck = wordOfInterest.Length - 1;

        foreach (var direction in directions)
        {
            bool existsStreak = ExistsStreak(coordinate, lengthToCheck, direction, input);

            if (!existsStreak)
            {
                continue;
            }

            streaks.Add(GetCoordinateStreak(coordinate, direction, wordOfInterest, input));
        }

        return streaks;
    }

    private static Cross? GetCoordinateCross(Coordinate coordinate, string[] input, string wordOfInterest, List<List<Direction>> listsOfDirections)
    {
        var crossArmLength = (wordOfInterest.Length - 1) / 2;

        var streaks = new List<Streak>();

        foreach (var directions in listsOfDirections)
        {
            bool streakExist = ExistsStreak(coordinate, crossArmLength, directions, input);

            if (!streakExist)
            {
                return null;
            }

            var streakStartingCoordinate = GetStreakStartingCoordinate(coordinate, directions[0], crossArmLength);

            streaks.Add(GetCoordinateStreak(streakStartingCoordinate, directions[1], wordOfInterest, input));
        }

        return new Cross(streaks);
    }

    private static Streak GetCoordinateStreak(Coordinate coordinate, Direction direction, string wordOfInterest, string[] input)
    {
        var coordinates = new List<Coordinate>();

        for (int i = 0; i < wordOfInterest.Length; i++)
        {
            var lineIndex = coordinate.LineIndex + i * direction.VerticalDirection;
            var charIndex = coordinate.CharIndex + i * direction.HorizontalDirection;
            coordinates.Add(new Coordinate(lineIndex, charIndex, input[lineIndex][charIndex]));
        }

        return new Streak(coordinates);
    }

    private static Coordinate GetStreakStartingCoordinate(Coordinate coordinate, Direction direction, int crossArmLength)
    {
        return new Coordinate(coordinate.LineIndex + (direction.VerticalDirection * crossArmLength), coordinate.CharIndex + (direction.HorizontalDirection * crossArmLength));
    }

    private static bool ExistsStreak(Coordinate coordinate, int lengthToCheck, Direction direction, string[] input)
    {
        var existsStreak = false;
        var numberOfLines = input.Length;
        var lineLength = input[coordinate.LineIndex].Length;

        existsStreak = coordinate.LineIndex + (direction.VerticalDirection * lengthToCheck) >= 0 &&
           coordinate.LineIndex + (direction.VerticalDirection * lengthToCheck) < numberOfLines &&
           coordinate.CharIndex + (direction.HorizontalDirection * lengthToCheck) >= 0 &&
           coordinate.CharIndex + (direction.HorizontalDirection * lengthToCheck) < lineLength;

        if (!existsStreak)
        {
            return false;
        }

        return existsStreak;
    }

    private static bool ExistsStreak(Coordinate coordinate, int lengthToCheck, List<Direction> directions, string[] input)
    {
        var existsStreak = false;

        foreach (var direction in directions)
        {
            existsStreak = ExistsStreak(coordinate, lengthToCheck, direction, input);

            if (!existsStreak)
            {
                return false;
            }
        }

        return existsStreak;
    }

    private static List<Coordinate> GetCoordinatesInLine(string line, int lineIndex, char charOfinterest)
    {
        var xIndices = new List<Coordinate>();
        var lineHasX = true;
        var startIndex = 0;

        while (lineHasX && startIndex < line.Length)
        {
            var xIndex = line.IndexOf(charOfinterest, startIndex);

            if (xIndex == -1)
            {
                lineHasX = false;
            }
            else
            {
                startIndex = xIndex + 1;
                xIndices.Add(new Coordinate(lineIndex, xIndex, charOfinterest));
            }
        }

        return xIndices;
    }
}
