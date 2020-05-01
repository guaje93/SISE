using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SISE
{
    class State
    {
        public int[,] puzzle;

        public static int Width { get; }
        public static int Height { get; }
        public Point zeroIndex;

        static State()
        {
            var matrixSize = Loader.GetMatrixSize();
            Width = matrixSize.Item1;
            Height = matrixSize.Item2;
        }



        public override bool Equals(object obj)
        {
            for (int i = 0; i < State.Height; i++)
            {
                for (int j = 0; j < State.Width; j++)
                {
                    if (this.puzzle[i, j] != (obj as State).puzzle[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public override string ToString()
        {
            var text = new StringBuilder();
            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                    text.Append(puzzle[i, j] + " ");

            return text.ToString();
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(puzzle);
        }
    }
}
