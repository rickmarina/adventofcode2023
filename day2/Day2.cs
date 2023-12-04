

using System.Text.RegularExpressions;

//--- Day 2: Cube Conundrum ---
public class Day2 : IDay
{

    public void SolvePart1()
    {
        var data = File.ReadAllLines(@"./day2/input1.txt");

        Regex pattern = new Regex(@"(\d+) (red|green|blue)");
        const int maxRed = 12, maxGreen = 13, maxBlue = 14;
        decimal totalSum = 0;
        int idGame = 0;

        foreach (string line in data)
        {
            idGame++;

            bool IsValidGame = true;
            foreach (var set in line.Split(";"))
            {

                System.Console.WriteLine($"Checking idGame {idGame}. Set: {set}");
                Dictionary<string, int> count = new() {
                    {"red", 0},
                    {"green", 0},
                    {"blue", 0}
                };

                foreach (Match match in pattern.Matches(set))
                {
                    int num = Convert.ToInt32(match.Groups[1].Value);
                    string color = match.Groups[2].Value;
                    count[color] += num;
                }

                if (count["red"] > maxRed || count["blue"] > maxBlue || count["green"] > maxGreen)
                {
                    IsValidGame = false;
                    break;
                }

            }
            if (IsValidGame)
                totalSum += idGame;
        }

        System.Console.WriteLine($"Total: {totalSum}");

    }

    public void SolvePart2()
    {
        var data = File.ReadAllLines(@"./day2/input2.txt");
        int idGame = 0;
        decimal totalSum = 0;

        Regex pattern = new Regex(@"(\d+) (red|green|blue)");
        foreach (string line in data)
        {
            idGame++;
            Dictionary<string, int> count = new() {
                    {"red", 0},
                    {"green", 0},
                    {"blue", 0}
                };
            foreach (Match match in pattern.Matches(line))
            {
                int num = Convert.ToInt32(match.Groups[1].Value);
                string color = match.Groups[2].Value;

                count[color] = Math.Max(count[color], num);
            }

            System.Console.WriteLine($"Min in game: {idGame} Red: {count["red"]} Green {count["green"]} Blue {count["blue"]}");
            totalSum += count["red"]*count["green"]*count["blue"];
        }

        System.Console.WriteLine($"total: {totalSum}");
    }
}