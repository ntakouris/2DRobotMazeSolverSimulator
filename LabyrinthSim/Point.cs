using System;
using System.Runtime.CompilerServices;

namespace LabyrinthSim
{
    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public static bool operator ==(Point a, Point b) => a.X == b.X && a.Y == b.Y;

        public static bool operator !=(Point a, Point b) => !(a == b);

        public static Point operator +(Point p, Direction d)
        {
            Point ret = new Point { X = p.X, Y = p.Y };

            if (d == Direction.UP)
            {
                ret.Y++;
            }

            if (d == Direction.DOWN)
            {
                ret.Y--;
            }

            if(d == Direction.RIGHT)
            {
                ret.X++;
            }

            if (d == Direction.LEFT)
            {
                ret.X--;
            }

            return ret;
        }

        public override string ToString()
        {
            return $"Point({X},{Y})";
        }

        internal void Deconstruct(out int i, out int j)
        {
            i = X;
            j = Y;
        }
    }

    public static class PointExtensions
    {
        public static int ManhattanDistanceTo(this Point p1, Point p2)
        {
            return Math.Abs(p1.X - p2.X) + Math.Abs(p1.Y - p2.Y);
        }

        public static Direction PairwiseDirectionTo(this Point p1, Point p2)
        {
            if (p1.X == p1.X)
            {
                if (p1.Y >= p2.Y)
                {
                    return Direction.UP;
                }

                return Direction.DOWN;
            }

            if (p1.X >= p2.X)
            {
                return Direction.LEFT;
            }

            return Direction.RIGHT;
        }
    }
}
