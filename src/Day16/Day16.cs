using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day16;

public static class Day16
{
    public static void Solve()
    {
        // read file
        var fileName = "PuzzleInput.txt";
        var input = File.ReadAllLines($"Day16\\{fileName}");
        var maze = MazeService.GetMaze(input);

        var solution = Part1.Solve(maze);
        Console.WriteLine($"Day16 Part1 Solution: {solution}");

        // todo refactor this sillyness
        var newMaze = MazeService.GetMaze(input);
        solution = Part2.Solve(newMaze);
        Console.WriteLine($"Day16 Part2 Solution: {solution}");
    }
}
