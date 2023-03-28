using System;
using System.Collections.Generic;
using System.Linq;

namespace eightQueen
{
    class Program
    {
        static void Main(string[] args)
        {
            var map = new EightQueen();
            Explore(map);
            Console.ReadLine();
        }

        static int index = 1;
        static void Explore(EightQueen map)
        {
            var minPos = map.QueenPos.Any() ? map.QueenPos.Max() : 0;
            foreach (var pos in map.PosList.Where(o => o > minPos))
            {
                var newmap = new EightQueen(map, pos);
                if (newmap.enough)
                {
                    Console.WriteLine($"Solution {index++}:");
                    string queenPos="";
                    foreach (var p in newmap.QueenPos)
                    {
                        queenPos  += $"({(p / 10).ToString()},{(p % 10).ToString()}),";
                    }
                    Console.WriteLine(queenPos.Remove(queenPos.Length-1));
                    newmap.Print();
                }
                else if (!newmap.notEnough)
                {
                    Explore(newmap);
                }
            }

        }
    }
}
