using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day7;

public static class EquationService
{
    public static List<Equation> GetSolvedEquations(List<Equation> equations, List<Operator> operators)
    {
        var solvedEquations = new List<Equation>();

        foreach (Equation equation in equations)
        {
            bool isSolvable = IsSolvable(equation, operators);

            if (isSolvable)
            {
                solvedEquations.Add(equation);
            }
        }

        return solvedEquations;
    }

    private static bool IsSolvable(Equation equation, List<Operator> operators)
    {
        var numberOfSplitEquations = equation.Numbers.Count - 1;

        //var operatorCombinations = GetOperatorCombinations(numberOfSplitEquations, operators);
        var numberOfOperatorCombinations = Math.Pow(operators.Count, numberOfSplitEquations);

        for ( var i = 0; i < numberOfOperatorCombinations; i++)
        {
            var binaryString = GetBinaryString(i, numberOfSplitEquations);
            Console.WriteLine(binaryString);
            var solution = equation.Numbers[0];
            for ( var j = 0; j < numberOfSplitEquations; j++)
            {
                var operatorToApply = operators[int.Parse(binaryString[j].ToString())];
                solution = ApplyOperator(solution, equation.Numbers[j + 1], operatorToApply);

                if (solution > equation.TestValue)
                {
                    break;
                }
            }

            if (solution == equation.TestValue)
            {
                return true;
            }
        }

        return false; 
    }

    private static long ApplyOperator(long firstNumber, long secondNumber, Operator operatorEnum)
    {
        return operatorEnum switch
        {
            Operator.Add => firstNumber + secondNumber,
            Operator.Multiply => firstNumber * secondNumber,
            Operator.Concatenate => long.Parse(firstNumber.ToString()+secondNumber.ToString()),
        };
    }

    //private static List<List<Operator>> GetOperatorCombinations(int numberOfSplitEquations, List<Operator> operators)
    //{
    //    var binaryStrings = GetBinaryStrings(operators.Count, numberOfSplitEquations);

    //    var numberOfOperatorCombinations = Math.Pow(operators.Count, numberOfSplitEquations);
    //    var operatorCounter = 0;
    //    List<List<Operator>> operatorLists = new List<List<Operator>>();
    //    var operatorList = new List<Operator>();

    //    for (int i = 0; i < numberOfOperatorCombinations; i++)
    //    {
    //        operatorCounter = i % numberOfSplitEquations;

    //        operatorList.Add()

    //        if (operatorCounter == 0)
    //        {
    //            operatorLists.Add(operatorList);
    //            operatorList = new List<Operator>();
    //        }
    //    }
    //}

    private static string GetBinaryString(int number, int numberOfSplitEquations)
    {
        var binarystring = Convert.ToString(number, 2);
        var binaryStringLength = binarystring.Length;
        var remainingLength = numberOfSplitEquations - binaryStringLength;
        var stringStart = new string('0', remainingLength);

        return stringStart + binarystring;
    }

    internal static List<Equation> GetSolvedEquationsWithMoreThanTwoOperators(List<Equation> equations, List<Operator> operators)
    {
        var solvedEquations = new List<Equation>();

        foreach (Equation equation in equations)
        {
            bool isSolvable = IsSolvableWithMoreThanTwoOperators(equation, operators);

            if (isSolvable)
            {
                solvedEquations.Add(equation);
            }
        }

        return solvedEquations;
    }

    private static bool IsSolvableWithMoreThanTwoOperators(Equation equation, List<Operator> operators)
    {
        var numberOfSplitEquations = equation.Numbers.Count - 1;

        var operatorCombinations = GetOperatorCombinations(numberOfSplitEquations, operators);

        foreach (var operatorCombination in operatorCombinations)
        {
            Console.WriteLine(operatorCombination);
            var solution = equation.Numbers[0];
            for (var j = 0; j < numberOfSplitEquations; j++)
            {
                var operatorToApply = operators[int.Parse(operatorCombination[j].ToString())];
                solution = ApplyOperator(solution, equation.Numbers[j + 1], operatorToApply);

                if (solution > equation.TestValue)
                {
                    break;
                }
            }

            if (solution == equation.TestValue)
            {
                return true;
            }
        }

        return false;
    }

    private static List<string> GetOperatorCombinations(int numberOfSplitEquations, List<Operator> operators)
    {
        var operatorCombinations = new List<string>();

        var numberOfOperatorCombinations = Math.Pow(operators.Count, numberOfSplitEquations);

        var length = numberOfSplitEquations;
        var maxNumber = operators.Count - 1;
        var possibleNumbers = maxNumber + 1;
        var possibleCombinations = Math.Pow(possibleNumbers, length);
        var combinationCounter = 0;

        var array = new int[length];
        Console.WriteLine(string.Join("", array));
        operatorCombinations.Add(string.Join("", array));

        var index = 0;
        while (combinationCounter < possibleCombinations)
        {
            for (int number = 0; number <= maxNumber; number++)
            {

                if (array[index] < maxNumber)
                {
                    array[index]++;
                    combinationCounter++;

                    Console.WriteLine(string.Join("", array));
                    operatorCombinations.Add(string.Join("", array));
                    continue;
                }
                else
                {
                    for (int nextIndexToIncrease = (index + 1); nextIndexToIncrease < length; nextIndexToIncrease++)
                    {
                        if (array[nextIndexToIncrease] == maxNumber)
                        {
                            continue;
                        }
                        else
                        {
                            array[nextIndexToIncrease]++;
                            for (int indexToCorrect = 0; indexToCorrect < nextIndexToIncrease; indexToCorrect++)
                            {
                                array[indexToCorrect] = 0;
                            }
                            break;
                        }
                    }

                    index = -1;
                    combinationCounter++;

                    if (combinationCounter >= possibleCombinations)
                    {
                        break;
                    }

                    Console.WriteLine(string.Join("", array));
                    operatorCombinations.Add(string.Join("", array));
                    break;
                }
            }
            index++;
        }

        return operatorCombinations;
    }
}
