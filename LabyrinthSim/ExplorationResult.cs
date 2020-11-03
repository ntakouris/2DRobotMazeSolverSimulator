using System;
using System.Collections.Generic;

namespace LabyrinthSim
{
    public class ExplorationResult
    {
        public bool Success { get; set; }
        public float ExplorationCost { get; set; }
        public float SolutionCost { get; set; }
        public IEnumerable<Point> SolutionPath { get; set; }
        public IEnumerable<Point> ExplorationPath { get; set; }
    }
}
