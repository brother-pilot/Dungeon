using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon
{
    public class DungeonTask
    {
        public static MoveDirection[] FindShortestPath(Map map)
        {
            var pathsToChests = FindPath(map, map.InitialPosition, map.Chests);
            if (pathsToChests.Length == 0)
            {
                List<Point>[] pathsToExit = FindPath(map, map.InitialPosition, new Point[] { map.Exit });
                if (pathsToExit.Length == 0)
                    return new MoveDirection[0];
                return pathsToExit[0]
                .Zip(pathsToExit[0].Skip(1), (a, b) =>
                ConvertOffsetToDirection(new Point(a.X - b.X, a.Y - b.Y)))
                .Reverse()
                .ToArray();
            }
            List<Point>[] pathsFromExitToChests = FindPath(map, map.Exit, map.Chests);
            int min = 0;
            int minItem1 = 0;
            int minItem2 = 0;
            FindMinSteps(ref min, ref minItem1, ref minItem2, pathsToChests, pathsFromExitToChests);
            if (min == 0) return new MoveDirection[0];
            var firstPath = pathsToChests[minItem1]
                .Zip(pathsToChests[minItem1].Skip(1), (a, b) =>
                ConvertOffsetToDirection(new Point(a.X - b.X, a.Y - b.Y)))
                .Reverse()
                .ToArray();
            var secondPath = pathsFromExitToChests[minItem2]
                .Zip(pathsFromExitToChests[minItem2].Skip(1), (a, b) =>
                    ConvertOffsetToDirection(new Point(-a.X + b.X, -a.Y + b.Y)))
                .ToArray();
            return firstPath.Concat(secondPath).ToArray(); ;
        }

        private static List<Point>[] FindPath(Map map, Point start, Point[] chests)
        {
            return BfsTask.FindPaths(map, start, chests)
                .Select(x => x.ToList())
                .ToArray();
        }
        private static void FindMinSteps(ref int min, ref int minItem1, ref int minItem2,
            List<Point>[] pathsToChests, List<Point>[] pathsFromExitToChests)
        {
            
            for (int i = 0; i < pathsToChests.Length; i++)
            {
                for (int j = 0; j < pathsFromExitToChests.Length; j++)
                {
                    if (pathsToChests[i][0] == pathsFromExitToChests[j][0])
                    {
                        if (min == 0)
                        {
                            min = pathsToChests[i].Count + pathsFromExitToChests[j].Count;
                            minItem1 = i;
                            minItem2 = j;
                        }
                        int current = pathsToChests[i].Count + pathsFromExitToChests[j].Count;
                        if (current < min)
                        {
                            min = current;
                            minItem1 = i;
                            minItem2 = j;
                        }
                    }
                }
            }
            //var enrollments = from pathF in pathsToChests
            //    from pathS in pathsFromExitToChests
            //    where (pathF[0] == pathS[0])
            //    select ((pathF.Count() + pathS.Count()).Min());
        }
        
        public static MoveDirection ConvertOffsetToDirection(Point offset)
        {
            return offsetToDirection[offset];
        }

        private static readonly Dictionary<Point, MoveDirection> offsetToDirection = new Dictionary<Point, MoveDirection>
        {
            {new Point(0, -1), MoveDirection.Up},
            {new Point(0, 1), MoveDirection.Down},
            {new Point(-1, 0), MoveDirection.Left},
            {new Point(1, 0), MoveDirection.Right}
        };

    }
}

//direction[0] = MoveDirection.Down;
//direction[1] = MoveDirection.Down;
//direction[2] = MoveDirection.Right;
//direction[3] = MoveDirection.Right;
//direction[4] = MoveDirection.Right;
//direction[5] = MoveDirection.Right;
//direction[6] = MoveDirection.Down;
//direction[7] = MoveDirection.Down;
//direction[8] = MoveDirection.Left;
//direction[9] = MoveDirection.Left;