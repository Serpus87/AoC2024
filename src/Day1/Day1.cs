using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day1;

internal static class Day1
{
    internal static void Solve()
    {
        // readInput
        var input = InputReader.Read();

        // sortInput
        var sortedInput = input.Sort();

        // subtract lists
        var subtractedInput = sortedInput.SubstractLocationIds();

        // sum
        var totalDistance = subtractedInput.Sum();

        // print solution
        Console.WriteLine(totalDistance);
    }
}
