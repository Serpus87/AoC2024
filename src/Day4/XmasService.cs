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

    public static List<Streak> GetStreaks(List<Coordinate> coordinates, string[] input, string wordOfInterest)
    {
        var streaks = new List<Streak>();
        var numberOfLines = input.Length;

        foreach (Coordinate coordinate in coordinates)
        {
            var lineLength = input[coordinate.LineIndex].Length;
            streaks.AddRange(GetCoordinateStreaks(coordinate, input, wordOfInterest));
        }

        return streaks;
    }

    public static List<Cross> GetCrosses(List<Coordinate> coordinates, string[] input, string wordOfInterest)
    {
        var crosses = new List<Cross>();
        var numberOfLines = input.Length;

        foreach (Coordinate coordinate in coordinates)
        {
            var lineLength = input[coordinate.LineIndex].Length;
            var cross = GetCoordinateCross(coordinate, input, wordOfInterest);

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

    // todo simplify and generalize this
    private static List<Streak> GetCoordinateStreaks(Coordinate coordinate, string[] input, string wordOfInterest)
    {
        var streaks = new List<Streak>();

        var streakLengthWithOutCharOfInterest = wordOfInterest.Length - 1;
        var lineLength = input[coordinate.LineIndex].Length;
        var numberOfLines = input.Length;

        bool hasLeftHorizontalStreak = coordinate.CharIndex - streakLengthWithOutCharOfInterest >= 0;
        bool hasRightHorizontalStreak = coordinate.CharIndex + streakLengthWithOutCharOfInterest < lineLength;
        bool hasUpperVerticalStreak = coordinate.LineIndex - streakLengthWithOutCharOfInterest >= 0;
        bool hasLowerVerticalStreak = coordinate.LineIndex + streakLengthWithOutCharOfInterest < numberOfLines;

        bool hasLeftUpperDiagonalStreak = hasLeftHorizontalStreak && hasUpperVerticalStreak;
        bool hasRightUpperDiagonalStreak = hasRightHorizontalStreak && hasUpperVerticalStreak;
        bool hasLeftLowerDiagonalStreak = hasLeftHorizontalStreak && hasLowerVerticalStreak;
        bool hasRightLowerDiagonalStreak = hasRightHorizontalStreak && hasLowerVerticalStreak;

        if (hasLeftHorizontalStreak)
        {
            var coordinates = new List<Coordinate> { coordinate };

            for (var i = 0; i < streakLengthWithOutCharOfInterest; i++)
            {
                var lineIndex = coordinate.LineIndex;
                var charIndex = coordinate.CharIndex - (i + 1);
                coordinates.Add(new Coordinate(lineIndex, charIndex, input[lineIndex][charIndex]));
            }

            streaks.Add(new Streak(coordinates));
        }

        if (hasRightHorizontalStreak)
        {
            var coordinates = new List<Coordinate> { coordinate };

            for (var i = 0; i < streakLengthWithOutCharOfInterest; i++)
            {
                var lineIndex = coordinate.LineIndex;
                var charIndex = coordinate.CharIndex + i + 1;
                coordinates.Add(new Coordinate(lineIndex, charIndex, input[lineIndex][charIndex]));
            }

            streaks.Add(new Streak(coordinates));
        }

        if (hasUpperVerticalStreak)
        {
            var coordinates = new List<Coordinate> { coordinate };

            for (var i = 0; i < streakLengthWithOutCharOfInterest; i++)
            {
                var lineIndex = coordinate.LineIndex - (i + 1);
                var charIndex = coordinate.CharIndex;
                coordinates.Add(new Coordinate(lineIndex, charIndex, input[lineIndex][charIndex]));
            }

            streaks.Add(new Streak(coordinates));
        }

        if (hasLowerVerticalStreak)
        {
            var coordinates = new List<Coordinate> { coordinate };

            for (var i = 0; i < streakLengthWithOutCharOfInterest; i++)
            {
                var lineIndex = coordinate.LineIndex + i + 1;
                var charIndex = coordinate.CharIndex;
                coordinates.Add(new Coordinate(lineIndex, charIndex, input[lineIndex][charIndex]));
            }

            streaks.Add(new Streak(coordinates));
        }

        if (hasLeftUpperDiagonalStreak)
        {
            var coordinates = new List<Coordinate> { coordinate };

            for (var i = 0; i < streakLengthWithOutCharOfInterest; i++)
            {
                var lineIndex = coordinate.LineIndex - (i + 1);
                var charIndex = coordinate.CharIndex - (i + 1);
                coordinates.Add(new Coordinate(lineIndex, charIndex, input[lineIndex][charIndex]));
            }

            streaks.Add(new Streak(coordinates));
        }

        if (hasRightUpperDiagonalStreak)
        {
            var coordinates = new List<Coordinate> { coordinate };

            for (var i = 0; i < streakLengthWithOutCharOfInterest; i++)
            {
                var lineIndex = coordinate.LineIndex - (i + 1);
                var charIndex = coordinate.CharIndex + i + 1;
                coordinates.Add(new Coordinate(lineIndex, charIndex, input[lineIndex][charIndex]));
            }

            streaks.Add(new Streak(coordinates));
        }

        if (hasLeftLowerDiagonalStreak)
        {
            var coordinates = new List<Coordinate> { coordinate };

            for (var i = 0; i < streakLengthWithOutCharOfInterest; i++)
            {
                var lineIndex = coordinate.LineIndex + i + 1;
                var charIndex = coordinate.CharIndex - (i + 1);
                coordinates.Add(new Coordinate(lineIndex, charIndex, input[lineIndex][charIndex]));
            }

            streaks.Add(new Streak(coordinates));
        }

        if (hasRightLowerDiagonalStreak)
        {
            var coordinates = new List<Coordinate> { coordinate };

            for (var i = 0; i < streakLengthWithOutCharOfInterest; i++)
            {
                var lineIndex = coordinate.LineIndex + i + 1;
                var charIndex = coordinate.CharIndex + i + 1;
                coordinates.Add(new Coordinate(lineIndex, charIndex, input[lineIndex][charIndex]));
            }

            streaks.Add(new Streak(coordinates));
        }

        return streaks;
    }

    private static Cross? GetCoordinateCross(Coordinate coordinate, string[] input, string wordOfInterest)
    {
        var crossArmLength = (wordOfInterest.Length - 1) / 2;

        var streak1Directions = new List<Direction> { new Direction(-1, -1), new Direction(1, 1) };
        var streak2Directions = new List<Direction> { new Direction(-1, 1), new Direction(1, -1) };

        bool streak1Exist = ExistsStreak(coordinate, crossArmLength, streak1Directions, input);
        bool streak2Exist = ExistsStreak(coordinate, crossArmLength, streak2Directions, input);

        if (!(streak1Exist && streak2Exist))
        {
            return null;
        }

        var streak1StartingCoordinate = GetStreakStartingCoordinate(coordinate, streak1Directions[0], crossArmLength);
        var streak2StartingCoordinate = GetStreakStartingCoordinate(coordinate, streak2Directions[0], crossArmLength);

        var streak1 = GetCoordinateStreak(streak1StartingCoordinate, streak1Directions[1], wordOfInterest, input);
        var streak2 = GetCoordinateStreak(streak2StartingCoordinate, streak2Directions[1], wordOfInterest, input);

        return new Cross(streak1, streak2);
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

    private static bool ExistsStreak(Coordinate coordinate, int lengthToCheck, List<Direction> directions, string[] input)
    {
        var existsStreak = false;
        var numberOfLines = input.Length;
        var lineLength = input[coordinate.LineIndex].Length;

        foreach (var direction in directions)
        {
            existsStreak = coordinate.LineIndex + (direction.VerticalDirection * lengthToCheck) >= 0 &&
               coordinate.LineIndex + (direction.VerticalDirection * lengthToCheck) < numberOfLines &&
               coordinate.CharIndex + (direction.HorizontalDirection * lengthToCheck) >= 0 &&
               coordinate.CharIndex + (direction.HorizontalDirection * lengthToCheck) < lineLength;

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
