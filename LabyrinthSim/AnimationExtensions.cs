using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace LabyrinthSim
{
    public static class AnimationExtensions
    {
        public static void Animate(this IEnumerable<Point> path, Grid grid, string animationTitle)
        {
            Console.WriteLine($"Press any key to start animation \"{animationTitle}\"");
            Console.ReadKey();

            grid.GetMaxDims(out var iMax, out var jMax);

            var arr = grid.Tiles.Clone() as char[,];
            Console.Clear();
            // Console.SetBufferSize(iMax, jMax);
            // Console.SetWindowSize(iMax * 2, jMax * 2);

            var left = Console.WindowLeft;
            var top = Console.WindowTop;

            foreach (var point in path)
            {
                Console.SetCursorPosition(left, top);
                for (var i = 0; i < iMax; i++)
                {
                    for (var j = 0; j < jMax; j++)
                    {
                        (var pi, var pj) = point;

                        if (i == pi && j == pj)
                        {
                            if (arr[i,j] != CellState.GOAL || arr[i,j] != CellState.START) {
                                arr[i, j] = CellState.MARK;
                            }
                            Console.Write(CellState.ROBOT);
                            continue;
                        }
                        
                        Console.Write(arr[i, j]);
                    }
                    Console.Write('\n');
                }
                Thread.Sleep(100);
            }

        }
    }
}
