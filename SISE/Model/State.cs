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

        public State Move(char direction)
        {
            State newState = new State(this);
            newState.move = direction;
            newState.moveSet += direction;
            newState.previousState = this;
            newState.depth = depth + 1;
            switch (direction)
            {
                case 'u':
                    Swap(ref newState.puzzle[zeroIndex.X, zeroIndex.Y], ref newState.puzzle[zeroIndex.X - 1, zeroIndex.Y]);
                    newState.zeroIndex.X--;
                    break;
                case 'd':
                    Swap(ref newState.puzzle[zeroIndex.X, zeroIndex.Y], ref newState.puzzle[zeroIndex.X + 1, zeroIndex.Y]);
                    newState.zeroIndex.X++;
                    break;
                case 'l':
                    Swap(ref newState.puzzle[zeroIndex.X, zeroIndex.Y], ref newState.puzzle[zeroIndex.X, zeroIndex.Y - 1]);
                    newState.zeroIndex.Y--;
                    break;
                case 'r':
                    Swap(ref newState.puzzle[zeroIndex.X, zeroIndex.Y], ref newState.puzzle[zeroIndex.X, zeroIndex.Y + 1]);
                    newState.zeroIndex.Y++;
                    break;
                default:
                    break;
            }
            return newState;
        }

        public void GenerateNextStates(string order)
        {
            foreach (char direction in order)
            {
                if (IsMoveAllowed(direction))
                {
                    nextStates.Add(Move(direction));
                }
            }
        }

        private bool IsMoveAllowed(char direction) => IsNotOutOfBound(direction) && IsNotTurningBack(direction);

        private bool IsNotOutOfBound(char direction)
        {
            return  !((!"udlr".Contains(direction.ToString())) ||
                    (zeroIndex.X == 0 && direction == 'u') ||
                    (zeroIndex.X == Height - 1 && direction == 'd') ||
                    (zeroIndex.Y == 0 && direction == 'l') ||
                    (zeroIndex.Y == Width - 1 && direction == 'r'));
        }
        private bool IsNotTurningBack(char direction) => 
                    !((move == 'l' && direction == 'r') ||
                     (this.move == 'u' && direction == 'd') ||
                     (this.move == 'r' && direction == 'l') ||
                     (this.move == 'd' && direction == 'u'));

        private void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp = lhs;
            lhs = rhs;
            rhs = temp;
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
