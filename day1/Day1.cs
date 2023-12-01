
//Trebuchet 
//It is possible achive the solution using a possitive lookahead regex pattern: 
//(?=(one|two|three|four|five|six|seve|eight|nine|ten|\d))
public class Day1 : IDay { 


    public void SolvePart1() { 
        var data = File.ReadAllLines(@"./day1/input1.txt");

        decimal sum = 0; 
        foreach (string line in data) {
            Console.WriteLine(line);

            string str_num = line.First(x=> char.IsNumber(x)).ToString()+line.Last(x=> char.IsNumber(x)).ToString();
            sum += decimal.Parse(str_num);
        }
        System.Console.WriteLine(sum);
    }

    private string Normalize(string cad) { 
        Dictionary<string, string> replaces = new() { 
            {"oneight", "oneeight"},
            {"eightwo", "eighttwo"},
            {"eighthree", "eightthree"},
            {"threeight", "threeeight"},
            {"twone", "twoone"},
            {"sevenine", "sevennine"},
            {"nineight", "nineeight"},
            {"fiveight", "fiveeight"},
            {"one", "1"},
            {"two", "2"},
            {"three", "3"},
            {"four", "4"},
            {"five", "5"},
            {"six", "6"},
            {"seven", "7"},
            {"eight", "8"},
            {"nine", "9"}
        };
        foreach (var kv in replaces) {
            cad = cad.Replace(kv.Key,kv.Value);
        }
        return cad;
    }

    public void SolvePart2() { 
        var data = File.ReadAllLines(@"./day1/input2.txt");

        decimal sum = 0; 
        foreach (string line in data) {
            Console.WriteLine(line);
            string normalized = Normalize(line); 

            System.Console.WriteLine($"normalized -> {normalized}");
            string str_num = normalized.First(x=> char.IsNumber(x)).ToString()+normalized.Last(x=> char.IsNumber(x)).ToString();
            sum += decimal.Parse(str_num);
        }
        System.Console.WriteLine(sum);
    }

}