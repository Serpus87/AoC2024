using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day2;

public static class ReportExtensions
{
    public static bool IsSafe(this Report report)
    {
        var isIncreasing = true;
        var isDecreasing = true;
        var isWithinRange = true;
        var isSafe = false;

        for (int i = 1; i < report.Levels.Count; i++)
        {
            var previousLevel = report.Levels[i - 1];
            var level = report.Levels[i];

            isIncreasing = isIncreasing && level > previousLevel;
            isDecreasing = isDecreasing && level < previousLevel;
            isWithinRange = Math.Abs(level - previousLevel) is >= 1 and <=3;

            isSafe = (isIncreasing || isDecreasing) && isWithinRange;
            if (!isSafe)
            {
                break;
            }
        }

        return isSafe;
    }

    public static List<bool> GetSafeList(this List<Report> reports)
    {
        var safeList = new List<bool>();

        foreach (var report in reports)
        {
            safeList.Add(report.IsSafe());
        }

        return safeList;
    }
}
