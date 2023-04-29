using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

/*
 * Практика «Поиск в ширину»
 *
 * На карте расположено несколько сундуков. Для тех сундуков, до которых существует путь от точки start, необходимо найти путь от сундука до точки start в виде односвязного списка SinglyLinkedList.

Для этого в классе BfsTask нужно реализовать поиск в ширину с указанной сигнатурой. Кстати, он вам понадобится и для следующей задачи!

Проверить корректность своего решения можно запустив тесты в классе Bfs_Should. Там же, по тестам, можно уточнить постановку задачи на различных крайних случаях.

После корректного выполнения задания, можно будет запустить проект. Кликнув на пустую ячейку вы увидите найденный вашим алгоритмом путь.
 */
namespace Dungeon
{
    public class BfsTask
    {
        public static IEnumerable<SinglyLinkedList<Point>> FindPaths(Map map, Point start, Point[] chests)
        {
            Dictionary<Point, SinglyLinkedList<Point>> path = new Dictionary<Point, SinglyLinkedList<Point>>();
            SinglyLinkedList<Point> pathCurrentChest = null;
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

                if (pathCurrentChest != null) yield return pathCurrentChest;
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
    }
}
        