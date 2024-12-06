using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day5;

public class Update
{
    public List<int> PageNumbers { get; set; }

    public Update(List<int> pageNumbers)
    {
        PageNumbers = pageNumbers;
    }
}
