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
            return File.ReadAllLines("3.txt");
        }

        public string GetResultPart1()
        {
            var (visitedFirst, visitedSecond) = GetLines();
            var intersections = visitedFirst.Intersect(visitedSecond);

            var closest = int.MaxValue;
            foreach (var point in intersections)
            {
                closest = Math.Min(closest, Math.Abs(point.X) + Math.Abs(point.Y));
            }
            return $"{closest}";
            
        }

        public string GetResultPart2()
        {
            var (visitedFirst, visitedSecond) = GetLines();
            var intersections = visitedFirst.Intersect(visitedSecond);

            var minSteps = int.MaxValue;
            foreach (var intersection in intersections)
            {
                var first = visitedFirst.First(x => x.Equals(intersection));
                var second = visitedSecond.First(x => x.Equals(intersection));

                minSteps = Math.Min(minSteps, first.Step + second.Step);
            }

            return $"{minSteps}";
        }

        private (List<Point> visitedFirst, List<Point> visitedSecond) GetLines()
        {
            var lines = GetWires();
            var firstLineCommands = lines[0].Split(',').Select(ToCommand).ToArray();
            var secondLineCommands = lines[1].Split(',').Select(ToCommand).ToArray();

            var visitedFirst = GetPoints(firstLineCommands);
            var visitedSecond = GetPoints(secondLineCommands);
            return (visitedFirst, visitedSecond);
        }

        private static List<Point> GetPoints((char cmd, int n)[] commands)
        {
            var lastX = 0;
            var lastY = 0;
            var loopCounter = 0;
            var points = new List<Point>();
            foreach (var (c, n) in commands)
            {
                
                switch (c)
                {
                    case 'U':
                        for (int i = 1; i <= n; i++)
                        {
                            lastY++;
                            points.Add(new Point(lastX, lastY, ++loopCounter));
                        }
                        break;
                    case 'D':
                        for (int i = 1; i <= n; i++)
                        {
                            lastY--;
                            points.Add(new Point(lastX, lastY, ++loopCounter));
                        }
                        break;
                    case 'R':
                        for (int i = 1; i <= n; i++)
                        {
                            lastX++;
                            points.Add(new Point(lastX, lastY, ++loopCounter));
                        }
                        break;
                    case 'L':
                        for (int i = 1; i <= n; i++)
                        {
                            lastX--;
                            points.Add(new Point(lastX, lastY, ++loopCounter));
                        }
                        break;
                }
            }

            return points;
        }

        private static (char cmd, int n) ToCommand(string command)
        {
            var c = command[0];
            var n = command.Substring(1, command.Length - 1);

            return (c, int.Parse(n));
        }

        public class Point
        {
            public Point(int x, int y, int step)
            {
                X = x;
                Y = y;
                Step = step;
            }
            public int X { get; }
            public int Y { get;  }
            public int Step { get;  }
            
            public override bool Equals(object obj)
            {
                if (!(obj is Point other)) return false;

                return X == other.X && Y == other.Y;
            }

            public override int GetHashCode()
            {
                return 42 * X.GetHashCode() * Y.GetHashCode();
            }
        }
    }
}
