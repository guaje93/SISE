using SISE.Solution;

namespace SISE
{
    public class AStarSolver : ISolver
    {
        private State state;
        private string v;
        private State _solvedState;

        public AStarSolver(State state, string v, State solvedState)
        {
            this.state = state;
            this.v = v;
            _solvedState = solvedState;
        }

        public int MaxDepth => throw new System.NotImplementedException();

        public int NumberOfVisitedStates => throw new System.NotImplementedException();

        public int NumberOfProcessedStates => throw new System.NotImplementedException();

        public string Solve()
        {
            throw new System.NotImplementedException();
        }
    }
}