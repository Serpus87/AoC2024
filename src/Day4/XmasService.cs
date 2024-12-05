using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day4;

public static class XmasService
{
    private const int StreakLengthWithOutX = 3;

    public static List<Coordinate> GetXCoordinates(string[] input)
    {
        var xCoordinates = new List<Coordinate>();

        for (int i = 0; i < input.Length; i++)
        {
            xCoordinates.AddRange(GetXCoordinatesInLine(input[i], i));
        }

        return xCoordinates;
    }

    public static List<Streak> GetStreaks(List<Coordinate> coordinates, string[] input)
    {
        var streaks = new List<Streak>();
        var numberOfLines = input.Length;

        foreach (Coordinate coordinate in coordinates)
        {
            var lineLength = input[coordinate.LineIndex].Length;
            streaks.AddRange(GetCoordinateStreaks(coordinate, numberOfLines, lineLength));
        }

        return streaks;
    }

    public static int ProcessStreaks(List<Streak> streaks, string[] input)
    {
        var xmasCounter = 0;

        foreach (var streak in streaks)
        {
            if(Enum.TryParse(input[streak.MCoordinate.LineIndex][streak.MCoordinate.CharIndex].ToString(), out Mark actualMMark))
            {
                streak.MCoordinate.ActualMark = actualMMark;
            };
            if(Enum.TryParse(input[streak.ACoordinate.LineIndex][streak.ACoordinate.CharIndex].ToString(), out Mark actualAMark))
            {
                streak.ACoordinate.ActualMark = actualAMark;
            };
            if(Enum.TryParse(input[streak.SCoordinate.LineIndex][streak.SCoordinate.CharIndex].ToString(), out Mark actualSMark)){
                streak.SCoordinate.ActualMark = actualSMark;
            };

            if (streak.StreakSpellsXmas())
            {
                xmasCounter++;
            }
        }

        return xmasCounter;
    }

