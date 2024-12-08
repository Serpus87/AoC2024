using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day6.Models;
using AdventOfCode.Day6.Services;

namespace AdventOfCode.Day6;

public static class Day6
{
    public static void Solve()
    {
        // read file
        var input = File.ReadAllLines($"Day6\\PuzzleInput.txt");

        var gameService = new GameService();
        var game = gameService.SetUpGame(input);
        PrintingService.Print(game.Map, game.Guard.Position);

        var solution = Part1.Solve(game, gameService);
        Console.WriteLine($"Day6 Part1 Solution: {solution}");

        solution = Part2.Solve(game, gameService);
        Console.WriteLine($"Day6 Part2 Solution: {solution}");
    }
}
