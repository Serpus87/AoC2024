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

    public static int ProcessStreaks(List<Streak> streaks, string[] input, string wordOfInterest)
    {
        return streaks.Count(x => x.StreakSpellsWordOfInterest(wordOfInterest)); ;
    }

    // todo simplify and generalize this
    private static List<Streak> GetCoordinateStreaks(Coordinate xCoordinate, string[] input, string wordOfInterest)
    {
        var streaks = new List<Streak>();

        var streakLengthWithOutCharOfInterest = wordOfInterest.Length - 1;
        var lineLength = input[xCoordinate.LineIndex].Length;
        var numberOfLines = input.Length;

        bool hasLeftHorizontalStreak = xCoordinate.CharIndex - streakLengthWithOutCharOfInterest >= 0;
        bool hasRightHorizontalStreak = xCoordinate.CharIndex + streakLengthWithOutCharOfInterest < lineLength;
        bool hasUpperVerticalStreak = xCoordinate.LineIndex - streakLengthWithOutCharOfInterest >= 0;
        bool hasLowerVerticalStreak = xCoordinate.LineIndex + streakLengthWithOutCharOfInterest < numberOfLines;

        bool hasLeftUpperDiagonalStreak = hasLeftHorizontalStreak && hasUpperVerticalStreak;
        bool hasRightUpperDiagonalStreak = hasRightHorizontalStreak && hasUpperVerticalStreak;
        bool hasLeftLowerDiagonalStreak = hasLeftHorizontalStreak && hasLowerVerticalStreak;
        bool hasRightLowerDiagonalStreak = hasRightHorizontalStreak && hasLowerVerticalStreak;

        if (hasLeftHorizontalStreak)
        {
            var coordinates = new List<Coordinate> { xCoordinate };

            for (var i = 0; i < streakLengthWithOutCharOfInterest; i++)
            {
                var lineIndex = xCoordinate.LineIndex;
                var charIndex = xCoordinate.CharIndex - (i + 1);
                coordinates.Add(new Coordinate(lineIndex, charIndex, input[lineIndex][charIndex]));
            }

            streaks.Add(new Streak(coordinates));
        }

        if (hasRightHorizontalStreak)
        {
            var coordinates = new List<Coordinate> { xCoordinate };

            for (var i = 0; i < streakLengthWithOutCharOfInterest; i++)
            {
                var lineIndex = xCoordinate.LineIndex;
                var charIndex = xCoordinate.CharIndex + i + 1;
                coordinates.Add(new Coordinate(lineIndex, charIndex, input[lineIndex][charIndex]));
            }

            streaks.Add(new Streak(coordinates));
        }

        if (hasUpperVerticalStreak)
        {
            var coordinates = new List<Coordinate> { xCoordinate };

            for (var i = 0; i < streakLengthWithOutCharOfInterest; i++)
            {
                var lineIndex = xCoordinate.LineIndex - (i + 1);
                var charIndex = xCoordinate.CharIndex;
                coordinates.Add(new Coordinate(lineIndex, charIndex, input[lineIndex][charIndex]));
            }

            streaks.Add(new Streak(coordinates));
        }

        if (hasLowerVerticalStreak)
        {
            var coordinates = new List<Coordinate> { xCoordinate };

            for (var i = 0; i < streakLengthWithOutCharOfInterest; i++)
            {
                var lineIndex = xCoordinate.LineIndex + i + 1;
                var charIndex = xCoordinate.CharIndex;
                coordinates.Add(new Coordinate(lineIndex, charIndex, input[lineIndex][charIndex]));
            }

            streaks.Add(new Streak(coordinates));
        }

        if (hasLeftUpperDiagonalStreak)
        {
            var coordinates = new List<Coordinate> { xCoordinate };

            for (var i = 0; i < streakLengthWithOutCharOfInterest; i++)
            {
                var lineIndex = xCoordinate.LineIndex - (i + 1);
                var charIndex = xCoordinate.CharIndex - (i + 1);
                coordinates.Add(new Coordinate(lineIndex, charIndex, input[lineIndex][charIndex]));
            }

            streaks.Add(new Streak(coordinates));
        }

        if (hasRightUpperDiagonalStreak)
        {
            var coordinates = new List<Coordinate> { xCoordinate };

            for (var i = 0; i < streakLengthWithOutCharOfInterest; i++)
            {
                var lineIndex = xCoordinate.LineIndex - (i + 1);
                var charIndex = xCoordinate.CharIndex + i + 1;
                coordinates.Add(new Coordinate(lineIndex, charIndex, input[lineIndex][charIndex]));
            }

            streaks.Add(new Streak(coordinates));
        }

        if (hasLeftLowerDiagonalStreak)
        {
            var coordinates = new List<Coordinate> { xCoordinate };

            for (var i = 0; i < streakLengthWithOutCharOfInterest; i++)
            {
                var lineIndex = xCoordinate.LineIndex + i + 1;
                var charIndex = xCoordinate.CharIndex - (i + 1);
                coordinates.Add(new Coordinate(lineIndex, charIndex, input[lineIndex][charIndex]));
            }

            streaks.Add(new Streak(coordinates));
        }

        if (hasRightLowerDiagonalStreak)
        {
            var coordinates = new List<Coordinate> { xCoordinate };

            for (var i = 0; i < streakLengthWithOutCharOfInterest; i++)
            {
                var lineIndex = xCoordinate.LineIndex + i + 1;
                var charIndex = xCoordinate.CharIndex + i + 1;
                coordinates.Add(new Coordinate(lineIndex, charIndex, input[lineIndex][charIndex]));
            }

            streaks.Add(new Streak(coordinates));
        }

        return streaks;
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
