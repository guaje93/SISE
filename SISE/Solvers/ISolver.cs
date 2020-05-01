using System;
using System.Collections.Generic;
using System.Text;

namespace SISE.Solution
{
    public interface ISolver
    {
        string Solve();

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
    }
}
