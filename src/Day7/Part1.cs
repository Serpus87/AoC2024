using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode.Day7;

public static class Part1
{
    public static long Solve(Input input)
    {
        // declare operators
        var operators = new List<Operator> { Operator.Add, Operator.Multiply };

        // get solved equations
        var solvedEquations = EquationService.GetSolvedEquations(input.Equations, operators);

        // sum testvalues

        long result = solvedEquations.Sum(x=>x.TestValue);
        return result;
    }
}
