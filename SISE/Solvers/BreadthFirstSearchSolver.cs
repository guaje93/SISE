using SISE.Solution;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SISE
{
    public class BreadthFirstSearchSolver : ISolver
    {
        public State Solved { get; }
        public BreadthFirstSearchSolver(State _initialState, string _neighborhoodSearchOrder, State _solved)
        {
            MaxDepth = int.MinValue;
            SolutionState = _initialState;
            NeighborhoodSearchOrder = _neighborhoodSearchOrder.ToLower();
            Solved = _solved;
        }
        public int MaxDepth { get; private set; }
        public State SolutionState { get; private set; }
        public string NeighborhoodSearchOrder { get; private set; }
        public int NumberOfVisitedStates { get; private set; }
        public int NumberOfProcessedStates { get; private set; }
        public string Solve()
        {
            Queue<State> toVisit = new Queue<State>();
            Queue<State> visited = new Queue<State>();
            string solutionString = "";
            bool solutionFound = false;

            toVisit.Enqueue(SolutionState);

            while (toVisit.Count > 0)
            {
                State currentState = toVisit.Dequeue();
                visited.Enqueue(currentState);

                if (currentState.depth > MaxDepth)
                    MaxDepth = currentState.depth;

                if ((this as ISolver).IsPuzzleSolution(currentState, Solved))
                {
                    solutionString = currentState.moveSet;
                    solutionFound = true;
                }
                else
                {
                    currentState.GenerateNextStates(NeighborhoodSearchOrder);

                    foreach (State state in currentState.nextStates)
                    {
                        if (toVisit.Concat(visited).All(p => !p.Equals(state)))
                        {
                            toVisit.Enqueue(state);
                        }
                    }
                }
            }

            NumberOfVisitedStates = visited.Count() + toVisit.Count();
            NumberOfProcessedStates = visited.Count();

            return solutionFound ? solutionString : "No solution found!";
        }
    }
}