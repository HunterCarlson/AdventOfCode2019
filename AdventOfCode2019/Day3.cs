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

            //Test2();

            Part2();
        }

        private static void Test1()
        {
            List<Point> wire1 = GetWirePath("R8,U5,L5,D3");
            List<Point> wire2 = GetWirePath("U7,R6,D4,L4");

            IEnumerable<Point> intersections = wire1.Intersect(wire2, new PointEqualityComparer());
            int minDistance = intersections.Select(p => p.ManhattanDistance()).Where(d => d != 0).Min();
        }

        private static void Part1()
        {
            (List<Point> wire1, List<Point> wire2) = GetWirePaths();
            IEnumerable<Point> intersections = wire1.Intersect(wire2, new PointEqualityComparer());
            int minDistance = intersections.Select(p => p.ManhattanDistance()).Where(d => d != 0).Min();

            Console.WriteLine("Part 1:");
            Console.WriteLine($"Distance: {minDistance}");
            Console.WriteLine();
        }

        private static void Test2()
        {
            List<Point> wire1 = GetWirePath("R75,D30,R83,U83,L12,D49,R71,U7,L72");
            List<Point> wire2 = GetWirePath("U62,R66,U55,R34,D71,R55,D58,R83");

            IEnumerable<Point> intersections = wire1.Intersect(wire2, new PointEqualityComparer());

            int minSteps = intersections.Select(i =>
                wire1.First(w => w.X == i.X && w.Y == i.Y).Steps + wire2.First(w => w.X == i.X && w.Y == i.Y).Steps).Where(s => s != 0).Min();
        }

        private static void Part2()
        {
            (List<Point> wire1, List<Point> wire2) = GetWirePaths();
            IEnumerable<Point> intersections = wire1.Intersect(wire2, new PointEqualityComparer());

            int minSteps = intersections.Select(i =>
                wire1.First(w => w.X == i.X && w.Y == i.Y).Steps + wire2.First(w => w.X == i.X && w.Y == i.Y).Steps).Where(s => s != 0).Min();

            Console.WriteLine("Part 2:");
            Console.WriteLine($"Steps: {minSteps}");
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
            int steps = 0;
            points.Add(new Point(x, y, steps));

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
                            steps++;
                            points.Add(new Point(x, y, steps));
                        }

                        break;
                    }
                    case 'D':
                    {
                        foreach (int d in Enumerable.Range(0, distance))
                        {
                            y--;
                            steps++;
                            points.Add(new Point(x, y, steps));
                        }

                        break;
                    }
                    case 'L':
                    {
                        foreach (int d in Enumerable.Range(0, distance))
                        {
                            x--;
                            steps++;
                            points.Add(new Point(x, y, steps));
                        }

                        break;
                    }
                    case 'R':
                    {
                        foreach (int d in Enumerable.Range(0, distance))
                        {
                            x++;
                            steps++;
                            points.Add(new Point(x, y, steps));
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
            Steps = 0;
        }

        public Point(int x, int y, int steps)
        {
            X = x;
            Y = y;
            Steps = steps;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Steps { get; set; }

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