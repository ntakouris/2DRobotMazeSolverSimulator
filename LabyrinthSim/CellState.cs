using System;
namespace LabyrinthSim
{
    public static class CellState
    {
        public const char WALL = '#';
        public const char GOAL = 'G';
        public const char START = 'S';
        public const char EMPTY = '-';
        public const char ROBOT = '@';
        public const char MARK = 'x';
        public const char UNOBSERVED = '?';
    }

}
