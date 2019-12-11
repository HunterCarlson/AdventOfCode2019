using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019
{
    /*
     * Day 3: Crossed Wires
     */
    public class Day3
    {
        public static void Solve()
        {
            Console.WriteLine("Day 3: Crossed Wires");
            Console.WriteLine();

            //Test1();

            Part1();
            //Part2();
        }

        private static void Test1()
        {
            List<Point> wire1 = GetWirePath("R8,U5,L5,D3");
            List<Point> wire2 = GetWirePath("U7,R6,D4,L4");

            IEnumerable<Point> intersections = wire1.Intersect(wire2, new PointEqualityComparer());
            int minDistance = intersections.Select(p => p.ManhattanDistance()).Where(d => d != 0).OrderBy(x => x).First();
        }

        private static void Part1()
        {
            (List<Point> wire1, List<Point> wire2) = GetWirePaths();
            IEnumerable<Point> intersections = wire1.Intersect(wire2, new PointEqualityComparer());
            int minDistance = intersections.Select(p => p.ManhattanDistance()).Where(d => d != 0).OrderBy(x => x).First();

            Console.WriteLine("Part 1:");
            Console.WriteLine($"Distance: {minDistance}");
            Console.WriteLine();
        }

        private static void Part2()
        {
            Console.WriteLine("Part 2:");
            Console.WriteLine("");
            Console.WriteLine();
        }

        private static (List<Point> wire1, List<Point> wire2) GetWirePaths()
        {
            string inputFilePath = $"{Environment.CurrentDirectory}/Input/Day3Input.txt";
            string[] lines = File.ReadAllLines(inputFilePath);
            List<Point> wire1 = GetWirePath(lines[0]);
            List<Point> wire2 = GetWirePath(lines[1]);

            return (wire1, wire2);
        }

        private static List<Point> GetWirePath(string dirStr)
        {
            var points = new List<Point>();
            int x = 0;
            int y = 0;
            points.Add(new Point(x, y));

            string[] directions = dirStr.Split(',');

            foreach (string direction in directions)
            {
                int distance = int.Parse(direction.Substring(1));

                switch (direction[0])
                {
                    case 'U':
                    {
                        foreach (int d in Enumerable.Range(0, distance))
                        {
                            y++;
                            points.Add(new Point(x, y));
                        }

                        break;
                    }
                    case 'D':
                    {
                        foreach (int d in Enumerable.Range(0, distance))
                        {
                            y--;
                            points.Add(new Point(x, y));
                        }

                        break;
                    }
                    case 'L':
                    {
                        foreach (int d in Enumerable.Range(0, distance))
                        {
                            x--;
                            points.Add(new Point(x, y));
                        }

                        break;
                    }
                    case 'R':
                    {
                        foreach (int d in Enumerable.Range(0, distance))
                        {
                            x++;
                            points.Add(new Point(x, y));
                        }

                        break;
                    }
                    default: throw new Exception($"Invalid direction {direction[0]}");
                }
            }

            return points;
        }
    }

    public class Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public int ManhattanDistance()
        {
            return Math.Abs(X) + Math.Abs(Y);
        }
    }

    public class PointEqualityComparer : IEqualityComparer<Point>
    {
        public bool Equals(Point a, Point b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        public int GetHashCode(Point obj)
        {
            return obj.X.GetHashCode() ^ obj.Y.GetHashCode();
        }
    }
}