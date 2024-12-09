﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using AdventOfCode.Day8.Models;


namespace AdventOfCode.Day8;

public static class Part2
{
    /// <summary>
    /// Your puzzle answer was .
    /// The first half of this puzzle is complete! It provides one gold star: *
    /// 
    /// --- Part Two ---
    /// Watching over your shoulder as you work, one of The Historians asks if you took the effects of resonant harmonics into your calculations.
    /// 
    /// Whoops!
    /// 
    /// After updating your model, it turns out that an antinode occurs at any grid position exactly in line with at least two antennas of the same frequency, regardless of distance.This means that some of the new antinodes will occur at the position of each antenna(unless that antenna is the only one of its frequency).
    /// 
    /// So, these three T-frequency antennas now create many antinodes:
    /// 
    /// T....#....
    /// ...T......
    /// .T....#...
    /// .........#
    /// ..#.......
    /// ..........
    /// ...#......
    /// ..........
    /// ....#.....
    /// ..........
    /// In fact, the three T-frequency antennas are all exactly in line with two antennas, so they are all also antinodes! This brings the total number of antinodes in the above example to 9.
    /// 
    /// The original example now has 34 antinodes, including the antinodes that appear on every antenna:
    /// 
    /// ##....#....#
    /// .#.#....0...
    /// ..#.#0....#.
    /// ..##...0....
    /// ....0....#..
    /// .#...#A....#
    /// ...#..#.....
    /// #....#.#....
    /// ..#.....A...
    /// ....#....A..
    /// .#........#.
    /// ...#......##
    /// Calculate the impact of the signal using this updated model. How many unique locations within the bounds of the map contain an antinode?
    /// 
    /// Answer: 
    /// 
    /// 
    /// 
    /// Although it hasn't changed, you can still get your puzzle input.
    /// 
    /// You can also[Share] this puzzle.
    /// </summary>

    public static int Solve(Map map, List<Antenna> antennas)
    {
        return 0;
    }
}
