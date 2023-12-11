//--- Day 7: Camel Cards ---

public class Day7 : IDay
{
    public void SolvePart1()
    {
        var data = File.ReadAllLines(@"./day7/input1.txt");

        foreach (var hand in data)
            System.Console.WriteLine(hand);

        var hands = data.Select(x=> new Hand(x.Split(" ")[0], Convert.ToInt32(x.Split(" ")[1]))).ToList();

        System.Console.WriteLine("----");
        hands.Sort(new HandsComparer());

        var ans = hands.Select((x,idx)=> x.bid * (idx+1)).Sum();

        System.Console.WriteLine($"Total: {ans}");
    }

    public void SolvePart2()
    {
        var data = File.ReadAllLines(@"./day7/input1.txt");

        foreach (var hand in data)
            System.Console.WriteLine(hand);

        var hands = data.Select(x=> new Hand(x.Split(" ")[0], Convert.ToInt32(x.Split(" ")[1]))).ToList();

        System.Console.WriteLine("----");
        hands.Sort(new HandsComparer2());

        var ans = hands.Select((x,idx)=> x.bid * (idx+1)).Sum();

        System.Console.WriteLine($"Total: {ans}");
    }
}
public struct Hand
{
    public string cards;
    public decimal bid;

    public Hand(string c, int b)
    {
        this.cards = c;
        this.bid = b;
    }
}


public class HandsComparer2 : IComparer<Hand>
{
    public Dictionary<char, int> values = new Dictionary<char, int>() {
        {'A', 14},
        {'K', 13},
        {'Q', 12},
        {'T', 10},
        {'9', 9},
        {'8', 8},
        {'7', 7},
        {'6', 6},
        {'5', 5},
        {'4', 4},
        {'3', 3},
        {'2', 2},
        {'J', 1}
    };


    public int Compare(Hand x, Hand y)
    {
        var cJ1 = x.cards.Count(x=> x == 'J');
        var cJ2 = y.cards.Count(x=> x == 'J');

        var g1 = x.cards.Where(x=> x != 'J').GroupBy(x => x).Select(g => new { c = g.Count(), v = g.Key }).OrderByDescending(x=>x.c).ToList();
        var g2 = y.cards.Where(x=> x != 'J').GroupBy(x => x).Select(g => new { c = g.Count(), v = g.Key }).OrderByDescending(x=>x.c).ToList();

        if (g1.Count == 0) { 
            g1.Add(new {c= cJ1, v= 'J'});
            cJ1 = 0;
        }
        if (g2.Count == 0) {
            g2.Add(new {c= cJ2, v= 'J'});
            cJ2 = 0;
        }

        for (int i=0; i< g1.Count;i++) {
            var diff2 = g1[i].c+cJ1 - (g2[i].c+cJ2);
            cJ1 = 0; cJ2 = 0;
            if (diff2 != 0) 
                return diff2;
        }

        for (int i = 0; i < x.cards.Length; i++)
        {
            var diff2 = values[x.cards[i]] - values[y.cards[i]];
            if (diff2 != 0)
                return diff2;
        }
        return 0;
    }
}

public class HandsComparer : IComparer<Hand>
{
    public Dictionary<char, int> values = new Dictionary<char, int>() {
        {'A', 14},
        {'K', 13},
        {'Q', 12},
        {'J', 11},
        {'T', 10},
        {'9', 9},
        {'8', 8},
        {'7', 7},
        {'6', 6},
        {'5', 5},
        {'4', 4},
        {'3', 3},
        {'2', 2}
    };


    public int Compare(Hand x, Hand y)
    {
        var diff = x.cards.Distinct().Count() - y.cards.Distinct().Count();

        if (diff != 0)
            return -1 * diff;

        var g1 = x.cards.GroupBy(x => x).Select(g => new { c = g.Count(), v = g.Key }).OrderByDescending(x=>x.c).ToList();
        var g2 = y.cards.GroupBy(x => x).Select(g => new { c = g.Count(), v = g.Key }).OrderByDescending(x=>x.c).ToList();

        for (int i=0; i< g1.Count;i++) {
            var diff2 = g1[i].c - g2[i].c;
            if (diff2 != 0) 
                return diff2;
        }

        for (int i = 0; i < x.cards.Length; i++)
        {
            var diff2 = values[x.cards[i]] - values[y.cards[i]];
            if (diff2 != 0)
                return diff2;
        }

        return 0;
    }
}