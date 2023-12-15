

using System.Diagnostics;
using System.Text.RegularExpressions;

/// <summary>
/// --- Day 12: Hot Springs ---
/// </summary>
public class Day12 : BaseDay, IDay
{
    // Generate all combinations possible of . and # for each ? and then eval it with regex
    public void SolvePart1()
    {
        var data = File.ReadAllLines(@"./day12/input1.txt"); 

        long total = 0; 
        foreach (var line in data) { 
            var cad = line.Split(" ")[0]; 
            var groups = line.Split(" ")[1].Split(","); 

            System.Console.WriteLine(cad);
            System.Console.WriteLine(string.Join(",",groups));
            var pattern = @"^\.*("+string.Join("",groups.Select(x=> @"[\?|#]{"+x+@"}[\.|\?]+"))[0..^8]+@")[\.|?]*$";

            System.Console.WriteLine($"pattern: {pattern}");
            
            Regex pat = new Regex(pattern, RegexOptions.Compiled);
            total += GenerateCombinations(cad, pat);    
            //Regex pat = new Regex(@"^\.*([\?|#]{3}[\.|\?]+[\?|#]{2}[\.|\?]+[\?|#]{1})\.*$"); 
        }

        System.Console.WriteLine($"total: {total}");
    }


    // Recursive method with DP 
    public void SolvePart2()
    {
        var data = File.ReadAllLines(@"./day12/input1.txt"); 

        long total = 0; 

        Stopwatch sw = new Stopwatch(); 
        sw.Start(); 

        foreach (var line in data) { 

            System.Console.WriteLine($"line: {line}...");
            var cad = line.Split(" ")[0]; 

            var groups = line.Split(" ")[1].Split(","); 

            var unfoldcad = UnfoldSpring(cad); 
            var unfoldgroups = UnfoldGroup(groups); 

            Dictionary<string,long> cache = new(); 
            total +=RecursiveCombinations(unfoldcad.ToArray(), unfoldgroups, 0, 0, 0, cache);
        }

        sw.Stop();
        System.Console.WriteLine($"msecs: {sw.Elapsed.TotalMilliseconds}");

        System.Console.WriteLine($"total: {total}");
    }

    public long RecursiveCombinations(char[] dots, int[] groups, int i, int bi, int current, Dictionary<string, long> cache) {
        string key = $"{i}|{bi}|{current}";
        if (cache.ContainsKey(key))
            return cache[key];

        if (i == dots.Length) {
            if (bi == groups.Length && current == 0)
                return 1; 
            else if (bi == groups.Length-1 && groups[bi] == current)
                return 1; 
            else 
                return 0;
        }

        long result = 0; 
        foreach (var c in ".#") {
            if (dots[i] == c ||dots[i] == '?') 
                if (c == '.' && current == 0)
                    result += RecursiveCombinations(dots, groups, i+1, bi, 0,cache);
                else if (c == '.' && current > 0 && bi < groups.Length && groups[bi] == current)
                    result += RecursiveCombinations(dots,groups, i+1, bi+1,0,cache);
                else if (c == '#')
                    result += RecursiveCombinations(dots,groups, i+1, bi, current+1, cache);
        }


        cache[key] = result;
        return result;
    }

    public string UnfoldSpring(string cad) { 
        return string.Join("?",Enumerable.Range(0,5).Select(x=> cad));
    }

    public int[] UnfoldGroup(string[] cad) {
        return Enumerable.Range(0,5).SelectMany(x=> cad).Select(int.Parse).ToArray();
    }



     public long GenerateCombinations(string cad, Regex validPattern) {
        Queue<string> queue = new(); 
        queue.Enqueue(cad); 

        long validCombinations = 0;
        
        long iterations = 0; 
        while (queue.Count > 0) { 
            iterations++;
            var current = queue.Dequeue(); 

            if (!current.Any(x=> x == '?')) {
                    validCombinations++;
            }
            else {
                Regex r = new Regex(@"\?");
                string n1 = r.Replace(current, "#", 1);
                string n2 = r.Replace(current, ".", 1);

                if (validPattern.IsMatch(n1)) { 
                    queue.Enqueue(n1);
                } 
                if (validPattern.IsMatch(n2)) { 
                    queue.Enqueue(n2);
                } 
            } 
        }

        System.Console.WriteLine($"combinations: {validCombinations} iterations: {iterations}");

        return validCombinations;
    }


} 