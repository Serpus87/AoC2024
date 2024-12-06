using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day5;

public static class InputReader
{
    public static List<OrderingRule> GetOrderingRules(string[] input)
    {
        var orderingRules = new List<OrderingRule>();

        foreach (var line in input)
        {
            if (line.Length == 0)
            {
                break;
            }

            var ruleNumbers = line.Split('|');

            orderingRules.Add(new OrderingRule(int.Parse(ruleNumbers[0]), int.Parse(ruleNumbers[1])));
        }

        return orderingRules;
    }

    public static List<Update> GetUpdates(string[] input)
    {
        var updates = new List<Update>();
        var isAfterWhiteLine = false;

        foreach (var line in input)
        {
            if (line.Length == 0)
            {
                isAfterWhiteLine = true;
                continue;
            }
            if (!isAfterWhiteLine)
            {
                continue;
            }

            var pages = line.Split(',');

            updates.Add(new Update(pages.Select(int.Parse).ToList()));
        }

        return updates;
    }
}
