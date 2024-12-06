using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day4;

namespace AdventOfCode.Day4;

public class Part1
{
    /// <summary>
    /// --- Day 4: Ceres Search ---
    /// "Looks like the Chief's not here. Next!" One of The Historians pulls out a device and pushes the only button on it.
    /// After a brief flash, you recognize the interior of the Ceres monitoring station!
    /// 
    /// As the search for the Chief continues, a small Elf who lives on the station tugs on your shirt; she'd like to know 
    /// if you could help her with her word search (your puzzle input). She only has to find one word: XMAS.
    /// 
    /// This word search allows words to be horizontal, vertical, diagonal, written backwards, or even overlapping other words.
    /// It's a little unusual, though, as you don't merely need to find one instance of XMAS - you need to find all of them.
    /// Here are a few ways XMAS might appear, where irrelevant characters have been replaced with.:
    /// 
    /// 
    /// ..X...
    /// .SAMX.
    /// .A..A.
    /// XMAS.S
    /// .X....
    /// The actual word search will be full of letters instead. For example:
    /// 
    /// MMMSXXMASM
    /// MSAMXMSMSA
    /// AMXSXMAAMM
    /// MSAMASMSMX
    /// XMASAMXAMM
    /// XXAMMXXAMA
    /// SMSMSASXSS
    /// SAXAMASAAA
    /// MAMMMXMMMM
    /// MXMXAXMASX
    /// In this word search, XMAS occurs a total of 18 times; here's the same word search again, but where letters not involved in any XMAS have been replaced with .:
    /// 
    /// ....XXMAS.
    /// .SAMXMS...
    /// ...S..A...
    /// ..A.A.MS.X
    /// XMASAMX.MM
    /// X.....XA.A
    /// S.S.S.S.SS
    /// .A.A.A.A.A
    /// ..M.M.M.MM
    /// .X.X.XMASX
    /// Take a look at the little Elf's word search. How many times does XMAS appear?
    /// 
    /// To begin, get your puzzle input.
    /// 
    /// Answer: 
    /// 
    /// 
    /// 
    /// You can also[Share] this puzzle.
    /// </summary>


    public static int Solve(string fileName)
    {
        // read file
        var input = File.ReadAllLines($"Day4\\{fileName}");

        // declare wordOfInterest
        var wordOfInterest = "XMAS";

        // get x-coordinates
        var xCoordinates = XmasService.GetCoordinates(input, wordOfInterest[0]);

        // declare Directions
        var directions = new List<Direction>
        {
            new Direction(-1,-1),
            new Direction(-1,0),
            new Direction(-1,1),
            new Direction(0,-1),
            new Direction(0,1),
            new Direction(1,-1),
            new Direction(1,0),
            new Direction(1,1)
        };

        // get streaks
        var streaks = XmasService.GetStreaks(xCoordinates, input, wordOfInterest, directions);

        // get solution
        var solution = XmasService.ProcessStreaks(streaks, input, wordOfInterest);

        // return
        return solution;
    }
}
