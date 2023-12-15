// --- Day 11: Cosmic Expansion ---

public class Day11 : BaseDay, IDay
{
    // Manhattan distance and identify rows and columns that double the space would work
    public void SolvePart1()
    {
        List<int> rowsDouble = new(); 
        List<int> colsDouble = new(); 
        List<Location<decimal>> galaxies = new(); 

        var data = File.ReadAllLines(@"./day11/input1.txt").Select((x,idx)=> {
                if (!x.Any(c=> c == '#')) {
                    rowsDouble.Add(idx);
                }
                return x.ToArray();
            }
        ).ToArray();
        
        for (int c=0; c<data[0].Length;c++) {
            bool anygalaxy = Enumerable.Range(0, data.Length).Any(row => data[row][c] == '#');
            if (!anygalaxy) 
                colsDouble.Add(c);
        }

        for (int i=0;i<data.Length;i++) { 
            for (int j = 0; j < data[0].Length; j++)
            {
                if (data[i][j] == '#') {
                    decimal dx = colsDouble.Count(x=> x<j);
                    decimal dy = rowsDouble.Count(x=> x<i);

                    //System.Console.WriteLine($"galaxy at {i}:{j} dy/dx {dy}/{dx}");
                    galaxies.Add(new Location<decimal>(j+dx,i+dy));
                }
            }
        }


        System.Console.WriteLine("cols: "+string.Join(",",colsDouble));
        System.Console.WriteLine("rows: "+string.Join(",",rowsDouble));
        System.Console.WriteLine("galaxies after expands: "+string.Join("|",galaxies));

        int pairs = 0;
        decimal totalDistance = 0;
        for (int i=0;i<galaxies.Count;i++) {
            for (int j=i+1;j<galaxies.Count;j++) {
                pairs++;
                decimal distance = Math.Abs(galaxies[i].x-galaxies[j].x)+Math.Abs(galaxies[i].y-galaxies[j].y);
                // System.Console.WriteLine($"distance from {galaxies[i]} to {galaxies[j]} : {distance}");
                totalDistance+=distance;
            }
        }

        System.Console.WriteLine($"Answer. Pairs {pairs} distance {totalDistance}");


    }

    public void SolvePart2()
    {
        List<int> rowsDouble = new(); 
        List<int> colsDouble = new(); 
        List<Location<decimal>> galaxies = new(); 

        var data = File.ReadAllLines(@"./day11/input1.txt").Select((x,idx)=> {
                if (!x.Any(c=> c == '#')) {
                    rowsDouble.Add(idx);
                }
                return x.ToArray();
            }
        ).ToArray();
        
        for (int c=0; c<data[0].Length;c++) {
            bool anygalaxy = Enumerable.Range(0, data.Length).Any(row => data[row][c] == '#');
            if (!anygalaxy) 
                colsDouble.Add(c);
        }

        for (int i=0;i<data.Length;i++) { 
            for (int j = 0; j < data[0].Length; j++)
            {
                if (data[i][j] == '#') {
                    decimal factor = 1_000_000;
                    decimal dx = colsDouble.Count(x=> x<j)*(factor-1);
                    decimal dy = rowsDouble.Count(x=> x<i)*(factor-1);

                    //System.Console.WriteLine($"galaxy at {i}:{j} dy/dx {dy}/{dx}");
                    galaxies.Add(new Location<decimal>(j+dx,i+dy));
                }
            }
        }


        System.Console.WriteLine("cols: "+string.Join(",",colsDouble));
        System.Console.WriteLine("rows: "+string.Join(",",rowsDouble));
        System.Console.WriteLine("galaxies after expands: "+string.Join("|",galaxies));

        int pairs = 0;
        decimal totalDistance = 0;
        for (int i=0;i<galaxies.Count;i++) {
            for (int j=i+1;j<galaxies.Count;j++) {
                pairs++;
                decimal distance = Math.Abs(galaxies[i].x-galaxies[j].x)+Math.Abs(galaxies[i].y-galaxies[j].y);
                // System.Console.WriteLine($"distance from {galaxies[i]} to {galaxies[j]} : {distance}");
                totalDistance+=distance;
            }
        }

        System.Console.WriteLine($"Answer. Pairs {pairs} distance {totalDistance}");
    }
}