using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LabyrinthSim
{
    public class Robot
    {
        public Func<Direction, Point, Point, float> PairwiseCostFunc { get; set; }
        public Labyrinth Labyrinth { get; set; }

        private HashSet<Point> Visited { get; set; } = new();
        private Stack<Point> SolutionPath { get; set; } = new();
        private Collection<Point> ExplorationPath { get; set; } = new();

        private float ExplorationCost { get; set; }
        private Stack<float> SolutionCost { get; set; } = new();

        public ExplorationResult Explore()
        {
            Point initialPosition = Labyrinth.GetInitialRobotPosition();
            Console.WriteLine($"Robot starts at initial position {initialPosition}");
            SolutionCost.Push(0);
            (var res, var _) = DfsUtil(initialPosition, null);

            return new ExplorationResult {
                Success = res,
                ExplorationPath = ExplorationPath,
                ExplorationCost = ExplorationCost,
                SolutionCost = SolutionCost.Sum(),
                SolutionPath = SolutionPath
            };
        }

        private (bool, Point) DfsUtil(Point parent, Point? from = null)
        {
            if (!Visited.Add(parent))
            {
                return (false, parent);
            }

            // add to current running paths
            ExplorationPath.Add(parent);
            SolutionPath.Push(parent);

            // if found goal, bubble up the solution
            if (Labyrinth.Grid[parent] == CellState.GOAL)
            {
                return (true, parent);
            }

            // for all non-visited adjacent cells
            foreach (var direction in Direction.GetAllDirections())
            {
                if (Labyrinth.TryGetPoint(parent, direction, out var pt))
                {
                    if (!Visited.Contains(pt))
                    {
                        // Console.WriteLine($"Going from {parent} -> {pt}");
                        if (from.HasValue)
                        {
                            var dir = from.Value.PairwiseDirectionTo(pt);
                            var visitingCost = PairwiseCostFunc(dir, parent, pt);
                            ExplorationCost += visitingCost;

                            SolutionCost.Push(visitingCost);
                        }
                        
                        (var res, var justExplored) = DfsUtil(pt, parent);

                        // if found, return current state
                        if (res)
                        {
                            // Console.WriteLine("Goal found");
                            return (true, parent);
                        }

                        // if not found, backtrack and
                        // keep track of robot pathing (including costs)
                        // Console.WriteLine($"Going from {justExplored} -> {parent}");

                        var previousDirection = justExplored.PairwiseDirectionTo(parent);
                        var backwardsCost = PairwiseCostFunc(previousDirection, justExplored, parent);

                        ExplorationCost += backwardsCost;
                        ExplorationPath.Add(parent);

                        SolutionPath.Pop();
                        SolutionCost.Pop();
                    }
                }
            }

            return (false, parent);
        }
    }
}
