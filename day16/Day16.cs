

#nullable disable

using System.Data;

/// <summary>
/// --- Day 16: The Floor Will Be Lava ---
/// </summary>
public class Beam
{
    public Location<int> pos;
    public Location<int> direction;

    public Beam(int x, int y, int dx, int dy)
    {
        this.pos = new Location<int>(x, y);
        this.direction = new Location<int>(dx, dy);
    }

    public override string ToString()
    {
        return $"{pos.x}|{pos.y}|{direction.x}|{direction.y}";
    }
}

public class Day16 : BaseDay, IDay
{

    public List<Beam> beams = new() { new Beam(0, 0, 1, 0) };
    public char[][] printMap;
    public void SolvePart1()
    {

        var map = File.ReadAllLines("./day16/input1.txt").Select(x => x.ToArray()).ToArray();

        printMap = CopyMatrix(map);
        //ShowMap(map);

        int howmanylocations = SolvePath(map);
        System.Console.WriteLine($"total locations energized: {howmanylocations}");

    }

    public void SolvePart2()
    {
        int max =0; 
        var map = File.ReadAllLines("./day16/input1.txt").Select(x => x.ToArray()).ToArray();

        printMap = CopyMatrix(map);

        Beam b = new Beam(0,0,0,0);
        for(int i=0;i<map.Length;i++) { 
            for (int j=0;j<map[0].Length;j++) { 
                if (i==0) {
                    b = new Beam(j,i, 0,1);
                }
                else if (i == map.Length-1) { 
                    b = new Beam(j,i, 0,-1);
                }
                else if (j==0) {
                    b = new Beam(j,i,1,0);
                }
                else if (j==map[0].Length){
                    b = new Beam(j,i,-1,0);
                }
                if (i==0 || i==map.Length-1 || j==0 || j == map[0].Length) {
                    // System.Console.WriteLine($"test beam.. {b}");
                    this.beams.Clear();
                    this.beams.Add(b);
                    int howmanylocations = SolvePath(map);
                    // System.Console.WriteLine($"total locations energized: {howmanylocations}");
                    max = Math.Max(max, howmanylocations);

                }

            }
        }

        System.Console.WriteLine($"max: {max}");
    }

    public int SolvePath(char[][] map)
    {
        Queue<Beam> queue = new(new[] { beams[0] });
        HashSet<string> reached = new() { beams[0].ToString() };
        HashSet<Location<int>> locations = new(new[] { beams[0].pos } );


        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            foreach (var d in GetDirections(map, current))
            {
                locations.Add(d.pos);
                if (reached.Add(d.ToString()))
                    queue.Enqueue(d);
            }
        }

        return locations.Count;
    }

    public bool HorizontalDirection(Beam b) => b.direction.x != 0;
    public bool VerticalDirection(Beam b) => b.direction.y != 0;
    public bool InsideMap(char[][] map, Location<int> l) => l.x >= 0 && l.x < map[0].Length && l.y >= 0 && l.y < map.Length;

    public IEnumerable<Beam> GetDirections(char[][] map, Beam current)
    {
        Beam nb = new Beam(current.pos.x + current.direction.x, current.pos.y + current.direction.y, current.direction.x, current.direction.y);
        char c = map[current.pos.y][current.pos.x];

        if (c == '.' && InsideMap(map, nb.pos))
            yield return nb;

        if (c == '-')
        {
            if (HorizontalDirection(current))
            {
                if (InsideMap(map, nb.pos))
                    yield return nb;
            }
            else
            {
                Beam b1 = new Beam(current.pos.x - 1, current.pos.y, -1, 0);
                if (InsideMap(map, b1.pos))
                    yield return b1;
                Beam b2 = new Beam(current.pos.x + 1, current.pos.y, 1, 0);
                if (InsideMap(map, b2.pos))
                    yield return b2;
            }
        }

        else if (c == '|')
        {
            if (VerticalDirection(current))
            {
                if (InsideMap(map, nb.pos))
                    yield return nb;
            }
            else
            {
                Beam b1 = new Beam(current.pos.x, current.pos.y - 1, 0, -1);
                if (InsideMap(map, b1.pos))
                    yield return b1;
                Beam b2 = new Beam(current.pos.x, current.pos.y + 1, 0, 1);
                if (InsideMap(map, b2.pos))
                    yield return b2;
            }
        }

        else if (c == '/')
        {
            Beam b1 = new Beam(current.pos.x - current.direction.y, current.pos.y-current.direction.x,-1*current.direction.y, -1*current.direction.x);
            if (InsideMap(map, b1.pos))
                yield return b1;
        }

        else if (c == '\\') {
            Beam b1 = new Beam(current.pos.x + current.direction.y, current.pos.y+current.direction.x,current.direction.y, current.direction.x);
            if (InsideMap(map, b1.pos))
                yield return b1;
        }



    }
    
}