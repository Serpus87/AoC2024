using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
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

    public static int Solve(List<long> stones, int numberOfTimesToBlink)
    {
        // part 1: 25 blinks:
        var stonesAfter25Blinks = BlinkingService.Blink(stones, 25);

        // part 2: 25 blinks, 25 blinks ,and count
        var result = 0;

        var stoneCounter = 0;
        foreach (var stoneAfter25Blinks in stonesAfter25Blinks)
        {
            stoneCounter++;
            Console.WriteLine($"StoneAfter25Blinks Number {stoneCounter} out of total {stonesAfter25Blinks.Count} number of stones");
            var stonesAfter50Blinks = BlinkingService.Blink(new List<long> { stoneAfter25Blinks }, 25);

            foreach (var stoneAfter50Blinks in stonesAfter50Blinks)
            {
                result += BlinkingService.Blink(new List<long> { stoneAfter50Blinks }, 25).Count;
            }
        }

        return result;
    }
}
