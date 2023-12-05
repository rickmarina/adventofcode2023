
//--- Day 5: If You Give A Seed A Fertilizer ---
public class Day5 : IDay
{

    class Map
    {
        public decimal destination_range;
        public decimal source_range;
        public decimal length;

        public Map(decimal dr, decimal sr, decimal length)
        {
            this.destination_range = dr;
            this.source_range = sr;
            this.length = length;
        }

        public decimal CalcDestination(decimal source)
        {
            if (source >= source_range && source < source_range + length)
            {
                return destination_range + (source - source_range);
            }
            return -1;
        }

    }

    public void SolvePart1()
    {
        var data = File.ReadAllLines(@"./day5/input1.txt");
        List<decimal> seeds = new();
        Dictionary<string, List<Map>> maps = new();

        string lastsection = "";
        foreach (var line in data)
        {
            if (string.IsNullOrEmpty(line))
                continue;
            if (line.StartsWith("seeds:"))
            {
                seeds = line.Split(":")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(decimal.Parse).ToList();
            }
            else if (line.EndsWith(":"))
            {
                lastsection = line;
                maps.Add(line, new List<Map>());
            }
            else
            {
                var mapinfo = line.Split(" ").Select(decimal.Parse).ToArray();
                maps[lastsection].Add(new Map(mapinfo[0], mapinfo[1], mapinfo[2]));
            }
        }

        decimal minLocation = decimal.MaxValue;

        foreach (var s in seeds)
        {
            System.Console.WriteLine($"calculation seed: {s}");

            decimal mapresult = s;
            foreach (var kv in maps)
            {
                System.Console.Write($"mapping {kv.Key}... {mapresult} ");

                for (int m = 0; m < kv.Value.Count; m++)
                {
                    decimal r = kv.Value[m].CalcDestination(mapresult);
                    if (r > -1)
                    {
                        mapresult = r;
                        System.Console.WriteLine("-> " + r);
                        break;
                    }
                }
            }

            minLocation = Math.Min(minLocation, mapresult);
        }

        System.Console.WriteLine($"Min Locaiton: {minLocation}");

    }

    public void SolvePart2()
    {
        var data = File.ReadAllLines(@"./day5/input1.txt");
        List<decimal> inputSeeds = new();
        Dictionary<string, List<Map>> maps = new();

        string lastsection = "";
        foreach (var line in data)
        {
            if (string.IsNullOrEmpty(line))
                continue;
            if (line.StartsWith("seeds:"))
            {
                inputSeeds = line.Split(":")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(decimal.Parse).ToList();
            }
            else if (line.EndsWith(":"))
            {
                lastsection = line;
                maps.Add(line, new List<Map>());
            }
            else
            {
                var mapinfo = line.Split(" ").Select(decimal.Parse).ToArray();
                maps[lastsection].Add(new Map(mapinfo[0], mapinfo[1], mapinfo[2]));
            }
        }


        Stack<(decimal, decimal)> seeds = new();

        for (int i = 0; i < inputSeeds.Count; i += 2)
        {
            seeds.Push((inputSeeds[i], inputSeeds[i] + inputSeeds[i + 1]));
        }

        foreach (var block in maps) { 

            Stack<(decimal, decimal)> newSeeds = new(); 
            while (seeds.Count > 0) { 
                (decimal s, decimal e) = seeds.Pop();

                bool overlap = false; 
                foreach (Map m in block.Value) {
                    decimal os = Math.Max(s, m.source_range);
                    decimal oe = Math.Min(e, m.source_range + m.length);
                    if (os < oe) { 
                        overlap = true;
                        newSeeds.Push((os - m.source_range + m.destination_range, oe - m.source_range + m.destination_range));
                        if (os > s)
                            seeds.Push((s, os));
                        if (e > oe)
                            seeds.Push((oe, e));
                        break;
                    }
                }

                if (!overlap) 
                    newSeeds.Push((s,e));

            }

            seeds = newSeeds;
        }

        System.Console.WriteLine($"{string.Join(",",seeds)}");

        System.Console.WriteLine(seeds.Min(x=> x.Item1));


    }
}