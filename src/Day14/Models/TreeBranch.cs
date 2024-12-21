using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day14.Models;

public class TreeBranch
{
    public List<Robot> Branch { get; set; }

    public TreeBranch(List<Robot> branch)
    {
        Branch = branch;
    }
}
