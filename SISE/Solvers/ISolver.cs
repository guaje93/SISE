using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SISE.Solution
{
    public interface ISolver
    {
        string Solve();
        int MaxDepth { get; }
        int StatesVisitedAmount { get; }
        int StatesProcessedAmount { get; }
        public bool DoesPuzzleNotReply(IEnumerable<State> enumerable, State newState) => !enumerable.Any(p => p.Equals(newState));
        public bool IsPuzzleSolution(State currentState, State solution) => currentState.Equals(solution);

    }
}
