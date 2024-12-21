using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day14.Models;

namespace AdventOfCode.Day14;

public class MapService
{
    public static List<Robot> GetRobotsFromFile(string[] input)
    {
        var robots = new List<Robot>();

        foreach (var line in input)
        {
            var robotText = line.Split(' ');
            var positionXIndex = robotText[0].IndexOf('=') + 1;
            var positionXLength = robotText[0].IndexOf(',') - positionXIndex;
            var positionYIndex = robotText[0].IndexOf(',') + 1;
            var positionYLength = robotText[0].Length - positionYIndex;

            var velocityXIndex = robotText[1].IndexOf('=') + 1;
            var velocityXLength = robotText[1].IndexOf(',') - velocityXIndex;
            var velocityYIndex = robotText[1].IndexOf(',') + 1;
            var velocityYLength = robotText[1].Length - velocityYIndex;

            var positionX = int.Parse(robotText[0].Substring(positionXIndex, positionXLength));
            var positionY = int.Parse(robotText[0].Substring(positionYIndex, positionYLength));
            var velocityX = int.Parse(robotText[1].Substring(velocityXIndex, velocityXLength));
            var velocityY = int.Parse(robotText[1].Substring(velocityYIndex, velocityYLength));

            robots.Add( new Robot(new Location(positionY, positionX), new Location(velocityY,velocityX)));
        }

        return robots;
    }

    public static Map SetupMap(int mapNRows, int mapNColumns, List<Robot> robots)
    {
        // create map
        var map = new Map(mapNRows, mapNColumns);

        // create empty map
        map.CreateEmpty();

        // define quadrants
        map.DefineQuadrants();

        // place robots on map
        map.PlaceRobots(robots);

        return map;
    }


    public static void MoveRobots(Map map, List<Robot> robots, int numberOfTimesToMove)
    {
        for (int i = 0; i < numberOfTimesToMove; i++) 
        {
            //map.Print();
            foreach (Robot robot in robots) 
            {
                var currentLocation = robot.Position;
                var nextLocation = map.GetNextLocation(robot);
                robot.Position = nextLocation;
                map.MoveRobot(currentLocation, nextLocation);
            }
        }
        //map.Print();
    }
}
