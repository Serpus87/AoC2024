﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day6.Models;

namespace AdventOfCode.Day6.Services;

public class GameService
{
    public Game SetUpGame(string[] input)
    {
        var nRows = input.Length;
        var nColumns = input[0].Length;

        var guardPosition = new Position(-1, -1);
        var map = new Map(nRows, nColumns);

        for (var row = 0; row < nRows; row++)
        {
            for (var column = 0; column < nColumns; column++)
            {
                if (input[row][column] == '^')
                {
                    guardPosition = new Position(row, column);
                }
                map.Fields[row, column] = new Field(new Position(row, column), input[row][column]);
            }
        }

        var guard = new Guard(guardPosition);

        return new Game(map, guard);
    }

    // todo improve this
    internal void Play(Game game)
    {
        var guardIsOnMap = true;
        while (guardIsOnMap)
        {
            // can Guard move?
            var nextPosition = GetNextPosition(game.Guard);

            var isNextPositionObstruction = false;
            var isNextPositionOnMap = nextPosition.Row >= 0 && nextPosition.Row < game.Map.NRows && nextPosition.Column >= 0 && nextPosition.Column < game.Map.NColumns;

            if (isNextPositionOnMap)
            {
                isNextPositionObstruction = game.Map.Fields[nextPosition.Row, nextPosition.Column].Fill == '#';
            }

            // yes -> move && update board
            if (!isNextPositionObstruction && isNextPositionOnMap)
            {
                game.Map.Fields[game.Guard.Position.Row, game.Guard.Position.Column].Fill = 'X';

                if (!(game.Map.Fields[nextPosition.Row, nextPosition.Column].Fill == 'X'))
                {
                    game.Guard.MoveCounter++;
                }

                game.Map.Fields[nextPosition.Row, nextPosition.Column].Fill = '^';

                game.Guard.Position = nextPosition;
            }

            if (!isNextPositionOnMap)
            {
                game.Map.Fields[game.Guard.Position.Row, game.Guard.Position.Column].Fill = 'X';
            }

            if (isNextPositionObstruction)
            {
                game.Guard.TurnRight90Degrees();
            }

            // no -> turn

            // print board
            //PrintingService.Print(game.Map, game.Guard.Position);
            //guardIsOnMap = isNextPositionOnMap;
            guardIsOnMap = isNextPositionOnMap;
        }
    }

    private static Position GetNextPosition(Guard guard)
    {
        return guard.MoveDirection switch
        {
            Enums.MoveDirection.Up => new Position(guard.Position.Row - 1, guard.Position.Column),
            Enums.MoveDirection.Down => new Position(guard.Position.Row + 1, guard.Position.Column),
            Enums.MoveDirection.Left => new Position(guard.Position.Row, guard.Position.Column - 1),
            Enums.MoveDirection.Right => new Position(guard.Position.Row, guard.Position.Column + 1),
            _ => throw new ArgumentOutOfRangeException($"unknown {guard.MoveDirection}")
        };
    }
}
