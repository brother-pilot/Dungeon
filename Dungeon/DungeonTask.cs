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
            

            var pathsToChests = BfsTask.FindPaths(map, map.InitialPosition, map.Chests)
                .Select(x => x.ToList())
                .ToArray();
            var pathsFromExitToChests = BfsTask.FindPaths(map, map.Exit, map.Chests)
                .Select(x => x.ToList())
                .ToArray();
            //if (pathsToChests.Length == 0|| pathsFromExitToChests.Length == 0) 
            //    return new MoveDirection[0];
            if (pathsToChests.Length == 0)
            {
                
                var pathsToExit = BfsTask.FindPaths(map, map.InitialPosition,
                        new Point[] { map.Exit })
                    .Select(x => x.ToList())
                    .ToArray();
                if (pathsToExit.Length == 0)
                return new MoveDirection[0];
                    int count = pathsToExit[0].Count();
                MoveDirection[] direction2 = new MoveDirection[count-1];
                for (int i = pathsToExit[0].Count - 1; i > 0; i--)
                {
                    int coordinateX = pathsToExit[0][i - 1].X - pathsToExit[0][i].X;
                    int coordinateY = pathsToExit[0][i - 1].Y - pathsToExit[0][i].Y;
                    direction2[pathsToExit[0].Count - 1 - i] = Walker.ConvertOffsetToDirection(new Size(coordinateX, coordinateY));
                    //dir[i, 0] = coordinateX;
                    //dir[i, 1] = coordinateY;
                }
                return direction2;
            }
            int min = 0;
            int minItem1 = 0;
            int minItem2 = 0;
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
            if (min==0) return new MoveDirection[0];
            MoveDirection[] direction=new MoveDirection[min-2];
            for (int i = pathsToChests[minItem1].Count-1; i > 0; i--)
            {
                int coordinateX = pathsToChests[minItem1][i-1].X - pathsToChests[minItem1][i].X;
                int coordinateY = pathsToChests[minItem1][i-1].Y - pathsToChests[minItem1][i].Y;
                direction[pathsToChests[minItem1].Count-1-i] = Walker.ConvertOffsetToDirection(new Size(coordinateX, coordinateY));
            }
            direction[pathsToChests[minItem1].Count - 1]= Walker.ConvertOffsetToDirection(new Size(
                pathsFromExitToChests[minItem2][1].X- pathsToChests[minItem1][0].X,
                pathsFromExitToChests[minItem2][1].Y - pathsToChests[minItem1][0].Y));
            for (int i = 0; i < pathsFromExitToChests[minItem2].Count - 1; i++)
            {
                int coordinateX = pathsFromExitToChests[minItem2][i+1].X - pathsFromExitToChests[minItem2][i].X;
                int coordinateY = pathsFromExitToChests[minItem2][i+1].Y - pathsFromExitToChests[minItem2][i].Y;
                direction[i+ pathsToChests[minItem2].Count-1] = Walker.ConvertOffsetToDirection(new Size(coordinateX, coordinateY));
            }
            return direction;
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
