
/// <summary>
/// --- Day 9: Mirage Maintenance ---
/// </summary>

public class Day9 : BaseDay, IDay
{
    public void SolvePart1()
    {
        var data = File.ReadAllLines(@"./day9/input1.txt");
        List<decimal> finals = new(); 

        foreach (var hist in data) {

            var nums = hist.Split(" ").Select(decimal.Parse).ToArray();
            Stack<decimal[]> stack = new(); 

            stack.Push(nums);
            while (nums.Any(x=> x != 0)) {
                nums = nums[..^1].Select((x,i) => nums[i+1]-x).ToArray(); 
                stack.Push(nums); 
                System.Console.WriteLine(string.Join(",",nums));
            }

            decimal result = 0;
            while (stack.Count > 0) { 
                var last = stack.Pop();
                System.Console.WriteLine("from stack: "+string.Join(",",last));

                result = last[^1] + result;
            }
            System.Console.WriteLine($"result: {result}");
            finals.Add(result); 

        }

        System.Console.WriteLine($"answer: {finals.Sum()}");
    }

    public void SolvePart2()
    {
        var data = File.ReadAllLines(@"./day9/input1.txt");
        List<decimal> finals = new(); 

        foreach (var hist in data) {

            var nums = hist.Split(" ").Select(decimal.Parse).ToArray();
            Stack<decimal[]> stack = new(); 

            stack.Push(nums);
            while (nums.Any(x=> x != 0)) {
                nums = nums[..^1].Select((x,i) => nums[i+1]-x).ToArray(); 
                stack.Push(nums); 
                System.Console.WriteLine(string.Join(",",nums));
            }

            decimal result = 0;
            while (stack.Count > 0) { 
                var last = stack.Pop();
                System.Console.WriteLine("from stack: "+string.Join(",",last));

                result = last[0] - result;
            }
            System.Console.WriteLine($"result: {result}");
            finals.Add(result); 

        }

        System.Console.WriteLine($"answer: {finals.Sum()}");
    }
}