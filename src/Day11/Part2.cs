using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day11;

public static class Part2
{
    /// <summary>
    /// Your puzzle answer was .
    ///  The first half of this puzzle is complete! It provides one gold star: *
    /// 
    /// --- Part Two ---
    /// The Historians sure are taking a long time.To be fair, the infinite corridors are very large.
    /// 
    /// How many stones would you have after blinking a total of 75 times?
    /// 
    /// Answer: 
    /// 
    /// 
    /// 
    /// Although it hasn't changed, you can still get your puzzle input.
    /// 
    /// You can also[Share] this puzzle.
    /// </summary>

    public static long Solve(List<long> stoneNumbers, int numberOfTimesToBlink)
    {
        // Create Stones
        var stones = new List<Stone>();

        foreach (var stoneNumber in stoneNumbers) 
        {
            stones.Add(new Stone(stoneNumber));
        }

        // Blink with disregard of instructions
        var stonesAfterBlinking = BlinkingService.BlinkWithDisregardOfInstructions(stones,numberOfTimesToBlink);

        return stonesAfterBlinking.Sum(x=>x.Count);
    }
}
