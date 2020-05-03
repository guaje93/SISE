using System.Collections.Generic;
using SISE.Model;
using System.Linq;

namespace SISE.Logic
{
    public class BreadthFirstSearchSolver : ISolver
    {
        #region Properties
        
        public State Solved { get; }
        public State InitialState { get; private set; }
        public int MaxDepth { get; private set; }
        public string SearchOrder { get; private set; }
        public int StatesVisitedAmount { get; private set; }
        public int StatesProcessedAmount { get; private set; }

        #endregion

        #region Constructors

        public BreadthFirstSearchSolver(State _initialState, string _neighborhoodSearchOrder, State _solved)
        {
            MaxDepth = int.MinValue;
            InitialState = _initialState;
            SearchOrder = _neighborhoodSearchOrder.ToLower();
            Solved = _solved;
        }

        #endregion

        #region Methods

        public string Solve()
        {
            Queue<State> toVisit = new Queue<State>();
            Queue<State> visited = new Queue<State>();
            string solutionString = "";
            bool solutionFound = false;

            toVisit.Enqueue(InitialState);

            while (toVisit.Count > 0 )
            {
                State currentState = toVisit.Dequeue();
                visited.Enqueue(currentState);

                if (currentState.Depth > MaxDepth)
                    MaxDepth = currentState.Depth;

                //Console.WriteLine(currentState);
                //Console.WriteLine("\n");

                if ((this as ISolver).IsPuzzleSolution(currentState, Solved))
                {
                    solutionString = currentState.MoveSet;
                    solutionFound = true;
                    break;
                }
                else
                {
                    currentState.GenerateNextStates(SearchOrder);

                    foreach (State state in currentState.NextStates)
                    {
                        if (!toVisit.Concat(visited).Any(p => p.Equals(state)))
                        {
                            toVisit.Enqueue(state);
                        }
                    }
                }
            }

            StatesVisitedAmount = visited.Count() + toVisit.Count();
            StatesProcessedAmount = visited.Count();

            return solutionFound ? solutionString : "No solution found!";
        }

        #endregion
    }
}