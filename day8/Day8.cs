// --- Day 8: Haunted Wasteland ---

public class Day8 : IDay
{
    struct Node
    {
        public string left;
        public string right;
    }
    public void SolvePart1()
    {
        var data = File.ReadAllText("./day8/input1.txt").Split("\r\n\r\n").Select(x => x).ToArray();
        string instructions = data[0];

        System.Console.WriteLine(instructions);

        Dictionary<string, Node> map = new();
        foreach (var line in data[1].Split("\r\n"))
        {

            var dat = line.Split("=", StringSplitOptions.TrimEntries);
            string k = dat[0];
            string l = dat[1].Split(",")[0][1..];
            string r = dat[1].Split(",")[1][1..^1];

            System.Console.WriteLine($"K: {k} L: {l}-{r}");
            map.Add(k, new Node() { left = l, right = r });
        }

        string current = "AAA";
        int pos = 0;
        int steps = 0;
        while (!current.Equals("ZZZ"))
        {
            steps++;
            if (instructions[pos] == 'L')
            {
                current = map[current].left;
            }
            else
            {
                current = map[current].right;
            }
            pos++;
            if (pos == instructions.Length)
                pos = 0;
        }

        System.Console.WriteLine($"Steps: {steps}");

    }

    public void SolvePart2()
    {
        var data = File.ReadAllText("./day8/input1.txt").Split("\r\n\r\n").Select(x => x).ToArray();
        string instructions = data[0];

        Dictionary<string, Node> map = new();
        foreach (var line in data[1].Split("\r\n"))
        {

            var dat = line.Split("=", StringSplitOptions.TrimEntries);
            string k = dat[0];
            string l = dat[1].Split(",")[0][1..];
            string r = dat[1].Split(",")[1][1..^1];

            map.Add(k, new Node() { left = l, right = r });
        }

        var starts = map.Where(x => x.Key.EndsWith("A")).Select(x => x.Key).ToArray();
        System.Console.WriteLine($"STARTS: {string.Join(",", starts)}");

        List<decimal> totalsteps = new();

        for (int m = 0; m < starts.Length; m++)
        {
            int pos = 0;
            int steps = 0;
            while (!starts[m].EndsWith("Z"))
            {
                steps++;
                if (instructions[pos] == 'L')
                    starts[m] = map[starts[m]].left;
                else
                    starts[m] = map[starts[m]].right;

                pos++;
                if (pos == instructions.Length)
                    pos = 0;

                //System.Console.WriteLine($"{string.Join(",", starts)}");
            }

            totalsteps.Add(steps);
            System.Console.WriteLine($"steps: {steps}");
        }


        decimal lcm = totalsteps[0];
        for (int i=1; i<totalsteps.Count;i++) {
            lcm = LCM(lcm, totalsteps[i]);
        }

        System.Console.WriteLine(lcm);


    }


    static decimal GCD(decimal a, decimal b)
    {
        if (a == 0)
            return b;
        return GCD(b % a, a);
    }

    static decimal LCM(decimal a, decimal b)
    {
        return (a / GCD(a, b)) * b;
    }
}