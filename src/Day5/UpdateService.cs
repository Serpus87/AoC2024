using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day5;

public static class UpdateService
{
    public static List<Update> GetCorrectlyOrderedUpdates(List<Update> updates, List<OrderingRule> orderingRules)
    {
        return updates.Where(x => IsCorrectlyOrdered(x, orderingRules)).ToList();
    }

    public static List<Update> GetInCorrectlyOrderedUpdates(List<Update> updates, List<OrderingRule> orderingRules)
    {
        return updates.Where(x => !IsCorrectlyOrdered(x, orderingRules)).ToList();
    }

    public static List<Update> OrderUpdates(List<Update> incorrectlyOrderedUpdates, List<OrderingRule> orderingRules)
    {
        return incorrectlyOrderedUpdates.Select(x => OrderUpdate(x, orderingRules)).ToList();
    }

    private static Update OrderUpdate(Update update, List<OrderingRule> orderingRules)
    {
        bool correctlyOrdered = false;

        while (!correctlyOrdered)
        {
            for (int i = 0; i < update.PageNumbers.Count; i++)
            {
                var pageNumber = update.PageNumbers[i];
                var nextNumbers = update.PageNumbers.Skip(i + 1).Take(update.PageNumbers.Count - i);
                var pageNumbersThatShouldBePrintedBeforePageNumber = orderingRules.Where(x => x.SecondPageNumber.Equals(pageNumber)).Select(x => x.FirstPageNumber).ToList();

                if (nextNumbers.Any(x => pageNumbersThatShouldBePrintedBeforePageNumber.Any(y => y == x)))
                {
                    var nextNumbersThatShouldBePrintedBeforePageNumber = nextNumbers.Where(x => pageNumbersThatShouldBePrintedBeforePageNumber.Any(y => y == x));
                    var nextNumbersThatShouldBePrintedAfterPageNumber = nextNumbers.Where(x => !pageNumbersThatShouldBePrintedBeforePageNumber.Any(y => y == x));
                    var numbersUntilPageNumber = update.PageNumbers.Take(i + 1);

                    var orderedPageNumbers = new List<int>();
                    orderedPageNumbers.AddRange(nextNumbersThatShouldBePrintedBeforePageNumber);
                    orderedPageNumbers.AddRange(numbersUntilPageNumber);
                    orderedPageNumbers.AddRange(nextNumbersThatShouldBePrintedAfterPageNumber);
                    update = new Update(orderedPageNumbers);

                    break;
                }
            }

            correctlyOrdered = IsCorrectlyOrdered(update, orderingRules);
        }

        return update;
    }

    public static List<int> GetMiddlePageNumbers(List<Update> correctlyOrderedUpdates)
    {
        var middlePageNumbers = new List<int>();

        foreach (var update in correctlyOrderedUpdates)
        {
            var middleIndex = (update.PageNumbers.Count - 1) / 2;
            middlePageNumbers.Add(update.PageNumbers.Skip(middleIndex).First());
        }

        return middlePageNumbers;
    }

    private static bool IsCorrectlyOrdered(Update update, List<OrderingRule> orderingRules)
    {
        for (int i = 0; i < update.PageNumbers.Count; i++)
        {
            var pageNumber = update.PageNumbers[i];
            var nextNumbers = update.PageNumbers.Skip(i + 1).Take(update.PageNumbers.Count - i);
            var pageNumbersThatShouldBePrintedBeforePageNumber = orderingRules.Where(x => x.SecondPageNumber.Equals(pageNumber)).Select(x => x.FirstPageNumber).ToList();

            if (nextNumbers.Any(x => pageNumbersThatShouldBePrintedBeforePageNumber.Any(y => y == x)))
            {
                return false;
            }
        }

        return true;
    }
}
