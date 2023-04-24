using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;


namespace Dungeon
{
    public class BfsTask
    {
        public static IEnumerable<SinglyLinkedList<Point>> FindPaths(Map map, Point start, Point[] chests)
        {
            Dictionary<Point, SinglyLinkedList<Point>> path = new Dictionary<Point, SinglyLinkedList<Point>>();
            SinglyLinkedList<Point> pathCurrentChest=null;
            for (int i = 0; i < chests.Length; i++) //пробегаемся по сундукам
            {
                if (path.ContainsKey(chests[i])) //проверяем не попадался ли нам ранее такой сундук
                {
                    yield return path[chests[i]];
                    continue;
                }
                else
                {
                    pathCurrentChest = FindPath(map, start, chests[i]);
                    path.Add(chests[i], pathCurrentChest);
                }
                if (pathCurrentChest!=null) yield return pathCurrentChest;
            }
            yield break;
        }


        public static SinglyLinkedList<Point> FindPath(Map map, Point start, Point chests)
        {
            var direction = new int[,] { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } };
            SinglyLinkedList<Point> current = null;
            var queue =
                new Queue<SinglyLinkedList<Point>>(); //помещаем в очередь не только точку но и путь по которому сюда пришли
            var queueVisited =
                new HashSet<Point>(); //отбрасываем лишние пути в эту точку если эту точку уже помещали в стэк
            queueVisited.Add(start);
            //var sourcedPath = new HashSet<Point>();
            queue.Enqueue(new SinglyLinkedList<Point>(start));

            while
                (queue.Count != 0) //в очередь постепенно кладутся точки которые мы будем использовать на следующем шаге
            {
                var point = queue.Dequeue();
                if (map.Dungeon[point.Value.X, point.Value.Y] == MapCell.Wall) continue;
                if (point.Value.Equals(chests))
                {
                    return point;
                }

                current = new SinglyLinkedList<Point>(point.Value, current);
                //for (var dy = -1; dy <= 1; dy++)
                //    for (var dx = -1; dx <= 1; dx++)
                //        if (dx != 0 && dy != 0) continue;
                //        else
                for (int j = 0; j <= 3; j++)
                {
                    var newPoint = new Point
                    { X = point.Value.X + direction[j, 0], Y = point.Value.Y + +direction[j, 1] };
                    if (map.InBounds(newPoint) && !queueVisited.Contains(newPoint))
                    {
                        queueVisited.Add(newPoint);
                        queue.Enqueue(new SinglyLinkedList<Point>(newPoint, point));
                    }
                }
            }
            return null;
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

        //public class BfsTask
        //{
        //    public static IEnumerable<SinglyLinkedList<Point>> FindPaths(Map map, Point start, Point[] chests)
        //    {
        //        //public static IEnumerable<Node> BreadthSearch(this Node startNode)//сделали расширением у класса Node
        //        {
        //            // Внимание! Перед использованием этого кода, прочитайте следующий слайд «Использование памяти». Это обход в ширину
        //            var visited = new HashSet<Point>();//посещенные вершины
        //            //var queue = new Queue<Point>();
        //            var queue = new Queue<SinglyLinkedList<Point>>();
        //            //queue.Enqueue(start);
        //            queue.Enqueue(new SinglyLinkedList<Point>(start));
        //            while (queue.Count != 0)
        //            {
        //                var point = queue.Dequeue();
        //                if (map.Dungeon[point.Value.X, point.Value.Y] == MapCell.Wall) continue;
        //                if (visited.Contains(node)) continue; //если мы уже были в этой вершине, то повторно ее не рассматриваем
        //                visited.Add(node);//добавляем в список просмотренных
        //                yield return node;
        //                foreach (var incidentNode in node.IncidentNodes)
        //                    queue.Enqueue(incidentNode);
        //            }
        //        }

        //    }
        //}
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
//	public class BfsTask
//	{
//		public static IEnumerable<SinglyLinkedList<Point>> FindPaths(Map map, Point start, Point[] chests)
//		{
//			var track = new Dictionary<Point, SinglyLinkedList<Point>>();
//			track[start] = new SinglyLinkedList<Point>(start);
//			var queue = new Queue<SinglyLinkedList<Point>>();
//			queue.Enqueue(track[start]);
//			foreach (var chest in chests)
//			{
//				if (track.ContainsKey(chest))
//				{
//					yield return track[chest];
//					continue;
//				}
//				var path = FindPath(track, queue, map, start, chest);
//				if (path != null)
//					yield return path;
//			}
//		}
//		static SinglyLinkedList<Point> FindPath(Dictionary<Point, SinglyLinkedList<Point>> track, Queue<SinglyLinkedList<Point>> queue, Map map, Point start, Point end)
//		{
//			while (queue.Count != 0)
//			{
//				var node = queue.Dequeue();
//				var incidentNodes = Walker.PossibleDirections
//					.Select(direction => node.Value + direction)
//					.Where(point => map.InBounds(point) && map.Dungeon[point.X, point.Y] != MapCell.Wall);
//				foreach (var nextNode in incidentNodes)
//				{
//					if (track.ContainsKey(nextNode)) continue;
//					track[nextNode] = new SinglyLinkedList<Point>(nextNode, node);
//					queue.Enqueue(track[nextNode]);
//				}
//				if (track.ContainsKey(end)) return track[end];
//			}
//			if (!track.ContainsKey(end)) return null;
//			throw new Exception("There should never be this exception");
//		}
//	}
//}

