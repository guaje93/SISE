using System;
using System.Collections.Generic;
using System.Text;

namespace SISE.Solution
{
    public interface ISolver
    {
        string Solve();
        int MaxDepth { get; }
        int NumberOfVisitedStates { get; }
        int NumberOfProcessedStates { get; }
        public bool IsPuzzleStateNew(IEnumerable<State> enumerable, State newState)
        {
            foreach (State state in enumerable)
            {
                if (state.Equals(newState))
                {
                    return false;
                }
            }
            return true;
        }
        public bool IsSolution(State currentState, State solution) => currentState.Equals(solution);

    }
}
