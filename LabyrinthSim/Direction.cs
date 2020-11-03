using System;
using System.Collections.Generic;

namespace LabyrinthSim
{
    public class Direction
    {
        public int Value { get; set; }

        public static readonly Direction UP = new Direction { Value = 0 };
        public static readonly Direction LEFT = new Direction { Value = 1 };
        public static readonly Direction RIGHT = new Direction { Value = 2 };
        public static readonly Direction DOWN = new Direction { Value = 3 };

        public static IEnumerable<Direction> GetAllDirections()
        {
            return new[] { UP, DOWN, LEFT, RIGHT };
        }

        public static bool operator ==(Direction a, Direction b) => a.Value == b.Value;
        public static bool operator !=(Direction a, Direction b) => a.Value != b.Value;

        public void RotateRight()
        {
            Value = (Value + 1) % 4;
        }

        public Direction Copy()
        {
            return new Direction { Value = this.Value };
        }
    }

    public static class DirectionExtensions
    {
        public static int TurningDistanceTo(this Direction start, Direction end)
        {
            if (start == end)
            {
                return 0;
            }

            Direction _s = start.Copy();
            Direction _e = end.Copy();

            while (_s != Direction.UP)
            {
                _s.RotateRight();
                _e.RotateRight();
            }

            if (_e == Direction.LEFT || _e == Direction.RIGHT)
            {
                return 1;
            }

            return 2;
        }
    }
}
