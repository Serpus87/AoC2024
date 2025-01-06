using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day19.Extensions;

namespace AdventOfCode.Day19.Models;

public class AlternativeDesignPatternToCheck
{
    public DesignPattern AlternativeDesignPattern { get; set; }
    public string NewDesignSubstring { get; set; }

    public AlternativeDesignPatternToCheck(DesignPattern alternativeDesignPattern, string newDesignSubString)
    {
        AlternativeDesignPattern = alternativeDesignPattern;
        NewDesignSubstring = newDesignSubString;
    }
}
