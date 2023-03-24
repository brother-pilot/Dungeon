using System.Collections.Generic;
using System.Drawing;

namespace Dungeon
{
    public class BfsTask
    {
        public static IEnumerable<SinglyLinkedList<Point>> FindPaths(Map map, Point start, Point[] chests)
        {
            //var track = new Dictionary<Point, Point>();//в словаре хранится из какой вершины в какую мы пришли

            ////track[start] = null;
            //var queue = new Queue<Point>();
            //queue.Enqueue(start);
            //while (queue.Count != 0)
            //{
            //    var point = queue.Dequeue();
            //    foreach (var nextPoint in node.IncidentNodes)
            //    {
            //        if (track.ContainsKey(nextNode)) continue; //смотрим посещали ли мы эту вершину

            //        track[nextNode] = node;//в nextnode зашли из текущей
            //        queue.Enqueue(nextNode);
            //    }
            //    if (track.ContainsKey(end)) break; //если нашли конечную вершину то заканчиваем обход
            //}
            //var pathItem = end;
            //var result = new List<Node>();
            //while (pathItem != null) //складываем с конца эти вершины

            //{
            //    result.Add(pathItem);
            //    pathItem = track[pathItem];
            //}
            //result.Reverse();
            //return result;




            SinglyLinkedList<Point> current = null;
            var queue = new Queue<SinglyLinkedList<Point>> ();   
            var visited = new SinglyLinkedList<Point>(start);
            queue.Enqueue(start);
            while (queue.Count != 0)//в очередь постепенно кладутся точки которые мы будем использовать на следующем шаге
            {
                var point = queue.Dequeue();
                //while (visited.Value != point)
                //{
                //    visited = visited.Previous;
                //}
                if (map.Dungeon[point.X, point.Y] == MapCell.Wall) continue;
                for (int i = 0; i < chests.Length; i++)
                {
                    if (point.Equals(chests[i])) yield return CorrectPath(current,chests[i]);
                }
                current = new SinglyLinkedList<Point>(point, current);
                visited.Add(current.Value);

                for (var dy = -1; dy <= 1; dy++)
                    for (var dx = -1; dx <= 1; dx++)
                        if (dx != 0 && dy != 0) continue;
                        else if (!visited.Contains(new Point { X = point.X + dx, Y = point.Y + dy })|| !map.InBounds(new Point { X = point.X + dx, Y = point.Y + dy }))
                            queue.Enqueue(new Point { X = point.X + dx, Y = point.Y + dy });

            }
            yield break;
        }

        public static IEnumerable<SinglyLinkedList<Point>> CorrectPath(SinglyLinkedList<Point> path, Point end)
        {
            var result = new SinglyLinkedList<Point>(end);

            return;
        }


    }

    public class Node
    {
        public readonly List<Node> IncidentNodes = new List<Node>(); //прописываем связь на соседние узлы
    }

}