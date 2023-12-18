
using System.Text;

/// <summary>
/// --- Day 13: Point of Incidence ---
/// part2: xor , then count greater than 0 to get total differences between strings
/// </summary>
public class Day13 : BaseDay, IDay
{
    int totalVertical = 0; 
    int totalHorizontal = 0; 
    bool fixsmug = true;
    bool smugfound = false; 
    public void SolvePart1()
    {
        var patterns = File.ReadAllText(@"./day13/input1.txt").Split("\r\n\r\n").ToArray();

        // _ = Differences("#.##..##.","#.....##.");
        // return;

        int count = 1; 
        foreach (var p in patterns) {
            System.Console.WriteLine($"pattern #{count++}");

            var pat = p.Split("\r\n").Select(x=> x).ToArray();

            bool perfect = false; 
            foreach (var row in FindEqualRows(pat)) {
                System.Console.WriteLine("Possible mirror Row: "+string.Join(",",row));
                if (IsPerfectReflectionHorizontal(pat, row.Item1, row.Item2)) {
                    totalHorizontal += (row.Item1+1)*100;
                    System.Console.WriteLine($"perfect h reflection {row.Item1} {row.Item2} smugfound {this.smugfound}");
                    perfect = true; 
                    break;
                }
                this.smugfound = false; 
            }

            if (!perfect) {
                foreach(var col in FindEqualCols(pat)) { 
                    if (IsPerfectReflectionVertical(pat, col.Item1, col.Item2)) {
                        totalVertical += col.Item1+1;
                        System.Console.WriteLine($"perfect v reflection {col.Item1} {col.Item2} smugfound {this.smugfound}");
                        perfect = true;
                        break;
                    }
                    this.smugfound = false; 
                }
            }

            

            this.fixsmug = true; 
            this.smugfound = false; 
            

        }

        System.Console.WriteLine($"{totalVertical+totalHorizontal}");
    }

    //32371
    public (int, int) SearchPerfectReflection(string[] pat,List<(int, int)> mirrorRows, List<(int, int)> mirrorCols) {
        foreach(var col in mirrorCols) { 
                if (IsPerfectReflectionVertical(pat, col.Item1, col.Item2)) {
                    totalVertical += col.Item1+1;
                    System.Console.WriteLine($"perfect v reflection {col.Item1} {col.Item2}");
                    return (col.Item1, col.Item2);
                }
            }
        foreach(var row in mirrorRows) {
            if (IsPerfectReflectionHorizontal(pat, row.Item1, row.Item2)) {
                totalHorizontal += (row.Item1+1)*100;
                System.Console.WriteLine($"perfect h reflection {row.Item1} {row.Item2}");
                return (row.Item1, row.Item2);
            } 
        }

        return (0,0);
    }

    public bool IsPerfectReflectionHorizontal(string[] pat, int i1, int i2) {
        int offset = 1; 
        bool perfect = true; 

        // System.Console.WriteLine($"{i1-offset} {i2+offset}");
        // System.Console.WriteLine($"{pat[i1-offset]} vs {pat[i2+offset]}");
        while (i1-offset >= 0 && i2+offset <pat.Length) {
            
            if (!AreRowsEqual(pat, i1-offset, i2+offset)) {
                return false;
                }
            offset++;
        }

        return perfect && (!fixsmug || smugfound); 
    }

    public bool IsPerfectReflectionVertical(string[] pat, int i1, int i2) {
        int offset = 1; 
        bool perfect = true; 
        while (i1-offset >= 0 && i2+offset < pat[0].Length) {
            if (!AreColsEqual(pat, i1-offset, i2+offset) ) {
                return false;
            }
            offset++;
        }

       return perfect && (!fixsmug || smugfound); 

    }

    public IEnumerable<(int, int)> FindEqualRows(string[] pat) {
        for (int i=0; i<pat.Length-1;i++) { 
            if (pat[i].Equals(pat[i+1]))
                yield return (i, i+1);
            
            var diff = Differences(pat[i],pat[i+1]);
            if (diff == 1) { 
                this.smugfound = true; 
                yield return (i, i+1);
            }
        }
    }

    public IEnumerable<(int, int)> FindEqualCols(string[] pat) { 
        for (int j=0;j<pat[0].Length-1;j++) { 
            if (pat.All(x=> x[j] == x[j+1])) 
                yield return (j, j+1);

            var str1 = string.Join("", pat.Select(x=> x[j])); 
            var str2 = string.Join("", pat.Select(x=> x[j+1])); 
            var diff = Differences(str1, str2); 
            if (diff == 1) {
                this.smugfound = true; 
                yield return (j, j+1);
            }
        }
    }
    public int Differences(string a, string b) { 
       return Enumerable.Range(0,a.Length).Select(i => a[i] ^ b[i]).Count(x=> x>0);
    }

    public bool AreRowsEqual(string[] pat, int r1, int r2) {
        if (!this.smugfound) {
            if (Differences(pat[r1],pat[r2]) == 1) {
                this.smugfound = true; 
                System.Console.WriteLine($"smug in rows ({r1}:{r2}): {pat[r1]} | {pat[r2]}");
                return true;
            }
        }
        return pat[r1].Equals(pat[r2]); 
    }

    public bool AreColsEqual(string[] pat, int c1, int c2) {
        if (!this.smugfound) {
            var str1 = string.Join("", pat.Select(x=> x[c1])); 
            var str2 = string.Join("", pat.Select(x=> x[c2])); 
            if (Differences(str1,str2) == 1) {
                this.smugfound= true; 
                return true;
            }
        }
        return pat.All(x=> x[c1]== x[c2]);
    }

    public void SolvePart2()
    {
        throw new NotImplementedException();
    }
}