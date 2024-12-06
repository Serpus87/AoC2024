using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day5;

public static class UpdateService
{
    public static List<Update> GetCorrectlyOrderedUpdates(List<Update> updates, List<OrderingRule> orderingsRules)
    {
        var correctlyOrderedUpdates = new List<Update>();

        foreach (var update in updates)
        {
            bool isCorrectlyOrdered = IsCorrectlyOrdered(update, orderingsRules);

            if (isCorrectlyOrdered)
            {
                correctlyOrderedUpdates.Add(update);
            }
        }

        return correctlyOrderedUpdates;
    }

    internal static List<int> GetMiddlePageNumbers(List<Update> correctlyOrderedUpdates)
    {
        var middlePageNumbers = new List<int>();

        foreach (var update in correctlyOrderedUpdates)
        {
            var middleIndex = (update.PageNumbers.Count - 1) / 2;
            middlePageNumbers.Add(update.PageNumbers.Skip(middleIndex).First());
        }

        return middlePageNumbers;
    }

    private static bool IsCorrectlyOrdered(Update update, List<OrderingRule> orderingsRules)
    {
        for (int i = 0; i < update.PageNumbers.Count; i++)
        {
            var pageNumber = update.PageNumbers[i];
            var nextNumbers = update.PageNumbers.Skip(i + 1).Take(update.PageNumbers.Count - i);
            var pageNumbersThatShouldBePrintedBeforePageNumber = orderingsRules.Where(x => x.SecondPageNumber.Equals(pageNumber)).Select(x => x.FirstPageNumber).ToList();

            if (nextNumbers.Any(x => pageNumbersThatShouldBePrintedBeforePageNumber.Any(y => y == x)))
            {
                return false;
            }
        }

        return true;
    }
}
