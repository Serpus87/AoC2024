using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day3;

public static class MulService
{
    private const int MinMulNumberLength = 1;
    private const int MaxMulNumberLength = 3;
    private const string MulStart = "mul(";
    private const int MulStartLength = 4;
    private const string MulEnd = ")";
    private const string MulSeparator = ",";
    private const int MulMaxLengthInCludingStartAndEnd = 12;

    public static List<Mul> ExtractMuls(string input)
    {
        var muls = new List<Mul>();

        while (!string.IsNullOrEmpty(input))
        {
            // find MulStart
            var indexMulStart = input.IndexOf(MulStart);

            // get string that possibly includes a Mul
            var possibleMulLength = indexMulStart + MulMaxLengthInCludingStartAndEnd < input.Length ? MulMaxLengthInCludingStartAndEnd : input.Length - indexMulStart;
            var possibleMul = input.Substring(indexMulStart, possibleMulLength);

            // extract mul if possible
            var mul = ExtractMulIfPossible(possibleMul);

            // if true Mul found, then add to muls
            if (mul != null)
            {
                muls.Add(mul);
            }

            // remove everything until next MulStart
            input = RemoveEverythingUntilNextMulStart(input, indexMulStart);
        }

        return muls;
    }

    public static List<int> Multiply(this List<Mul> muls)
    {
        var multipliedMuls = new List<int>();

        foreach (var mul in muls)
        {
            multipliedMuls.Add(mul.Number1 * mul.Number2);
        }

        return multipliedMuls;
    }

    private static Mul? ExtractMulIfPossible(string possibleMul)
    {
        var hasMulEnd = possibleMul.Contains(MulEnd);

        if (!hasMulEnd)
        {
            return null;
        }

        var hasSeparator = possibleMul.Contains(MulSeparator);

        if (!hasSeparator)
        {
            return null;
        }

        var mulEndIsAfterSeperator = possibleMul.IndexOf(MulEnd) > possibleMul.IndexOf(MulSeparator);

        if (!mulEndIsAfterSeperator)
        {
            return null;
        }

        var indexOfSeperator = possibleMul.IndexOf(MulSeparator);
        var possibleNumber1 = possibleMul.Substring(MulStartLength, indexOfSeperator - MulStartLength);

        int.TryParse(possibleNumber1, out var number1);

        if (number1 == 0)
        {
            return null;
        }

        var indexOfMulEnd = possibleMul.IndexOf(MulEnd);
        var possibleNumber2 = possibleMul.Substring(indexOfSeperator + 1, indexOfMulEnd - (indexOfSeperator + 1));

        int.TryParse(possibleNumber2, out var number2);

        if (number2 == 0)
        {
            return null;
        }

        return new Mul(number1,number2);
    }

    private static string RemoveEverythingUntilNextMulStart(string input, int indexMulStart)
    {
        var removedUntilAfterFirstMulStart = input.Remove(0, indexMulStart + MulStartLength);

        var indexNextMulStart = removedUntilAfterFirstMulStart.IndexOf(MulStart);

        if (indexNextMulStart < 0)
        {
            return string.Empty;
        }

        var newInput = removedUntilAfterFirstMulStart.Remove(0, indexNextMulStart);
        return newInput;
    }
}
