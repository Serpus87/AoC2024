﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode.Day7;

public static class Part2
{
    /// <summary>
    /// Your puzzle answer was .
    /// 
    /// --- Part Two ---
    /// The engineers seem concerned; the total calibration result you gave them is nowhere close to being within safety tolerances.Just then, you spot your mistake: some well-hidden elephants are holding a third type of operator.
    /// 
    /// The concatenation operator (||) combines the digits from its left and right inputs into a single number.For example, 12 || 345 would become 12345. All operators are still evaluated left-to-right.
    /// 
    /// Now, apart from the three equations that could be made true using only addition and multiplication, the above example has three more equations that can be made true by inserting operators:
    /// 
    /// 156: 15 6 can be made true through a single concatenation: 15 || 6 = 156.
    /// 7290: 6 8 6 15 can be made true using 6 * 8 || 6 * 15.
    /// 192: 17 8 14 can be made true using 17 || 8 + 14.
    /// Adding up all six test values(the three that could be made before using only + and* plus the new three that can now be made by also using ||) produces the new total calibration result of 11387.
    /// 
    /// Using your new knowledge of elephant hiding spots, determine which equations could possibly be true. What is their total calibration result?
    /// 
    /// Your puzzle answer was 165278151522644.
    /// 
    /// Both parts of this puzzle are complete! They provide two gold stars: **
    /// 
    /// At this point, you should return to your Advent calendar and try another puzzle.
    /// 
    /// If you still want to see it, you can get your puzzle input.
    /// 
    /// You can also[Share] this puzzle.
    /// </summary>

    public static long Solve(Input input)
    {
        // declare operators
        var operators = new List<Operator> { Operator.Add, Operator.Multiply, Operator.Concatenate };

        // get solved equations
        var solvedEquations = EquationService.GetSolvedEquationsWithMoreThanTwoOperators(input.Equations, operators);

        // sum testvalues
        long result = solvedEquations.Sum(x => x.TestValue);
        return result;
    }
}
