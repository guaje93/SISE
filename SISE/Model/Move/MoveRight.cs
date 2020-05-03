﻿
using System;
using System.Collections.Generic;
using System.Text;

namespace SISE.Model.Move
{
    public class MoveRight : IMoveable
    {
        public State Move(State state, char direction)
        {
            State newState = new State(state);
            newState.move = direction;
            newState.moveSet += direction;
            newState.previousState = state;
            newState.depth = state.depth + 1;
            MoveZero(state, newState);
            return newState;
        }

        private void MoveZero(State state, State newState)
        {
            Swap(ref newState.puzzle[state.zeroIndex.X, state.zeroIndex.Y], ref newState.puzzle[state.zeroIndex.X, state.zeroIndex.Y + 1]);
            newState.zeroIndex.Y++;
        }

        private void Swap(ref int lhs, ref int rhs)
        {
            int temp = lhs;
            lhs = rhs;
            rhs = temp;
        }
    }
}
