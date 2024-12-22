using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day14.Models;

namespace AdventOfCode.Day14;

public static class RobotExtensions
{
    public static bool HaveXMassTreeArrangement(this List<Robot> robots)
    {
        var robotsHaveXMassTreeArrangement = false;

        //var treeTrunks = robots.FindTreeTrunks();
        //var xMassTrees = treeTrunks.FindXMassTrees(robots);
        var bigBranches = robots.FindLargeRowOfSubsequentRobots();

        return bigBranches.Count > 0;
    }

    public static List<TreeBranch> FindLargeRowOfSubsequentRobots(this List<Robot> robots)
    {
        var bottomBranchLength = 15;
        var bottomBranches = new List<TreeBranch>();
        var robotRows = robots.Select(x => x.Position.Row).Distinct().ToList();

        foreach (var robotRow in robotRows)
        {
            var robotsInRow = robots.Where(x => x.Position.Row == robotRow).ToList();

            if (robotsInRow.Count < bottomBranchLength)
            {
                continue;
            }

            foreach (var robot in robotsInRow)
            {
                if (!robot.IsStartOfBranch(robots))
                {
                    continue;
                }
                if (robot.IsEndOfBranch(robots))
                {
                    continue;
                }

                var nextRobot = robot.GetNextPieceOfBranch(robotsInRow);
                var treeBranch = new TreeBranch(new List<Robot> { robot });
                //var treeBranchHasNoEnd = false;

                while (!nextRobot.IsEndOfBranch(robotsInRow))
                {
                    treeBranch.Branch.Add(nextRobot!);
                    nextRobot = nextRobot!.GetNextPieceOfBranch(robotsInRow);

                    //if (nextRobot == null)
                    //{
                    //    treeBranchHasNoEnd = true;
                    //}
                }

                var length = treeBranch.Branch.Count;
                if (length < bottomBranchLength)
                {
                    continue;
                }

                bottomBranches.Add(treeBranch);

                // -- for testing purpose break;
                break;
                // --
            }

            // -- for testing purpose, end early
            if (bottomBranches.Count > 0)
            {
                break;
            }
            // --
        }

        return bottomBranches;
    }


    public static List<TreeTrunk> FindTreeTrunks(this List<Robot> robots)
    {
        var treeTrunks = new List<TreeTrunk>();

        foreach (var robot in robots)
        {
            if (!robot.IsPeak(robots))
            {
                continue;
            }
            if (robot.IsBottom(robots))
            {
                continue;
            }

            var robotsInSameColumn = robots.Where(x => x.Position.Column == robot.Position.Column).ToList();

            var nextRobot = robot.GetNextPieceOfTrunk(robotsInSameColumn);
            var treeTrunk = new TreeTrunk(new List<Robot> { robot });
            var treeTrunkHasNoBottom = false;

            while (!(treeTrunkHasNoBottom || nextRobot!.IsBottom(robotsInSameColumn)))
            {
                treeTrunk.Trunk.Add(nextRobot!);
                nextRobot = nextRobot!.GetNextPieceOfTrunk(robotsInSameColumn);

                if (nextRobot == null)
                {
                    treeTrunkHasNoBottom = true;
                }
            }

            var length = treeTrunk.Trunk.Count;
            if (length < 5 || treeTrunkHasNoBottom)
            {
                continue;
            }

            treeTrunks.Add(treeTrunk);
        }

        return treeTrunks;
    }

    public static List<XMassTree> FindXMassTrees(this List<TreeTrunk> treeTrunks, List<Robot> robots)
    {
        var xMassTrees = new List<XMassTree>();

        foreach (var treeTrunk in treeTrunks)
        {
            var trunkLength = treeTrunk.Trunk.Count;
            var numberOfBranches = trunkLength - 3;
            var branchLength = 2;
            var branches = new List<TreeBranch>();
            var hasNoBranch = false;

            for (int i = 0; i < numberOfBranches; i++)
            {
                branchLength += (int)(Math.Floor((decimal)i / 2));

                var trunkPosition = 2 + i;
                var robotsInSameRow = robots.Where(x => x.Position.Row == treeTrunk.Trunk[trunkPosition].Position.Row).ToList();

                var branch = treeTrunk.FindBranch(trunkPosition, branchLength, robotsInSameRow);

                if (branch == null)
                {
                    hasNoBranch = true;
                    break;
                }

                branches.Add(branch);
            }

            if (hasNoBranch)
            {
                continue;
            }

            xMassTrees.Add(new XMassTree(treeTrunk, branches));
        }

        return xMassTrees;
    }


    public static TreeBranch FindBranch(this TreeTrunk treeTrunk, int trunkPosition, int branchLength, List<Robot> robotsInSameRow)
    {
        throw new NotImplementedException();
    }


    public static bool IsPeak(this Robot robot, List<Robot> robots)
    {
        return !robots.Any(x => x.Position.Column == robot.Position.Column && x.Position.Row == (robot.Position.Row - 1)
        || x.Position.Column == (robot.Position.Column - 1) && x.Position.Row == robot.Position.Row
        || x.Position.Column == (robot.Position.Column - 1) && x.Position.Row == robot.Position.Row);
    }

    public static bool IsBottom(this Robot robot, List<Robot> robots)
    {
        return !robots.Any(x => x.Position.Column == robot.Position.Column && x.Position.Row == (robot.Position.Row + 1)
        || x.Position.Column == (robot.Position.Column - 1) && x.Position.Row == robot.Position.Row
        || x.Position.Column == (robot.Position.Column - 1) && x.Position.Row == robot.Position.Row);
    }

    public static bool IsStartOfBranch(this Robot robot, List<Robot> robots)
    {
        return !robots.Any(x => x.Position.Column == (robot.Position.Column - 1) && x.Position.Row == robot.Position.Row);
    }

    public static bool IsEndOfBranch(this Robot robot, List<Robot> robots)
    {
        return !robots.Any(x => x.Position.Column == (robot.Position.Column + 1) && x.Position.Row == robot.Position.Row);
    }

    public static Robot? GetNextPieceOfTrunk(this Robot robot, List<Robot> robots)
    {
        return robots.FirstOrDefault(x => x.Position.Column == robot.Position.Column && x.Position.Row == (robot.Position.Row + 1));
    }

    public static Robot GetNextPieceOfBranch(this Robot robot, List<Robot> robots)
    {
        return robots.First(x => x.Position.Column == (robot.Position.Column + 1) && x.Position.Row == robot.Position.Row);
    }
}
