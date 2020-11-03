using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace LabyrinthSim
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //var testGrid = new char[,]
            //{   
            //    { CellState.EMPTY, CellState.WALL, CellState.WALL,CellState.WALL },       
            //    { CellState.EMPTY, CellState.EMPTY, CellState.GOAL,CellState.WALL }, 
            //    { CellState.EMPTY, CellState.WALL, CellState.WALL,CellState.EMPTY }, 
            //    { CellState.EMPTY, CellState.EMPTY, CellState.WALL,CellState.EMPTY },
            //    { CellState.WALL, CellState.START, CellState.EMPTY,CellState.EMPTY },  
            //};

            //var grid = new Grid { Name = "TestGrid", Tiles = testGrid };
            var grid = new Grid(@"./../../Grids/Test.txt");
            Console.WriteLine("Running on grid: ");
            grid.WriteToConsole();
            var labyrinth = new Labyrinth { Grid = grid };

            static float PairwiseTurnAndForwardCost(Direction dir, Point p1, Point p2)
            {
                var pointDirection = p1.PairwiseDirectionTo(p2);
                var turnTimes = dir.TurningDistanceTo(pointDirection);

                var pointDist = p1.ManhattanDistanceTo(p2);

                return turnTimes * 0.5f + pointDist;
            }

            var robot = new Robot { PairwiseCostFunc = PairwiseTurnAndForwardCost, Labyrinth = labyrinth };

            var navResult = robot.Explore();

            Console.WriteLine($"Finished exploration. Exploration cost: {navResult.ExplorationCost}");

            Console.WriteLine("Exploration Path: ");
            foreach (var pt in navResult.ExplorationPath)
            {
                Console.Write($"({pt.X} , {pt.Y}) ->");
            }

            Console.WriteLine();

            if (!navResult.Success)
            {
                Console.WriteLine("Explored all accessible cells, found no target symbol");
                navResult.ExplorationPath.Animate(labyrinth.Grid, "Exploration Path");
                return;
            }

            Console.WriteLine($"Found solution path. Solution cost: {navResult.SolutionCost}");
            Console.WriteLine("Solution Path: ");
            foreach (var pt in navResult.SolutionPath.Reverse())
            {
                Console.Write($"({pt.X} , {pt.Y}) ->");
            }

            Console.WriteLine();
            navResult.SolutionPath.Reverse().Animate(labyrinth.Grid, "Solution Path");

            navResult.ExplorationPath.Animate(labyrinth.Grid, "Exploration Path");
        }
    }
}
