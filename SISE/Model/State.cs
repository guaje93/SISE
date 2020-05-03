using SISE.Model.Move;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SISE
{
    public class State
    {
        public State previousState;
        public List<State> nextStates;
        public int[,] puzzle;
        public char move;
        public String moveSet;
        public static int Width { get; private set; }
        public static int Height { get; private set; }
        public Point zeroIndex;
        public int depth = 0;
        private readonly Mediator _mediator = new Mediator();

        public State(int[,] p, Point point, Tuple<int, int> matrix)
        {
            Width = matrix.Item1;
            Height = matrix.Item2;
            nextStates = new List<State>();
            puzzle = new int[Height, Width];
            zeroIndex = point;
            Array.Copy(p, puzzle, p.Length);
            zeroIndex = point;
            moveSet = "";
        }
        public State(int[,] p, Point point)
        {
            nextStates = new List<State>();
            puzzle = new int[Height, Width];
            zeroIndex = point;
            Array.Copy(p, puzzle, p.Length);
            zeroIndex = point;
            moveSet = "";
        }
        public State(State state)
        {
            puzzle = new int[State.Height, State.Width];
            nextStates = new List<State>();
            Array.Copy(state.puzzle, puzzle, state.puzzle.Length);
            zeroIndex = new Point(state.zeroIndex.X, state.zeroIndex.Y);
            moveSet = state.moveSet;
        }

        public void GenerateNextStates(string order)
        {
            foreach (char direction in order)
            {
                if (_mediator.IsMoveAllowed(this,direction))
                {
                    nextStates.Add(_mediator.Move(this,direction));
                }
            }
        }

        public override string ToString()
        {
            var text = new StringBuilder();
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                    text.Append(puzzle[i, j] + " ");
                text.Append("\n");
            }

            return text.ToString();
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

        public override int GetHashCode()
        {
            return HashCode.Combine(puzzle);
        }
    }
}
