using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent
{
    public class Day3
    {
        private string[] GetWires()
        {
            return File.ReadAllLines("Inputs/3.txt");
        }

        public string GetResultPart1()
        {
            var lines = GetWires();
            var firstLineCommands = lines[0].Split(',').Select(ToCommand).ToArray();
            var secondLineCommands = lines[1].Split(',').Select(ToCommand).ToArray();
            var visitedFirst = new List<(int, int)>();
            var visitedSecond = new List<(int, int)>();


            var lastX = 0;
            var lastY = 0;
            foreach (var (c, n) in firstLineCommands)
            {
                switch (c)
                {
                    case 'U':
                        for (int i = 1; i <= n; i++)
                        {
                            lastY++;
                            visitedFirst.Add((lastX, lastY));
                        }
                        break;
                    case 'D':
                        for (int i = 1; i <= n; i++)
                        {
                            lastY--;
                            visitedFirst.Add((lastX, lastY));
                        }
                        break;
                    case 'R':
                        for (int i = 1; i <= n; i++)
                        {
                            lastX++;
                            visitedFirst.Add((lastX, lastY));
                        }
                        break;
                    case 'L':
                        for (int i = 1; i <= n; i++)
                        {
                            lastX--;
                            visitedFirst.Add((lastX, lastY));
                        }
                        break;
                }
            }

            lastX = 0;
            lastY = 0;
            foreach (var (c, n) in secondLineCommands)
            {
                switch (c)
                {
                    case 'U':
                        for (int i = 1; i <= n; i++)
                        {
                            lastY++;
                            visitedSecond.Add((lastX, lastY));
                        }
                        break;
                    case 'D':
                        for (int i = 1; i <= n; i++)
                        {
                            lastY--;
                            visitedSecond.Add((lastX, lastY));
                        }
                        break;
                    case 'R':
                        for (int i = 1; i <= n; i++)
                        {
                            lastX++;
                            visitedSecond.Add((lastX, lastY));
                        }
                        break;
                    case 'L':
                        for (int i = 1; i <= n; i++)
                        {
                            lastX--;
                            visitedSecond.Add((lastX, lastY));
                        }
                        break;
                }
            }

            var firstQ1 = visitedFirst.Where(x => x.Item1 > 0 && x.Item2 > 0).ToArray();
            var firstQ2 = visitedFirst.Where(x => x.Item1 < 0 && x.Item2 > 0).ToArray();
            var firstQ3 = visitedFirst.Where(x => x.Item1 < 0 && x.Item2 < 0).ToArray();
            var firstQ4 = visitedFirst.Where(x => x.Item1 > 0 && x.Item2 < 0).ToArray();

            var secondQ1 = visitedSecond.Where(x => x.Item1 > 0 && x.Item2 > 0).ToArray();
            var secondQ2 = visitedSecond.Where(x => x.Item1 < 0 && x.Item2 > 0).ToArray();
            var secondQ3 = visitedSecond.Where(x => x.Item1 < 0 && x.Item2 < 0).ToArray();
            var secondQ4 = visitedSecond.Where(x => x.Item1 > 0 && x.Item2 < 0).ToArray();

            var shortestQ1 = GetShortest(firstQ1, secondQ1);
            var shortestQ2 = GetShortest(firstQ2, secondQ2);
            var shortestQ3 = GetShortest(firstQ3, secondQ3);
            var shortestQ4 = GetShortest(firstQ4, secondQ4);

            return $"{shortestQ1} {shortestQ2} {shortestQ3} {shortestQ4}";


        }
        private static int GetShortest((int, int)[] first, (int, int)[] second)
        {
            var shortest = int.MaxValue;
            foreach (var itemFirst in first)
            {
                foreach (var itemSecond in second)
                {
                    if (itemFirst == itemSecond)
                    {
                        var dist = CalculateManhattanDistance(0, 0, itemFirst.Item1, itemFirst.Item2);
                        if (dist < shortest)
                            shortest = dist;
                    }
                }
            }

            return shortest;
        }

        public static int CalculateManhattanDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }

        private static (char cmd, int n) ToCommand(string command)
        {
            var c = command[0];
            var n = command.Substring(1, command.Length - 1);

            return (c, int.Parse(n));
        }
    }
}
