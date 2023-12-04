
//--- Day 4: Scratchcards ---

using System.Text.RegularExpressions;

public class Day4 : IDay
{

    public void SolvePart1()
    {
        var data = File.ReadAllLines(@"./day4/input1.txt");

        var ans = data.Select(card => { 
            var parts = card.Split("|", StringSplitOptions.RemoveEmptyEntries);
            string p0 = parts[0].Trim();
            string p1 = parts[1].Trim();

            System.Console.WriteLine($"parts {p0}-{p1}");
            
            var wnumbers = p0.Split(":")[1].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var mine = p1.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList(); 

            var result = mine.Count(x=> wnumbers.Any(n => n == x));
            return result > 0 ? Math.Pow(2, result-1) : 0;
        }).Sum();

        System.Console.WriteLine(ans);
    }

    public void SolvePart2()
    {

        var data = File.ReadAllLines(@"./day4/input1.txt");

        int[] total = Enumerable.Range(0,data.Length).Select(x=> 1).ToArray(); 

        _ = data.Select((card, idx) => { 
            var parts = card.Split("|");
            string p0 = parts[0].Trim();
            string p1 = parts[1].Trim();

            var wnumbers = p0.Split(":")[1].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var mine = p1.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList(); 

            var result = mine.Count(x=> wnumbers.Any(n => n == x));
            System.Console.WriteLine($"index: {idx} total cards:{total[idx]} matches: {result}");
            for (int i=idx+1;i<=idx+result; i++) {
                total[i]+=total[idx];
            }
            return 1;
        }).Sum(); 

        System.Console.WriteLine(total.Sum());
    }
}