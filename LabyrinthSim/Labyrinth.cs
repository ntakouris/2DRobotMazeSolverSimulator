using System;
using System.Collections.Generic;
using System.Linq;

namespace LabyrinthSim
{
    public class Labyrinth
    {
        public Grid Grid { get; set; }

        public Point GetInitialRobotPosition()
        {
            Grid.GetMaxDims(out var maxX, out var maxY);
            for (var i = 0; i < maxX; i++)
            {
                for (var j = 0; j < maxY; j++)
                {
                    if (Grid[i, j] == CellState.START)
                    {
                        return new Point { X = i, Y = j };
                    }
                }
            }

            throw new KeyNotFoundException($"Start position with symbol {CellState.START} not found in grid {Grid}");
        }

        internal bool TryGetPoint(Point current, Direction direction, out Point pt)
        {
            pt = current + direction;

            Grid.GetMaxDims(out var maxX, out var maxY);

            // Console.Write($"{current} -> {pt}: ");
            if (pt.X < 0 || pt.Y < 0 || pt.X >= maxX || pt.Y >= maxY)
            {
                // Console.WriteLine(" NOT Accessible");
                return false;
            }

            var blockedPaths = new[] { CellState.WALL };
            if (blockedPaths.Contains(Grid[pt]))
            {
                // Console.WriteLine(" NOT Accessible");
                return false;
            }
            // Console.WriteLine(" Accessible");
            return true;
        }
    }
}
