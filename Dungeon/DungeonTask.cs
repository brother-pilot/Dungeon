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
                int count = pathsToExit[0].Count();
                MoveDirection[] directionToExit = new MoveDirection[count - 1];
                ConvertPathToDirection(ref directionToExit, 0, pathsToExit);
                return directionToExit;
            }
            var pathsFromExitToChests = FindPath(map, map.Exit, map.Chests);
            int min = 0;
            int minItem1 = 0;
            int minItem2 = 0;
            FindMinSteps(ref min, ref minItem1, ref minItem2, pathsToChests, pathsFromExitToChests);
            if (min == 0) return new MoveDirection[0];
            MoveDirection[] direction = new MoveDirection[min - 2];
            //int[,] dir = new int[min - 2, 2];
            //pathsToChests.Zip()
            ConvertPathToDirection(ref direction, minItem1, pathsToChests);
            ConvertPathToDirectionInvers(ref direction, minItem1, minItem2, pathsToChests, pathsFromExitToChests);
            return direction;
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
        }

        private static void ConvertPathToDirection(ref MoveDirection[] direction, int minItem1,
            List<Point>[] pathsToChests)
        {
            for (int i = pathsToChests[minItem1].Count - 1; i > 0; i--)
            {
                int coordinateX = pathsToChests[minItem1][i - 1].X - pathsToChests[minItem1][i].X;
                int coordinateY = pathsToChests[minItem1][i - 1].Y - pathsToChests[minItem1][i].Y;
                direction[pathsToChests[minItem1].Count - 1 - i] =
                    Walker.ConvertOffsetToDirection(new Size(coordinateX, coordinateY));
            }
        }

        private static void ConvertPathToDirectionInvers(ref MoveDirection[] direction, int minItem1, int minItem2,
            List<Point>[] pathsToChests, List<Point>[] pathsFromExitToChests)
        {
            ConvertPathToDirection(ref direction, minItem1, pathsToChests);
            direction[pathsToChests[minItem1].Count - 1] = Walker.ConvertOffsetToDirection(new Size(
                pathsFromExitToChests[minItem2][1].X - pathsToChests[minItem1][0].X,
                pathsFromExitToChests[minItem2][1].Y - pathsToChests[minItem1][0].Y));
            for (int i = 0; i < pathsFromExitToChests[minItem2].Count - 1; i++)
            {
                int coordinateX = pathsFromExitToChests[minItem2][i + 1].X - pathsFromExitToChests[minItem2][i].X;
                int coordinateY = pathsFromExitToChests[minItem2][i + 1].Y - pathsFromExitToChests[minItem2][i].Y;
                direction[i + pathsToChests[minItem1].Count - 1] = Walker.ConvertOffsetToDirection(new Size(coordinateX, coordinateY));
            }
        }

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
