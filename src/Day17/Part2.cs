﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day17.Models;
using AdventOfCode.Day17.Services;
using Microsoft.Win32;

namespace AdventOfCode.Day17;

public static class Part2
{
    /// <summary>
    /// Your puzzle answer was 2,1,0,1,7,2,5,0,3.
    /// 
    /// The first half of this puzzle is complete! It provides one gold star: *
    /// 
    /// --- Part Two ---
    /// Digging deeper in the device's manual, you discover the problem: this program is supposed to output another copy of the program! Unfortunately, the value in register A seems to have been corrupted. You'll need to find a new value to which you can initialize register A so that the program's output instructions produce an exact copy of the program itself.
    /// 
    /// For example:
    /// 
    /// Register A: 2024
    /// Register B: 0
    /// Register C: 0
    /// 
    /// Program: 0,3,5,4,3,0
    /// This program outputs a copy of itself if register A is instead initialized to 117440. (The original initial value of register A, 2024, is ignored.)
    /// 
    /// What is the lowest positive initial value for register A that causes the program to output a copy of itself?
    /// 
    /// Answer: 
    ///  
    /// 
    /// Although it hasn't changed, you can still get your puzzle input.
    /// 
    /// You can also[Share] this puzzle.
    /// </summary>
    public static int Solve(List<Register> registers, List<int> programInput)
    {
        var result = ComputerService.FindInitialAValue(programInput,registers);

        return result;
    }
}
