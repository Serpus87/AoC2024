using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day5;

public class OrderingRule
{
    public int FirstPageNumber { get; set; }
    public int SecondPageNumber { get; set; }

    public OrderingRule(int firstPageNumber, int secondPageNumber)
    {
        FirstPageNumber = firstPageNumber;
        SecondPageNumber = secondPageNumber;
    }
}
