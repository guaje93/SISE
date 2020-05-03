using SISE.Model;
using System.Drawing;

namespace SISE.Logic
{
    public class MoveUp : IMoveable
    {
        #region Methods

        public State Move(State state, char direction)
        {
            State newState = new State(state);
            newState.Move = direction;
            newState.MoveSet += direction;
            newState.PreviousState = state;
            newState.Depth = state.Depth + 1;
            MoveZero(state, newState);
            return newState;
        }

        private void MoveZero(State state, State newState)
        {
            Swap(ref newState.Puzzle[state.ZeroIndex.X, state.ZeroIndex.Y], ref newState.Puzzle[state.ZeroIndex.X - 1, state.ZeroIndex.Y]);
            newState.ZeroIndex = new Point(newState.ZeroIndex.X - 1, newState.ZeroIndex.Y);
        }

        private void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp = lhs;
            lhs = rhs;
            rhs = temp;
        }

        #endregion
    }
}
