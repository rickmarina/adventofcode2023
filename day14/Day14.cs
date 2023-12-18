

using System.Collections.Immutable;

/// <summary>
/// --- Day 14: Parabolic Reflector Dish ---
/// </summary>
public class Day14 : BaseDay, IDay
{

    int total = 0;
    public void SolvePart1()
    {
        var map = File.ReadAllLines("./day14/input1.txt").Select(y => y.ToArray()).ToArray();

        Location<int> tiltDirection = new Location<int>(0, -1);

        TiltMap(map, tiltDirection);

        var total = TotalLoad(map);

        System.Console.WriteLine($"{total}");
        // ShowMap(map);


    }

    public void SolvePart2()
    {
        var map = File.ReadAllLines("./day14/input1.txt").Select(y => y.ToArray()).ToArray();

        long CYCLES = 1000000000;
        Dictionary<long, long> gridLoad = new();
        Dictionary<string, long> seen = new();

        long iter = 0;
        long offset = 0;
        long first = 0; 

        for (long c = 0; c < CYCLES; c++)
        {
            TiltMap(map, new Location<int>(0, -1));

            RotateMatrixClockwise(map);
            TiltMap(map, new Location<int>(0, -1));

            RotateMatrixClockwise(map);
            TiltMap(map, new Location<int>(0, -1));

            RotateMatrixClockwise(map);
            TiltMap(map, new Location<int>(0, -1));

            RotateMatrixClockwise(map);

            var key = GetSHA256(string.Join("", map.Select(x => string.Join("", x))));
            if (!seen.ContainsKey(key))
            {
                gridLoad.Add(c, TotalLoad(map));
                seen.Add(key, c);
            }
            else
            {
                System.Console.WriteLine($"cycle repeated at {c}");
                first = seen[key];
                iter= c; 
                offset = c - first;
                break;
            }
        }

        long finalcycle = first + ((CYCLES - first) % (offset))-1;
        System.Console.WriteLine($"first {first} iter: {iter} calc: {finalcycle}");
        if (gridLoad.ContainsKey(finalcycle)) {
            System.Console.WriteLine($"solution: {gridLoad[finalcycle]}");

        }
        // System.Console.WriteLine($"final after {CYCLES} cycles. offset {offset}");
        // ShowMap(map);
        // Console.ReadKey();
    }

    public long TotalLoad(char[][] map) { 
        return map.Select((x,idx)=> x.Count(x=> x=='O')*(map.Length-idx)).Sum();
    }
    public void TiltMap(char[][] map, Location<int> direction)
    {
        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[0].Length; j++)
            {
                if (map[i][j] == 'O')
                {
                    // System.Console.WriteLine($"roca: {i},{j}");
                    bool move = true;
                    Location<int> rock = new Location<int>(j, i);
                    do
                    {
                        Location<int> destination = new Location<int>(rock.x + direction.x, rock.y + direction.y);

                        //movimiento posible 
                        if (destination.x >= 0 && destination.x < map[0].Length && destination.y >= 0 && destination.y < map.Length && map[destination.y][destination.x] == '.')
                        {
                            // System.Console.WriteLine($"moviendo roca: {rock} -> {destination}");
                            map[destination.y][destination.x] = 'O';
                            map[rock.y][rock.x] = '.';
                            rock.x = destination.x;
                            rock.y = destination.y;
                        }
                        else
                            move = false;

                        // ShowMap(map);
                        // System.Console.WriteLine("---");
                        // Console.ReadKey();
                    } while (move);

                    total += map.Length - rock.y;

                }
            }
        }

        // System.Console.WriteLine($"total: {total}");
    }


}