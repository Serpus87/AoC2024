using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day7;

public static class Part2
{
    public static long Solve(Input input)
    {
        // declare operators
        var operators = new List<Operator> { Operator.Add, Operator.Multiply, Operator.Concatenate };

        // get solved equations
        var solvedEquations = EquationService.GetSolvedEquationsWithMoreThanTwoOperators(input.Equations, operators);

        // sum testvalues

        long result = solvedEquations.Sum(x => x.TestValue);
        return result;
    }
}