    private static List<Streak> GetCoordinateStreaks(Coordinate xCoordinate, int numberOfLines, int lineLength)
    {
        var streaks = new List<Streak>();

        bool hasLeftHorizontalStreak = xCoordinate.CharIndex - StreakLengthWithOutX >= 0;
        bool hasRightHorizontalStreak = xCoordinate.CharIndex + StreakLengthWithOutX < lineLength;
        bool hasUpperVerticalStreak = xCoordinate.LineIndex - StreakLengthWithOutX >= 0;
        bool hasLowerVerticalStreak = xCoordinate.LineIndex + StreakLengthWithOutX < numberOfLines;

        bool hasLeftUpperDiagonalStreak = hasLeftHorizontalStreak && hasUpperVerticalStreak;
        bool hasRightUpperDiagonalStreak = hasRightHorizontalStreak && hasUpperVerticalStreak;
        bool hasLeftLowerDiagonalStreak = hasLeftHorizontalStreak && hasLowerVerticalStreak;
        bool hasRightLowerDiagonalStreak = hasRightHorizontalStreak && hasLowerVerticalStreak;

        if (hasLeftHorizontalStreak)
        {
            var mCoordinate = new Coordinate(xCoordinate.LineIndex, xCoordinate.CharIndex - 1, Mark.M);
            var aCoordinate = new Coordinate(xCoordinate.LineIndex, xCoordinate.CharIndex - 2, Mark.A);
            var sCoordinate = new Coordinate(xCoordinate.LineIndex, xCoordinate.CharIndex - 3, Mark.S);
            streaks.Add(new Streak(xCoordinate, mCoordinate, aCoordinate, sCoordinate));
        }

        if (hasRightHorizontalStreak)
        {
            var mCoordinate = new Coordinate(xCoordinate.LineIndex, xCoordinate.CharIndex + 1, Mark.M);
            var aCoordinate = new Coordinate(xCoordinate.LineIndex, xCoordinate.CharIndex + 2, Mark.A);
            var sCoordinate = new Coordinate(xCoordinate.LineIndex, xCoordinate.CharIndex + 3, Mark.S);
            streaks.Add(new Streak(xCoordinate, mCoordinate, aCoordinate, sCoordinate));
        }

        if (hasUpperVerticalStreak)
        {
            var mCoordinate = new Coordinate(xCoordinate.LineIndex - 1, xCoordinate.CharIndex, Mark.M);
            var aCoordinate = new Coordinate(xCoordinate.LineIndex - 2, xCoordinate.CharIndex, Mark.A);
            var sCoordinate = new Coordinate(xCoordinate.LineIndex - 3, xCoordinate.CharIndex, Mark.S);
            streaks.Add(new Streak(xCoordinate, mCoordinate, aCoordinate, sCoordinate));
        }

        if (hasLowerVerticalStreak)
        {
            var mCoordinate = new Coordinate(xCoordinate.LineIndex + 1, xCoordinate.CharIndex, Mark.M);
            var aCoordinate = new Coordinate(xCoordinate.LineIndex + 2, xCoordinate.CharIndex, Mark.A);
            var sCoordinate = new Coordinate(xCoordinate.LineIndex + 3, xCoordinate.CharIndex, Mark.S);
            streaks.Add(new Streak(xCoordinate, mCoordinate, aCoordinate, sCoordinate));
        }

        if (hasLeftUpperDiagonalStreak)
        {
            var mCoordinate = new Coordinate(xCoordinate.LineIndex - 1, xCoordinate.CharIndex - 1, Mark.M);
            var aCoordinate = new Coordinate(xCoordinate.LineIndex - 2, xCoordinate.CharIndex - 2, Mark.A);
            var sCoordinate = new Coordinate(xCoordinate.LineIndex - 3, xCoordinate.CharIndex - 3, Mark.S);
            streaks.Add(new Streak(xCoordinate, mCoordinate, aCoordinate, sCoordinate));
        }

        if (hasRightUpperDiagonalStreak)
        {
            var mCoordinate = new Coordinate(xCoordinate.LineIndex - 1, xCoordinate.CharIndex + 1, Mark.M);
            var aCoordinate = new Coordinate(xCoordinate.LineIndex - 2, xCoordinate.CharIndex + 2, Mark.A);
            var sCoordinate = new Coordinate(xCoordinate.LineIndex - 3, xCoordinate.CharIndex + 3, Mark.S);
            streaks.Add(new Streak(xCoordinate, mCoordinate, aCoordinate, sCoordinate));
        }

        if (hasLeftLowerDiagonalStreak)
        {
            var mCoordinate = new Coordinate(xCoordinate.LineIndex + 1, xCoordinate.CharIndex - 1, Mark.M);
            var aCoordinate = new Coordinate(xCoordinate.LineIndex + 2, xCoordinate.CharIndex - 2, Mark.A);
            var sCoordinate = new Coordinate(xCoordinate.LineIndex + 3, xCoordinate.CharIndex - 3, Mark.S);
            streaks.Add(new Streak(xCoordinate, mCoordinate, aCoordinate, sCoordinate));
        }

        if (hasRightLowerDiagonalStreak)
        {
            var mCoordinate = new Coordinate(xCoordinate.LineIndex + 1, xCoordinate.CharIndex + 1, Mark.M);
            var aCoordinate = new Coordinate(xCoordinate.LineIndex + 2, xCoordinate.CharIndex + 2, Mark.A);
            var sCoordinate = new Coordinate(xCoordinate.LineIndex + 3, xCoordinate.CharIndex + 3, Mark.S);
            streaks.Add(new Streak(xCoordinate, mCoordinate, aCoordinate, sCoordinate));
        }

        return streaks;
    }

    private static List<Coordinate> GetXCoordinatesInLine(string line, int lineIndex)
    {
        var xIndices = new List<Coordinate>();
        var lineHasX = true;
        var startIndex = 0;

        while (lineHasX && startIndex < line.Length)
        {
            var xIndex = line.IndexOf(Mark.X.ToString(), startIndex);

            if (xIndex == -1)
            {
                lineHasX = false;
            }
            else
            {
                startIndex = xIndex + 1;
                xIndices.Add(new Coordinate(lineIndex, xIndex));
            }
        }

        return xIndices;
    }

    public static int ExtractXmas(string[] input)
    {
        int result = 0;

        for (int i = 0; i < input.Length; i++)
        {
            input[i].IndexOf("X");
        }

        return result;
    }
}
