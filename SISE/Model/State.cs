using SISE.Model.Move;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SISE
{
    public class State
    {
        #region Fields

        private readonly Mediator _mediator = new Mediator();

        #endregion

        #region Static Properties

        public static int Height { get; private set; }
        public static int Width { get; private set; }
        
        #endregion

        #region Properties

        public State PreviousState { get; set; }
        public List<State> NextStates { get; set; }
        public int[,] Puzzle { get; set; }
        public char Move { get; set; }
        public string MoveSet { get; set; }
        public Point ZeroIndex { get; set; }
        public int Depth { get; set; } = 0;

        #endregion

        #region Constructors

        public State(State state)
        {
            Puzzle = new int[State.Height, State.Width];
            NextStates = new List<State>();
            Array.Copy(state.Puzzle, Puzzle, state.Puzzle.Length);
            ZeroIndex = new Point(state.ZeroIndex.X, state.ZeroIndex.Y);
            MoveSet = state.MoveSet;
        }
        public State(int[,] p, Point point)
        {
            NextStates = new List<State>();
            Puzzle = new int[Height, Width];
            ZeroIndex = point;
            Array.Copy(p, Puzzle, p.Length);
            ZeroIndex = point;
            MoveSet = "";
        }
        public State(int[,] p, Point point, Tuple<int, int> matrix)
        {
            Width = matrix.Item1;
            Height = matrix.Item2;
            NextStates = new List<State>();
            Puzzle = new int[Height, Width];
            ZeroIndex = point;
            Array.Copy(p, Puzzle, p.Length);
            ZeroIndex = point;
            MoveSet = "";
        }

        #endregion

        #region Methods

        public void GenerateNextStates(string order)
        {
            foreach (char direction in order)
            {
                if (_mediator.IsMoveAllowed(this,direction))
                {
                    NextStates.Add(_mediator.Move(this,direction));
                }
            }
        }

        #endregion

        #region Override Methods

        public override string ToString()
        {
            var text = new StringBuilder();
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                    text.Append(Puzzle[i, j] + " ");
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
                    if (this.Puzzle[i, j] != (obj as State).Puzzle[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Puzzle);
        }

        #endregion
    }
}
