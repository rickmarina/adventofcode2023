/// <summary>
/// --- Day 15: Lens Library ---
/// </summary>
/// 
public class Lens {
    public string label;
    public string focal; 

    public Lens (string l, string f) { 
        label = l;
        focal = f;
    }
}
public class Day15 : BaseDay, IDay
{
    public void SolvePart1()
    {
        long result =0;
        var dat =File.ReadAllLines("./day15/input1.txt"); 
        foreach (var line in dat) { 
            foreach (var op in line.Split(",")) {
                result += Hash(op);
                System.Console.WriteLine($"op: {op} result: {result}");
            }
        }

        System.Console.WriteLine("final: "+result);
    }

    public Dictionary<int, List<Lens>> boxes = new(); 

    public void SolvePart2()
    {

        var dat =File.ReadAllLines("./day15/input1.txt"); 
        foreach (var line in dat) { 
            foreach (var op in line.Split(",")) {
                System.Console.WriteLine($"op: {op}");
                string label = ""; 
                string focallength = ""; 
                string command = ""; 
                if (op.IndexOf("=") != -1) { 
                    command = "=";
                    label = op.Split("=")[0]; 
                    focallength = op.Split("=")[1];
                }
                else if (op.IndexOf("-") != -1) { 
                    command = "-";
                    label = op.Split("-")[0]; 
                }
                int boxId = Hash(label);

                System.Console.WriteLine($"label: {label} boxid: {boxId} command: {command} focal: {focallength}");
                UpdateBox(boxId, label, focallength, command);

                //Console.ReadKey();
            }
        }
        long result =0;

        foreach (var b in boxes) { 
            string lens = string.Join(",", b.Value.Select(x=> $"[{x.label} {x.focal}]"));
            System.Console.WriteLine($"boxid: {b.Key} lens: {lens} ");

            result += (b.Key+1) * b.Value.Select((x,i) => (i+1) * int.Parse(x.focal)).Sum();
        }
        System.Console.WriteLine($"result: {result}");

    }

    public void UpdateBox(int boxid, string label, string focal, string command) { 
        if (command.Equals("=")) {
            if (!boxes.ContainsKey(boxid)) { 
                boxes.Add(boxid, new() { new Lens(label, focal)});
            }else {
                var l = boxes[boxid].FirstOrDefault(x=> x.label.Equals(label)); 
                if (l != null) 
                    l.focal = focal; 
                else 
                    boxes[boxid].Add(new Lens(label,focal));
            }
        }
        else if (command.Equals("-")) { 
            if (boxes.ContainsKey(boxid)) {
                boxes[boxid].RemoveAll(x=> x.label.Equals(label));
            }
        }
    }

    public int Hash(string str) { 
        var arr = str.Select(x=> (int)x);
        var result = arr.Aggregate(
            (a,b) => {
                return ((a*17)%256)+b;
            }
        );
        return result*17%256;
    }
    
}