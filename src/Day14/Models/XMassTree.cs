using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day14.Models;

public class XMassTree
{
    public TreeTrunk TreeTrunk { get; set; }
    public List<TreeBranch> TreeBranches { get; set; }

    public XMassTree(TreeTrunk treeTrunk, List<TreeBranch> treeBranches)
    {
        TreeTrunk = treeTrunk;
        TreeBranches = treeBranches;
    }
}
