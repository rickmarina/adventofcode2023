using System.Text.RegularExpressions;

public class Day6 : IDay
{
    public void SolvePart1()
    {
        var data = File.ReadAllLines(@"./day6/input1.txt"); 

        var times = data[0].Split(":")[1].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        var distances = data[1].Split(":")[1].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

        System.Console.WriteLine("times: "+string.Join(",", times));
        System.Console.WriteLine("distances: "+string.Join(",", distances));

        double ans = 1; 
        for (int i=0; i< times.Length; i++) { 
            ans *= CalcResultForTimeSpace(times[i], distances[i]);
        }

        System.Console.WriteLine("Result: "+ans);
    }

    private double CalcResultForTimeSpace(double t, double s) {
        double min = (t - Math.Sqrt(t*t - (4*s)))/2;
        double max = (t + Math.Sqrt(t*t - (4*s)))/2;
        double r1 = (int)(min+1); 
        double r2 = Math.Ceiling(max);
        return (r2-r1);
    }

    public void SolvePart2()
    {
        var data = File.ReadAllLines(@"./day6/input1.txt"); 

        var time = Convert.ToDouble(string.Join("",data[0].Split(":")[1].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()));
        var distance = Convert.ToDouble(string.Join("",data[1].Split(":")[1].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()));

        System.Console.WriteLine("time: "+time);
        System.Console.WriteLine("distance: "+distance);

        double ans = 1; 

        ans *= CalcResultForTimeSpace(time,distance);

        System.Console.WriteLine("Result: "+ans);
    }
}