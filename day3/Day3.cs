using System.Text.RegularExpressions;

//--- Day 3: Gear Ratios ---
public class Day3 : IDay
{
    class Location {
        public int y;
        public int x;

        public Location(int y, int x) { 
            this.y = y;
            this.x = x; 
        }

        public override string ToString()
        {
            return $"y:{y} x:{x}";
        }
    }

    class Symbol { 
        public char caracter;
        public Location pos;

    }
    class Number { 
        public int num;
        public Location pos;
        public bool include; 

        public void SetInclude(bool i) {
            include = i;
        }
    }

    public void SolvePart1()
    {

        var data = File.ReadAllLines("./day3/input1.txt");

        Dictionary<int, List<Number>> numbers = new(); 
        List<Symbol> specials = new(); 

        for (int i=0; i<data.Length;i++) { 
            List<Number> listNumbers = new();  
            System.Console.WriteLine(data[i]);

            Regex pat = new Regex(@"(\d+)");

            foreach (Match match in  pat.Matches(data[i])) { 
                //System.Console.WriteLine($"{match.Index} {match.Groups[0].Value}");
                listNumbers.Add(new Number() { num = Convert.ToInt32(match.Groups[0].Value), pos = new Location(i, match.Index) { }  });
            }
            numbers.Add(i, listNumbers);

            for(int j=0; j< data[i].Length;j++) { 
                if (!char.IsNumber(data[i][j]) && data[i][j] != '.')
                    specials.Add(new Symbol() { pos = new Location(i,j), caracter = data[i][j] }); 
            }
        }

        foreach (var s in specials) { 
            System.Console.WriteLine($"Special: {s.caracter} {s.pos}");

            //Row before 
            if (s.pos.y != 0) { 
                IncludeNumbersForSpecial(numbers[s.pos.y-1], s);
            }
            //Same row than special 
            IncludeNumbersForSpecial(numbers[s.pos.y], s);
            //Row after 
            if (s.pos.y != data.Length) {
                IncludeNumbersForSpecial(numbers[s.pos.y+1], s);
            }
        }

        decimal totalSum = 0; 
        foreach (var nums in numbers.Values) { 
            totalSum += nums.Where(x=> x.include).Select(x => Convert.ToInt32(x.num)).Sum();
        }

        System.Console.WriteLine($"Total: {totalSum}");

    }

    private void IncludeNumbersForSpecial(List<Number> numbers, Symbol s) { 
        foreach (var n in numbers) {
                if (n.pos.x <= s.pos.x+1 && (n.pos.x + n.num.ToString().Length) >= s.pos.x ) {
                    n.include = true; 
                    System.Console.WriteLine($"include: {n.num}");
                }
                    
            }
    }

    public void SolvePart2()
    {
        var data = File.ReadAllLines("./day3/input1.txt");

        Dictionary<int, List<Number>> numbers = new(); 
        List<Symbol> specials = new(); 

        for (int i=0; i<data.Length;i++) { 
            List<Number> listNumbers = new();  
            System.Console.WriteLine(data[i]);

            Regex pat = new Regex(@"(\d+)");

            foreach (Match match in  pat.Matches(data[i])) { 
                //System.Console.WriteLine($"{match.Index} {match.Groups[0].Value}");
                listNumbers.Add(new Number() { num = Convert.ToInt32(match.Groups[0].Value), pos = new Location(i, match.Index) { }  });
            }
            numbers.Add(i, listNumbers);

            for(int j=0; j< data[i].Length;j++) { 
                if (!char.IsNumber(data[i][j]) && data[i][j] != '.')
                    specials.Add(new Symbol() { pos = new Location(i,j), caracter = data[i][j] }); 
            }
        }

        decimal total = 0; 

        //Just check all * 
        foreach (var s in specials.Where(x=> x.caracter.Equals('*'))) { 
            System.Console.WriteLine($"Special: {s.caracter} {s.pos}");

            List<int> adjacents =  new();
            //Row before 
            if (s.pos.y != 0) { 
                adjacents.AddRange(Adjacents(numbers[s.pos.y-1], s));
            }
            //Same row than special 
            adjacents.AddRange(Adjacents(numbers[s.pos.y], s));

            //Row after 
            if (s.pos.y != data.Length) {
                adjacents.AddRange(Adjacents(numbers[s.pos.y+1], s));
            }

            if (adjacents.Count == 2) {
                //Is a gear 
                int mult = adjacents.Aggregate((a,b) => a*b);
                System.Console.WriteLine($"gear detected: {string.Join(", ", adjacents)} mult: {mult}");
                total += mult;
            }
        }

        System.Console.WriteLine($"Total: {total}");
    }

    private IEnumerable<int> Adjacents(List<Number> numbers, Symbol s) { 
        foreach (var n in numbers) {
                if (n.pos.x <= s.pos.x+1 && (n.pos.x + n.num.ToString().Length) >= s.pos.x ) {
                    System.Console.WriteLine($"adjacent: {n.num}");
                    yield return Convert.ToInt32(n.num);
                }
                    
            }
    }
}