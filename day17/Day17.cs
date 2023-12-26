
/// <summary>
/// --- Day 17: Clumsy Crucible ---
/// </summary>
/// 

#nullable disable
public class QueueNode
{
    public int heatLoss = 0;
    public Location<int> loc;
    public int dX = 0;
    public int dY = 0;
    public int n = 0;


    public override string ToString()
    {
        return $"{loc.ToString()}|{dX}|{dY}|{n}";
    }
}

public class Day17 : BaseDay, IDay
{

    public void SolvePart1()
    {
        var map = File.ReadAllLines("./day17/input1.txt").Select(x => x.ToArray()).ToArray();

        QueueNode start = new() { loc = new(0, 0), dX = 0, dY = 0, n = 0, heatLoss = 0 };
        Location<int> end = new(map[0].Length - 1, map.Length - 1);

        Dijsktra(map, start, end);

    }


    public void Dijsktra(char[][] map, QueueNode start, Location<int> end)
    {
        HashSet<string> reached = new();
        PriorityQueue<QueueNode, int> queue = new();
        queue.Enqueue(start, 0);

        while (queue.Count > 0)
        {

            var current = queue.Dequeue();

            if (current.loc.Equals(end))
            {
                System.Console.WriteLine($"exit found!");
                System.Console.WriteLine($"heat loss: {current.heatLoss}");
                break;
            }

            if (!reached.Add(current.ToString()))
                continue;

            if (current.n < 3 && (current.dX, current.dY) != (0, 0))
            {
                // System.Console.WriteLine($"seguimos camino en la misma dirección!");
                Location<int> newLocation = new(current.loc.x + current.dX, current.loc.y + current.dY);

                if (newLocation.x >= 0 && newLocation.x < map[0].Length && newLocation.y >= 0 && newLocation.y < map.Length)
                {
                    int cost = current.heatLoss + int.Parse(map[newLocation.y][newLocation.x].ToString());
                    QueueNode nq = new()
                    {
                        loc = newLocation,
                        dX = current.dX,
                        dY = current.dY,
                        heatLoss = cost,
                        n = current.n + 1
                    };
                    // System.Console.WriteLine($"add queue element: {nq.ToString()}");

                    queue.Enqueue(nq, cost);

                }
            }

            foreach (var (dx, dy) in new[] { (0, 1), (1, 0), (0, -1), (-1, 0) })
            {
                if ((dx, dy) != (current.dX, current.dY) && (dx, dy) != (-current.dX, -current.dY))
                {
                    Location<int> newLocation = new(current.loc.x + dx, current.loc.y + dy);
                    if (newLocation.x >= 0 && newLocation.x < map[0].Length && newLocation.y >= 0 && newLocation.y < map.Length)
                    {
                        int cost = current.heatLoss + int.Parse(map[newLocation.y][newLocation.x].ToString());
                        QueueNode nq = new()
                        {
                            loc = newLocation,
                            dX = dx,
                            dY = dy,
                            heatLoss = cost,
                            n = 1
                        };
                        // System.Console.WriteLine($"add queue element: {nq.ToString()}");
                        queue.Enqueue(nq, cost);

                    }
                }
            }

        }


    }


    public void Dijsktra2(char[][] map, QueueNode start, Location<int> end)
    {
        HashSet<string> reached = new();
        PriorityQueue<QueueNode, int> queue = new();
        queue.Enqueue(start, 0);

        while (queue.Count > 0)
        {

            var current = queue.Dequeue();

            if (current.loc.Equals(end) && current.n >= 4)
            {
                System.Console.WriteLine($"exit found!");
                System.Console.WriteLine($"heat loss: {current.heatLoss}");
                break;
            }

            if (!reached.Add(current.ToString()))
                continue;

            if (current.n < 10 && (current.dX, current.dY) != (0, 0))
            {
                // System.Console.WriteLine($"seguimos camino en la misma dirección!");
                Location<int> newLocation = new(current.loc.x + current.dX, current.loc.y + current.dY);

                if (newLocation.x >= 0 && newLocation.x < map[0].Length && newLocation.y >= 0 && newLocation.y < map.Length)
                {
                    int cost = current.heatLoss + int.Parse(map[newLocation.y][newLocation.x].ToString());
                    QueueNode nq = new()
                    {
                        loc = newLocation,
                        dX = current.dX,
                        dY = current.dY,
                        heatLoss = cost,
                        n = current.n + 1
                    };
                    // System.Console.WriteLine($"add queue element: {nq.ToString()}");

                    queue.Enqueue(nq, cost);

                }
            }

            if (current.n >= 4 || (current.dX, current.dY) == (0, 0))
            {
                foreach (var (dx, dy) in new[] { (0, 1), (1, 0), (0, -1), (-1, 0) })
                {
                    if ((dx, dy) != (current.dX, current.dY) && (dx, dy) != (-current.dX, -current.dY))
                    {
                        Location<int> newLocation = new(current.loc.x + dx, current.loc.y + dy);
                        if (newLocation.x >= 0 && newLocation.x < map[0].Length && newLocation.y >= 0 && newLocation.y < map.Length)
                        {
                            int cost = current.heatLoss + int.Parse(map[newLocation.y][newLocation.x].ToString());
                            QueueNode nq = new()
                            {
                                loc = newLocation,
                                dX = dx,
                                dY = dy,
                                heatLoss = cost,
                                n = 1
                            };
                            // System.Console.WriteLine($"add queue element: {nq.ToString()}");
                            queue.Enqueue(nq, cost);

                        }
                    }
                }
            }

        }


    }


    public void SolvePart2()
    {
        var map = File.ReadAllLines("./day17/input1.txt").Select(x => x.ToArray()).ToArray();

        QueueNode start = new() { loc = new(0, 0), dX = 0, dY = 0, n = 0, heatLoss = 0 };
        Location<int> end = new(map[0].Length - 1, map.Length - 1);

        Dijsktra2(map, start, end);
    }
}