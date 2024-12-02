using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day2;

public static class ReportExtensions
{
    public static List<bool> GetSafeList(this List<Report> reports)
    {
        var safeList = new List<bool>();

        foreach (var report in reports)
        {
            safeList.Add(report.IsSafe());
        }

        return safeList;
    }

    public static bool IsSafe(this Report report)
    {
        return report.GetUnsafeLevel() == null;

        //var isIncreasing = true;
        //var isDecreasing = true;
        //var isWithinRange = true;
        //var isSafe = false;

        //for (int i = 1; i < report.Levels.Count; i++)
        //{
        //    var previousLevel = report.Levels[i - 1];
        //    var level = report.Levels[i];

        //    isIncreasing = isIncreasing && level > previousLevel;
        //    isDecreasing = isDecreasing && level < previousLevel;
        //    isWithinRange = Math.Abs(level - previousLevel) is >= 1 and <= 3;

        //    isSafe = (isIncreasing || isDecreasing) && isWithinRange;
        //    if (!isSafe)
        //    {
        //        break;
        //    }
        //}

        //return isSafe;
    }

    public static List<bool> GetSafeListWithDampner(this List<Report> reports)
    {
        var safeList = new List<bool>();

        foreach (var report in reports)
        {
            safeList.Add(report.IsSafeWithDampner());
        }

        return safeList;
    }

    public static bool IsSafeWithDampner(this Report report)
    {
        var reportCopy = report.Copy();
        var isSafe = false;
        var isSafeWithoutDampner = report.IsSafe();

        if (isSafeWithoutDampner)
        {
            return true;
        }

        var getFirstUnsafe = report.GetUnsafeLevel(true);
        report.Levels.RemoveAt((int)getFirstUnsafe!);

        isSafeWithoutDampner = report.IsSafe();

        if (isSafeWithoutDampner)
        {
            return true;
        }

        var getSecondUnsafe = reportCopy.GetUnsafeLevel();
        reportCopy.Levels.RemoveAt((int)getSecondUnsafe!);

        isSafeWithoutDampner = reportCopy.IsSafe();

        if (isSafeWithoutDampner)
        {
            return true;
        }

        return isSafe;
    }

    private static Report Copy(this Report originalReport)
    {
        var copyLevels = new List<int>();

        foreach (var level in originalReport.Levels)
        {
            copyLevels.Add(level);
        }

        return new Report(copyLevels);
    }

    public static int? GetUnsafeLevel(this Report report, bool usePreviousLevel = false)
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
            isWithinRange = Math.Abs(level - previousLevel) is >= 1 and <= 3;

            isSafe = (isIncreasing || isDecreasing) && isWithinRange;
            if (!isSafe)
            {
                return usePreviousLevel ? i - 1 : i;
            }
        }

        return null;
    }
}
