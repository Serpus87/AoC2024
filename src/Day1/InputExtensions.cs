using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day1;

public static class InputExtensions
{
    public static Input Sort(this Input input)
    {
        input.FirstLocationIds.Sort();
        input.SecondLocationIds.Sort();

        return input;
    }

    public static List<int> SubtractLocationIds(this Input input)
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

    public static Input GetIdCounts(this Input input)
    {
        var countedIds = new Input();
        countedIds.FirstLocationIds = input.FirstLocationIds;

        foreach (var leftId in input.FirstLocationIds)
        {
            var countsInRightList = input.SecondLocationIds.Count(x => x.Equals(leftId));
            countedIds.SecondLocationIds.Add(countsInRightList);
        }

        return countedIds;
    }

    public static List<int> MultiplyIdsWithCounts(this Input input)
    {
        var result = new List<int>();

        for (int i = 0; i < input.FirstLocationIds.Count; i++)
        {
            var firstLocationId = input.FirstLocationIds[i];
            var secondLocationId = input.SecondLocationIds[i];
            var factorOfIds = firstLocationId * secondLocationId;

            result.Add(factorOfIds);
        }

        return result;
    }
}
