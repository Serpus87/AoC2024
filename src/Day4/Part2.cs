using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day4;

namespace AdventOfCode.Day4;

public class Part2
{
    /// <summary>
    /// Your puzzle answer was .
    ///
    ///    The first half of this puzzle is complete! It provides one gold star: *
    ///
    ///--- Part Two ---
    ///The Elf looks quizzically at you.Did you misunderstand the assignment?
    ///
    ///Looking for the instructions, you flip over the word search to find that this isn't actually an XMAS puzzle; it's an X-MAS puzzle in which you're supposed to find two MAS in the shape of an X. One way to achieve that is like this:
    ///
    ///M.S
    ///.A.
    ///M.S
    ///Irrelevant characters have again been replaced with . in the above diagram.Within the X, each MAS can be written forwards or backwards.
    ///
    ///Here's the same example from before, but this time all of the X-MASes have been kept instead:
    ///
    ///.M.S......
    ///..A..MSMS.
    ///.M.S.MAA..
    ///..A.ASMSM.
    ///.M.S.M....
    ///..........
    ///S.S.S.S.S.
    ///.A.A.A.A..
    ///M.M.M.M.M.
    ///..........
    ///In this example, an X-MAS appears 9 times.
    ///
    ///Flip the word search from the instructions back over to the word search side and try again.How many times does an X-MAS appear?
    ///
    ///Answer: 
    ///
    ///
    ///
    ///Although it hasn't changed, you can still get your puzzle input.
    ///
    ///You can also[Share] this puzzle.
    /// </summary>


    public static int Solve(string fileName)
    {
        // read file
        var input = File.ReadAllLines($"Day4\\{fileName}");

        // declare wordOfInterest
        var wordOfInterest = "MAS";

        // get a-coordinates
        var aCoordinates = XmasService.GetCoordinates(input, wordOfInterest[1]);

        // get crosses
        var crosses = XmasService.GetCrosses(aCoordinates, input, wordOfInterest);

        // get solution
        var solution = XmasService.ProcessCrosses(crosses, input, wordOfInterest);

        // return
        return solution;
    }
}
