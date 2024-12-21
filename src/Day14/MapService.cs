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
            var positionX = int.Parse(line.Substring(line.IndexOf('p') + 2, line.IndexOf(',') - (line.IndexOf('p') + 2)));
            var positionY = int.Parse(line.Substring(line.IndexOf(','), line.IndexOf('v') - (line.IndexOf(',') - 2)));
            var velocityX = int.Parse(line.Substring(line.IndexOf('v') + 2, line.IndexOf(',', line.IndexOf('v') - (line.IndexOf('v') + 2))));
            var velocityY = int.Parse(line.Substring(line.IndexOf(',',line.IndexOf('v'))));

            robots.Add( new Robot(new Location(positionX, positionY), new Location(velocityX,velocityY)));
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
            map.Print();
            foreach (Robot robot in robots) 
            {
                var currentLocation = robot.Position;
                var nextLocation = map.GetNextLocation(robot);
                map.MoveRobot(currentLocation, nextLocation);
            }
        }
        map.Print();
    }
}
