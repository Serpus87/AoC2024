using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day5;

public class Part2
{
    /// <summary>
    /// Your puzzle answer was 4609.
    /// 
    /// The first half of this puzzle is complete! It provides one gold star: *
    /// 
    /// --- Part Two ---
    /// While the Elves get to work printing the correctly-ordered updates, you have a little time to fix the rest of them.
    /// 
    /// For each of the incorrectly-ordered updates, use the page ordering rules to put the page numbers in the right order.For the above example, 
    /// here are the three incorrectly-ordered updates and their correct orderings:
    /// 
    /// 75,97,47,61,53 becomes 97,75,47,61,53.
    /// 61,13,29 becomes 61,29,13.
    /// 97,13,75,29,47 becomes 97,75,47,29,13.
    /// After taking only the incorrectly-ordered updates and ordering them correctly, their middle page numbers are 47, 29, and 47. Adding these together produces 123.
    /// 
    /// Find the updates which are not in the correct order.What do you get if you add up the middle page numbers after correctly ordering just those updates?
    /// 
    /// Answer: 
    /// 
    /// 
    /// 
    /// Although it hasn't changed, you can still get your puzzle input.
    /// 
    /// You can also[Share] this puzzle.
    /// <returns></returns>

    public static int Solve(List<OrderingRule> orderingRules, List<Update> updates)
    {
        // get Updates that are not in correct order
        var incorrectlyOrderedUpdates = UpdateService.GetInCorrectlyOrderedUpdates(updates, orderingRules);

        // order
        var correctlyOrderedUpdates = UpdateService.OrderUpdates(incorrectlyOrderedUpdates, orderingRules);

        // get middleNumbers
        var middlePageNumbers = UpdateService.GetMiddlePageNumbers(correctlyOrderedUpdates);

        // return sum
        return middlePageNumbers.Sum();
    }
}
