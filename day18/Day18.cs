/// <summary>
/// --- Day 18: Lavaduct Lagoon ---
/// </summary>

public class Day18 : BaseDay, IDay
{
    public void SolvePart1()
    {
        var data = File.ReadAllLines("./day18/input1.txt").Select(x => new { d = x.Split(" ")[0], u = x.Split(" ")[1] }).ToArray();

        Location<int> position = new(1, 1);

        List<Location<int>> hole = new();
        hole.Add(position);

        int totalunits = 0;
        foreach (var dig in data)
        {
            System.Console.WriteLine($"Order: {dig.d} {dig.u} units");
            int units = int.Parse(dig.u);
            totalunits += units;
            if (dig.d == "D")
                position.y += units;
            else if (dig.d == "L")
                position.x -= units;
            else if (dig.d == "U")
                position.y -= units;
            else if (dig.d == "R")
                position.x += units;
            hole.Add(new Location<int>(position.x, position.y));
        }

        var area = GaussArea(hole);

        //picks theorem 
        // A = i + b/2 -1 
        // i = A - b/2 +1
        var i = area - (totalunits / 2) + 1;

        System.Console.WriteLine($"Total {i + totalunits}");
    }

    public void SolvePart2()
    {
        var data = File.ReadAllLines("./day18/input1.txt").Select(x => new { d = x.Split(" ")[0], u = x.Split(" ")[1], c= x.Split(" ")[2][2..^1] }).ToArray();
        Location<int> position = new(1, 1);

        List<Location<int>> hole = new();
        hole.Add(position);

        double totalunits = 0;
        foreach (var dig in data)
        {

            int units = Convert.ToInt32(dig.c[..^1], 16);
            string dir = dig.c[^1].ToString();
            System.Console.WriteLine($"Order: {dig.d} {dig.u} units color: {dig.c} {units}");

            totalunits += units;
            if (dir == "1")
                position.y += units;
            else if (dir == "2")
                position.x -= units;
            else if (dir == "3")
                position.y -= units;
            else if (dir == "0")
                position.x += units;
            hole.Add(new Location<int>(position.x, position.y));
        }

         var area = GaussArea(hole);

        //picks theorem 
        // A = i + b/2 -1 
        // i = A - b/2 +1
        var i = area - (totalunits / 2) + 1;

        System.Console.WriteLine($"Total {i + totalunits}");

    }

}