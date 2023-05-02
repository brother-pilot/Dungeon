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
            var queue =
                new Queue<SinglyLinkedList<Point>>(); //помещаем в очередь не только точку но и путь по которому сюда пришли
            queue.Enqueue(new SinglyLinkedList<Point>(start));
            var queueVisited =
                new HashSet<Point>(); //отбрасываем лишние пути в эту точку если эту точку уже помещали в стэк
            queueVisited.Add(start);
            for (int i = 0; i < chests.Length; i++) //пробегаемся по сундукам
            {
                if (path.ContainsKey(chests[i])) //проверяем не попадался ли нам ранее такой сундук
                {
                    yield return path[chests[i]];
                    continue;
                }
                else
                {
                    while (pathCurrentChest == null || !pathCurrentChest.Value.Equals(chests[i]))
                    {
                        pathCurrentChest = FindPath(map, start, chests[i], chests, queue, queueVisited, path);
                        if (pathCurrentChest != null) 
                        {
                            if (!path.ContainsKey(chests[i]))//смотрим есть ли возращаемый путь до сундука в списке путей
                                path.Add(pathCurrentChest.Value, pathCurrentChest);
                        }
                        else break;
                    }
                    if (pathCurrentChest != null) yield return pathCurrentChest;
                }
            }

            yield break;
        }

        public static SinglyLinkedList<Point> FindPath(Map map, Point start, Point chest, Point[] chests,
            Queue<SinglyLinkedList<Point>> queue, HashSet<Point> queueVisited, Dictionary<Point, SinglyLinkedList<Point>> path)
        {
            var direction = new int[,] { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } };
            while (queue.Count != 0) //в очередь постепенно кладутся точки которые мы будем использовать на следующем шаге
            {
                var point = queue.Dequeue();
                if (map.Dungeon[point.Value.X, point.Value.Y] == MapCell.Wall) continue;
                if (!path.ContainsKey(point.Value)) //проверяем не находили ли мы этот путь
                {
                    if (chests.Contains(point.Value))
                    {
                        if (path.Count < chests.Length && queueVisited.Count < map.Dungeon.Length) queue.Enqueue(point);
                            //для того чтобы просмотреть оставшиеся точки
                        return point;
                    }
                }
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