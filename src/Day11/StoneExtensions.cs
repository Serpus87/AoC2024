using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day11;

public static class StoneExtensions
{
    public static List<Stone> Collect(this List<Stone> stones)
    {
        var collectedStones = new List<Stone>();

        var distinctNumbers = stones.Select(x=>x.Number).Distinct();

        foreach (var number in distinctNumbers)
        {
            var stonesWithNumber = stones.Where(x=>x.Number == number).ToList();

            collectedStones.Add(stonesWithNumber.AddCounts());
        }

        return collectedStones;
    }

    public static Stone AddCounts(this List<Stone> stones)
    {
        var newCount = stones.Sum(x => x.Count);
        var number = stones.First().Number;

        return new Stone(number, newCount);
    }
}
