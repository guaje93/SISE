using System.Collections.Generic;
using System.Linq;
using SISE.Model;

namespace SISE.Logic
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
