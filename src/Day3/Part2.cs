using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode.Day3;

public class Part2
{
    /// <summary>
    /// Your puzzle answer was .
    /// 
    /// The first half of this puzzle is complete! It provides one gold star: *
    /// 
    /// --- Part Two ---
    /// As you scan through the corrupted memory, you notice that some of the conditional statements are also still intact.
    /// If you handle some of the uncorrupted conditional statements in the program, you might be able to get an even more 
    /// accurate result.
    /// 
    /// There are two new instructions you'll need to handle:
    /// 
    /// The do() instruction enables future mul instructions.
    /// The don't() instruction disables future mul instructions.
    /// Only the most recent do() or don't() instruction applies. At the beginning of the program, mul instructions are enabled.
    /// 
    /// For example:
    /// 
    /// xmul(2,4)&mul[3, 7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))
    /// This corrupted memory is similar to the example from before, but this time the mul(5,5) and mul(11,8) instructions are 
    /// disabled because there is a don't() instruction before them. The other mul instructions function normally, including 
    /// the one at the end that gets re-enabled by a do() instruction.
    /// 
    /// This time, the sum of the results is 48 (2*4 + 8*5).
    /// 
    /// Handle the new instructions; what do you get if you add up all of the results of just the enabled multiplications?
    /// 
    /// Answer: 
    ///  
    /// 
    /// Although it hasn't changed, you can still get your puzzle input.
    /// 
    /// You can also[Share] this puzzle.
    /// </summary>


    public static int Solve(string input)
    {
        // get muls
        var muls = MulService.ExtractMulsWithInstructions(input);

        // multiply muls
        var multipliedMuls = muls.Multiply();

        // sum
        var sumOfMultipliedMults = multipliedMuls.Sum();

        // return
        return sumOfMultipliedMults;
    }
}
