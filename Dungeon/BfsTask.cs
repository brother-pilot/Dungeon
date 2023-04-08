using System.Collections.Generic;
using System.Drawing;


namespace Dungeon
{
    public class BfsTask
    {
        public static IEnumerable<SinglyLinkedList<Point>> FindPaths(Map map, Point start, Point[] chests)
        {
            //var list = new List<SinglyLinkedList<Point>>();
            SinglyLinkedList<Point> current = null;
            var queue = new Queue<SinglyLinkedList<Point>>();//помещаем в очередь не только точку но и путь по которому сюда пришли
            var visited = new HashSet<Point>();//помечаем точки в которых уже искали возможные пути
            var queueVisited = new HashSet<Point>();//отбрасываем лишние пути в эту точку если эту точку уже помещали в стэк
            queueVisited.Add(start);
            //var sourcedPath = new HashSet<Point>();
            queue.Enqueue(new SinglyLinkedList<Point>(start));
            while (queue.Count != 0)//в очередь постепенно кладутся точки которые мы будем использовать на следующем шаге
            {
                var point = queue.Dequeue();
                if (map.Dungeon[point.Value.X, point.Value.Y] == MapCell.Wall) continue;
                for (int i = 0; i < chests.Length; i++)
                {
                    //if (point.Value.Equals(chests[i]) && !sourcedPath.Contains(chests[i]))
                    if (point.Value.Equals(chests[i]))
                    {
                        //sourcedPath.Add(point.Value);
                        //list.Add(point);
                        yield return point;
                    }
                }
                current = new SinglyLinkedList<Point>(point.Value, current);
                visited.Add(current.Value);
                for (var dy = -1; dy <= 1; dy++)
                    for (var dx = -1; dx <= 1; dx++)
                        if (dx != 0 && dy != 0) continue;
                        else
                        {
                            var newPoint = new Point { X = point.Value.X + dx, Y = point.Value.Y + dy };
                            if (map.InBounds(newPoint) && !queueVisited.Contains(newPoint) && !visited.Contains(newPoint))
                            {
                                queueVisited.Add(newPoint);
                                queue.Enqueue(new SinglyLinkedList<Point>(newPoint, point));
                            }
                        }
            }
            //if (list.Count > 0)
            //{
            //    for (int i = 0; i < chests.Length; i++) //выбираем самый короткий путь до сундука
            //    {
            //        var bestPice = double.PositiveInfinity;
            //        SinglyLinkedList<Point> best = null;
            //        foreach (var item in list)
            //        {
            //            if (item.Value.Equals(chests[i]))
            //                if (item.Length < bestPice)
            //                    best = item;
            //        }
            //        yield return best;
            //    }
            //}
            yield break;
        }
    }
}

//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Dungeon
//{
//    public class BfsTask
//    {
//        public static IEnumerable<SinglyLinkedList<Point>> FindPaths(Map map, Point start, Point[] chests)
//        {
//            var track = new Dictionary<Point, SinglyLinkedList<Point>>();
//            track[start] = new SinglyLinkedList<Point>(start);
//            var queue = new Queue<SinglyLinkedList<Point>>();
//            queue.Enqueue(track[start]);
//            foreach (var chest in chests)
//            {
//                if (track.ContainsKey(chest))
//                {
//                    yield return track[chest];
//                    continue;
//                }
//                var path = FindPath(track, queue, map, start, chest);
//                if (path != null)
//                    yield return path;
//            }
//        }
//        static SinglyLinkedList<Point> FindPath(Dictionary<Point, SinglyLinkedList<Point>> track, Queue<SinglyLinkedList<Point>> queue, Map map, Point start, Point end)
//        {
//            while (queue.Count != 0)
//            {
//                var node = queue.Dequeue();
//                var incidentNodes = Walker.PossibleDirections
//                    .Select(direction => node.Value + direction)
//                    .Where(point => map.InBounds(point) && map.Dungeon[point.X, point.Y] != MapCell.Wall);
//                foreach (var nextNode in incidentNodes)
//                {
//                    if (track.ContainsKey(nextNode)) continue;
//                    track[nextNode] = new SinglyLinkedList<Point>(nextNode, node);
//                    queue.Enqueue(track[nextNode]);
//                }
//                if (track.ContainsKey(end)) return track[end];
//            }
//            if (!track.ContainsKey(end)) return null;
//            throw new Exception("There should never be this exception");
//        }
//    }
//}