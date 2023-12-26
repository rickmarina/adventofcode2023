#nullable disable

using System.Data;

/// <summary>
/// --- Day 19: Aplenty ---
/// </summary>
/// 
public class Condition {
    public string variable;
    public int value;
    public string op;
    public string destiny;

    public override string ToString()
    {
        return $"var: {variable} op:{op} value: {value} -> {destiny}";
    }

}
public class Rules {
    
    public string name;
    public string default_rule; 
    public List<Condition> conditions;

    public override string ToString()
    {
        return $"name: {name} def: {default_rule} conds: {string.Join(",", conditions)}";
    }

}

    

public class Day19 : BaseDay, IDay
{
    public Dictionary<string, Rules> workflows = new(); 

    public void SolvePart1()
    {
        var data = File.ReadAllText("./day19/input1.txt");

        var wflows = data.Split("\r\n\r\n")[0].Split("\r\n");
        var parts = data.Split("\r\n\r\n")[1].Split("\r\n");

        foreach (var wk in wflows) {
            var name = wk.Split("{")[0].ToString();
            
            Rules r = new Rules() { name = name};
            
            List<Condition> conditions = new(); 
            foreach (var cond in wk[..^1].Split("{")[1].Split(",")) {
                if (cond.IndexOf(":") != -1) {
                    
                    var parse = cond.Split(":"); 

                    Condition c = new Condition() { 
                        variable = parse[0][0].ToString(), 
                        value = int.Parse(parse[0].Substring(2)),
                        destiny = parse[1],
                        op = parse[0][1].ToString()
                    };
                    conditions.Add(c);

                } else {
                    r.default_rule = cond;
                }

            }
            r.conditions = conditions; 
            
            workflows.Add(name, r);

        }

        foreach (var w in workflows) {
            System.Console.WriteLine("-> "+w.ToString());
        }

        //loop parts 
        foreach (var p in parts) { 
            string current_workflow = "in";

            System.Console.WriteLine($"part: {p}");
            
            List<(string, int)> partInfo = new(); 
            foreach (var elem in p[1..^1].Split(",")) { 
                partInfo.Add( (elem[0].ToString(), int.Parse(elem[2..])));
            }

            while (current_workflow !="A" || current_workflow !="R") {
                
            }
        }
    }

    public void SolvePart2()
    {
        throw new NotImplementedException();
    }
}