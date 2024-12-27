using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day14.Models;
using AdventOfCode.Day15.Models;
using AdventOfCode.Day4;
using AdventOfCode.Day6.Models;
using Microsoft.VisualBasic;
using AdventOfCode.Day15.Extensions;
namespace AdventOfCode.Day15;

public static class Part2
{
    /// <summary>
    /// Your puzzle answer was .
    ///     The first half of this puzzle is complete! It provides one gold star: *
    /// 
    /// --- Part Two ---
    /// The lanternfish use your information to find a safe moment to swim in and turn off the malfunctioning robot! Just as they start preparing a festival in your honor, reports start coming in that a second warehouse's robot is also malfunctioning.
    /// 
    /// This warehouse's layout is surprisingly similar to the one you just helped. There is one key difference: everything except the robot is twice as wide! The robot's list of movements doesn't change.
    /// 
    /// To get the wider warehouse's map, start with your original map and, for each tile, make the following changes:
    /// 
    /// If the tile is #, the new map contains ## instead.
    /// If the tile is O, the new map contains[] instead.
    /// If the tile is ., the new map contains..instead.
    /// If the tile is @, the new map contains @. instead.
    /// This will produce a new warehouse map which is twice as wide and with wide boxes that are represented by[]. (The robot does not change size.)
    /// 
    /// The larger example from before would now look like this:
    /// 
    /// ####################
    /// ##....[]....[]..[]##
    /// ##............[]..##
    /// ##..[][]....[]..[]##
    /// ##....[]@.....[]..##
    /// ##[]##....[]......##
    /// ##[]....[]....[]..##
    /// ##..[][]..[]..[][]##
    /// ##........[]......##
    /// ####################
    /// Because boxes are now twice as wide but the robot is still the same size and speed, boxes can be aligned such that they directly push two other boxes at once. For example, consider this situation:
    /// 
    /// #######
    /// #...#.#
    /// #.....#
    /// #..OO@#
    /// #..O..#
    /// #.....#
    /// #######
    /// 
    /// <vv<<^^<<^^
    /// After appropriately resizing this map, the robot would push around these boxes as follows:
    /// 
    /// Initial state:
    /// ##############
    /// ##......##..##
    /// ##..........##
    /// ##....[][]@.##
    /// ##....[]....##
    /// ##..........##
    /// ##############
    /// 
    /// Move<:
    /// ##############
    /// ##......##..##
    /// ##..........##
    /// ##...[][]@..##
    /// ##....[]....##
    /// ##..........##
    /// ##############
    /// 
    /// Move v:
    /// ##############
    /// ##......##..##
    /// ##..........##
    /// ##...[][]...##
    /// ##....[].@..##
    /// ##..........##
    /// ##############
    /// 
    /// Move v:
    /// ##############
    /// ##......##..##
    /// ##..........##
    /// ##...[][]...##
    /// ##....[]....##
    /// ##.......@..##
    /// ##############
    /// 
    /// Move <:
    /// ##############
    /// ##......##..##
    /// ##..........##
    /// ##...[][]...##
    /// ##....[]....##
    /// ##......@...##
    /// ##############
    /// 
    /// Move<:
    /// ##############
    /// ##......##..##
    /// ##..........##
    /// ##...[][]...##
    /// ##....[]....##
    /// ##.....@....##
    /// ##############
    /// 
    /// Move ^:
    /// ##############
    /// ##......##..##
    /// ##...[][]...##
    /// ##....[]....##
    /// ##.....@....##
    /// ##..........##
    /// ##############
    /// 
    /// Move ^:
    /// ##############
    /// ##......##..##
    /// ##...[][]...##
    /// ##....[]....##
    /// ##.....@....##
    /// ##..........##
    /// ##############
    /// 
    /// Move <:
    /// ##############
    /// ##......##..##
    /// ##...[][]...##
    /// ##....[]....##
    /// ##....@.....##
    /// ##..........##
    /// ##############
    /// 
    /// Move<:
    /// ##############
    /// ##......##..##
    /// ##...[][]...##
    /// ##....[]....##
    /// ##...@......##
    /// ##..........##
    /// ##############
    /// 
    /// Move ^:
    /// ##############
    /// ##......##..##
    /// ##...[][]...##
    /// ##...@[]....##
    /// ##..........##
    /// ##..........##
    /// ##############
    /// 
    /// Move ^:
    /// ##############
    /// ##...[].##..##
    /// ##...@.[]...##
    /// ##....[]....##
    /// ##..........##
    /// ##..........##
    /// ##############
    /// This warehouse also uses GPS to locate the boxes.For these larger boxes, distances are measured from the edge of the map to the closest edge of the box in question.So, the box shown below has a distance of 1 from the top edge of the map and 5 from the left edge of the map, resulting in a GPS coordinate of 100 * 1 + 5 = 105.
    /// 
    /// ##########
    /// ##...[]...
    /// ##........
    /// In the scaled-up version of the larger example from above, after the robot has finished all of its moves, the warehouse would look like this:
    /// 
    /// ####################
    /// ##[].......[].[][]##
    /// ##[]...........[].##
    /// ##[]........[][][]##
    /// ##[]......[]....[]##
    /// ##..##......[]....##
    /// ##..[]............##
    /// ##..@......[].[][]##
    /// ##......[][]..[]..##
    /// ####################
    /// The sum of these boxes' GPS coordinates is 9021.
    /// 
    /// Predict the motion of the robot and boxes in this new, scaled-up warehouse.What is the sum of all boxes' final GPS coordinates?
    /// 
    /// Answer: 
    /// 
    /// 
    /// 
    /// Although it hasn't changed, you can still get your puzzle input.
    /// 
    /// You can also[Share] this puzzle.
    /// </summary>

    public static int Solve(WideWarehouse warehouse)
    {
        // move robot
        WarehouseService.MakeAllRobotMovesInWideWarehouseKISS(warehouse);

        // get GPS
        var result = warehouse.WideBoxes.GetGPSSum(warehouse.Map);

        // first try wrong answer: 1505033 too low
        // second try wrong answer: 1521738 too high
        // third try wrong answer: 1509823 too high
        // fourth try: 1509724!
        return result;
    }
}
