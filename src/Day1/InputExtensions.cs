using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day1;

internal static class InputExtensions
{
    public static Input Sort(this Input input)
    {
        input.FirstLocationIds.Sort();
        input.SecondLocationIds.Sort();

        return input;
    }

    public static List<int> Sort(this List<int> locationIds)
    {
        return locationIds.Order().ToList();
    }

    public static List<int> SubstractLocationIds(this Input input)
    {
        var subtractedInput = new List<int>();

        for (int i = 0; i < input.FirstLocationIds.Count; i++)
        {
            var firstLocationId = input.FirstLocationIds[i];
            var secondLocationId = input.SecondLocationIds[i];
            var differenceInLocations = Math.Abs(firstLocationId - secondLocationId);

            subtractedInput.Add(differenceInLocations);
        }

        return subtractedInput;
    }
}
