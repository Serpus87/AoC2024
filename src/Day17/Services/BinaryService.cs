using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode.Day17.Services;

public static class BinaryService
{
    public static uint CalculateBitWiseXOR(uint firstInt, uint secondInt)
    {
        if (secondInt == 7)
        {
            return (uint)(8 * (int)Math.Ceiling((decimal)firstInt / 8) - firstInt % 8 - 1);
        }

        var firstString = Convert.ToString(firstInt, 2);
        var secondString = Convert.ToString(secondInt, 2);

        var stringLengthDifference = firstString.Length - secondString.Length;

        if (stringLengthDifference > 0)
        {
            var stringStart = new string('0', Math.Abs(stringLengthDifference));
            secondString = stringStart + secondString;
        }

        if (stringLengthDifference < 0)
        {
            var stringStart = new string('0', Math.Abs(stringLengthDifference));
            firstString = stringStart + firstString;
        }

        var bitWiseXORString = GetBitWiseXOR(firstString, secondString);
        var result = Convert.ToUInt32(bitWiseXORString, 2);

        return result;
    }

    private static string GetBitWiseXOR(string firstString, string secondString)
    {
        var bitWiseXORString = string.Empty;

        for (var i = 0; i < firstString.Length; i++)
        {
            var firstBit = firstString[i];
            var secondBit = secondString[i];

            if (firstBit == secondBit)
            {
                bitWiseXORString += "0";
            }
            else
            {
                bitWiseXORString += "1";
            }
        }

        return bitWiseXORString;
    }
}
