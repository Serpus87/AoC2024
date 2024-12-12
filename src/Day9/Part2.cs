using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day9.Models;
using Microsoft.VisualBasic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode.Day9;

public static class Part2
{
    /// <summary>
    /// Your puzzle answer was 6341711060162.

    ///  The first half of this puzzle is complete! It provides one gold star: *
    /// 
    /// --- Part Two ---
    /// Upon completion, two things immediately become clear.First, the disk definitely has a lot more contiguous free space, just like the amphipod hoped.Second, the computer is running much more slowly! Maybe introducing all of that file system fragmentation was a bad idea?
    /// 
    /// The eager amphipod already has a new plan: rather than move individual blocks, he'd like to try compacting the files on his disk by moving whole files instead.
    /// 
    /// This time, attempt to move whole files to the leftmost span of free space blocks that could fit the file. Attempt to move each file exactly once in order of decreasing file ID number starting with the file with the highest file ID number.If there is no span of free space to the left of a file that is large enough to fit the file, the file does not move.
    /// 
    /// The first example from above now proceeds differently:
    /// 
    /// 00...111...2...333.44.5555.6666.777.888899
    /// 0099.111...2...333.44.5555.6666.777.8888..
    /// 0099.1117772...333.44.5555.6666.....8888..
    /// 0099.111777244.333....5555.6666.....8888..
    /// 00992111777.44.333....5555.6666.....8888..
    /// The process of updating the filesystem checksum is the same; now, this example's checksum would be 2858.
    /// 
    /// Start over, now compacting the amphipod's hard drive using this new method instead. What is the resulting filesystem checksum?
    /// 
    /// Answer: 
    ///  
    /// 
    /// Although it hasn't changed, you can still get your puzzle input.
    /// 
    /// You can also[Share] this puzzle.
    ///  </summary>
    public static int Solve(DiskMap diskMap)
    {
        int result = 0;
        return result;
    }
}
