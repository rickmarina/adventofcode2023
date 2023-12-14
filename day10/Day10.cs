//--- Day 10: Pipe Maze ---
/*
    | is a vertical pipe connecting north and south.
    - is a horizontal pipe connecting east and west.
    L is a 90-degree bend connecting north and east.
    J is a 90-degree bend connecting north and west.
    7 is a 90-degree bend connecting south and west.
    F is a 90-degree bend connecting south and east.
    . is ground; there is no pipe in this tile.
    S is the starting position of the animal; there is a pipe on this tile, but your sketch doesn't show what shape the pipe has.
*/
using System.Data;
using System.Reflection.Metadata;

public class Day10 : BaseDay, IDay
{

    public static Dictionary<char, List<Location<int>>> directions = new() {
            {'-', new() { new Location<int>(-1,0), new Location<int>(1,0) } },
            {'|', new() { new Location<int>(0,-1), new Location<int>(0,1) } },
            {'L', new() { new Location<int>(1,0), new Location<int>(0,-1) } },
            {'F', new() { new Location<int>(1,0),  new Location<int>(0,1) } },
            {'J', new() { new Location<int>(-1,0), new Location<int>(0,-1) } },
            {'7', new() { new Location<int>(-1,0), new Location<int>(0,1) } }
            //{'S', new() { new Location(-1,0),  new Location(1,0),  new Location(0,-1),  new Location(0,1) } }
        };
    public void SolvePart1()
    {
        Location<int> start = new(0, 0);

        var map = File.ReadAllLines("./day10/input1.txt").Select((x, idx) =>
        {
            int posX = x.IndexOf('S');
            if (posX != -1)
            {
                start.x = posX;
                start.y = idx;
            }
            return x.ToArray();

        }).ToArray();

        System.Console.WriteLine($"start: {start.ToString()}");


        //Determine which character is S 
        foreach (var key in directions.Keys)
        {
            map[start.y][start.x] = key;
            if (GetNeighbors(map, start).Count() == 2) {
                break;
            }
        }

        var path = SolveMap(map, start);

        System.Console.WriteLine($"Ans: {path.Count/2}");

    }

    public HashSet<Location<int>> SolveMap(char[][] map, Location<int> start)
    {
        Queue<Location<int>> queue = new Queue<Location<int>>();
        queue.Enqueue(start);

        var track = CopyMatrix(map);

        HashSet<Location<int>> reached = new();
        reached.Add(start);

        int steps = 0;
        while (queue.Count > 0)
        {
            Location<int> current = queue.Dequeue();

            track[current.y][current.x] = 'O';
            steps++;

            foreach (var next in GetNeighbors(map, current))
            {
                if (reached.Add(next))
                    queue.Enqueue(next);
            }

        }

        System.Console.WriteLine($"Total steps: {steps}");

        return reached;
    }

    public static IEnumerable<Location<int>> GetNeighbors(char[][] map, Location<int> from)
    {

        foreach (var d in directions[map[from.y][from.x]])
        {
            //check if the new position is possible 
            Location<int> newLoc = new Location<int>(from.x + d.x, from.y + d.y);

            if (newLoc.x >= 0 && newLoc.x < map[0].Length && newLoc.y >= 0 && newLoc.y < map.Length && map[newLoc.y][newLoc.x] != '.')
            {
                if (directions[map[newLoc.y][newLoc.x]].Any(p => p.x == -1* d.x && p.y == -1* d.y))
                    yield return newLoc;
            }
        }
    }


    //Possible solutions for part2: 
    //Explode array to avoid one pipe touches another pipe, then BFS
    //Shoelace formula and Pick's theorem
    //Raycasting (vertical or diagonal)
    public void SolvePart2()
    {
        Location<int> start = new(0, 0);

        var map = File.ReadAllLines("./day10/input1.txt").Select((x, idx) =>
        {
            int posX = x.IndexOf('S');
            if (posX != -1)
            {
                start.x = posX;
                start.y = idx;
            }
            return x.ToArray();

        }).ToArray();


        //Determine which character is S 
        foreach (var key in directions.Keys)
        {
            map[start.y][start.x] = key;
            if (GetNeighbors(map, start).Count() == 2) {
                break;
            }
        }

        System.Console.WriteLine($"Character is : {map[start.y][start.x]}");

        var path = SolveMap(map, start); 

        //remove pipes not used in path
        for (int i=0; i< map.Length;i++) 
            for (int j=0;j< map[0].Length;j++) 
                if (!path.Contains(new Location<int>(j,i))) 
                    map[i][j]='.';

        //diagonal raytracing
        int totalInside = 0; 
        for (int i=0; i< map.Length;i++) {
            for (int j=0;j< map[0].Length;j++) { 
                if (!path.Contains(new Location<int>(j,i))) {
                    //raytracing this point 
                    int count = 0; 
                    for (int ray=1; (j+ray)< map[0].Length && (i+ray)<map.Length;ray++) { 
                        var tile = map[i+ray][j+ray];
                        if (tile != 'L' && tile != '7' && (tile == '-' || tile == '|' || tile == 'J' || tile =='F')) {
                            count++;
                        }
                    }

                    if (count % 2== 0) {
                        System.Console.WriteLine($"{i}/{j} outside");
                        map[i][j]='O';
                        }
                    else {
                        System.Console.WriteLine($"{i}/{j} INSIDE!");

                        map[i][j]='I';
                        totalInside++;
                    }
                    // ShowMap(map);
                    // Console.ReadKey();
                }
            }
        }

        

        System.Console.WriteLine($"Total inside: {totalInside}");

        //raycasting diagonal each point not in the path 
    }


    static void ShowMap(char[][] matrix)
    {
        foreach (char[] fila in matrix)
        {
            foreach (char c in fila)
            {
                Console.Write(c + " ");
            }
            Console.WriteLine();
        }
    }

}