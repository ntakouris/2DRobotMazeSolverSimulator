using System;
using System.Collections;
using System.IO;

namespace LabyrinthSim
{
    public class Grid
    {
        internal char[,] Tiles { get; set; }
        public string Name { get; set; }

        public Grid() { }
        public Grid(string filename)
        {
            Name = filename;

            var lineList = new ArrayList();
            var lineSize = 0;
            var totalLines = 0;
            using (var file = File.OpenText(filename))
            {
                var lines = file.ReadToEnd().Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in lines)
                {
                    totalLines++;
                    var chars = line.Trim().ToCharArray();
                    lineSize = chars.Length;
                    lineList.Add(chars);
                }
            }
            Console.WriteLine($"Loading grid from {filename}. {totalLines} lines in total, with {lineSize} tiles each.");
            Tiles = new char[totalLines, lineSize];

            for (var i = 0; i < totalLines; i++)
            {
                var line = (char[])lineList[i];

                for (var j = 0; j < lineSize; j++)
                {
                    var c = line[j];
                    Tiles[i, j] = c;
                }
            }
        }

        public char this[int i, int j]
        {
            get
            {
                return Tiles[i, j];
            }

            set
            {
                Tiles[i, j] = value;
            }
        }

        public char this[Point p]
        {
            get
            {
                return Tiles[p.X, p.Y];
            }

            set
            {
                Tiles[p.X, p.Y] = value;
            }
        }

        public void WriteToConsole()
        {
            this.GetMaxDims(out var maxI, out var maxJ);

            for(var i = 0; i < Tiles.GetLength(0); i++)
            {
                for(var j = 0; j < Tiles.GetLength(1); j++)
                {
                    Console.Write(Tiles[i, j]);
                }
                Console.WriteLine();
            }
        }

        public override string ToString()
        {
            return $"Grid({Name})";
        }
    }

    public static class GridExtensions
    {
        public static void GetMaxDims(this Grid grid, out int MaxI, out int MaxJ)
        {
            MaxI = grid.Tiles.GetLength(0);
            MaxJ = grid.Tiles.GetLength(1);
        }
    }
}
