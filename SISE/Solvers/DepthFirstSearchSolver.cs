using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SISE.Solution
{
    public class DepthFirstSearchSolver : ISolver
    {
        readonly int maxPossibleDepth = 20;
        public State Solved { get; }
        public DepthFirstSearchSolver(State _initialState, string _neighborhoodSearchOrder, State _solved)
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
            bool solutionFound = false;

            Stack<State> visited = new Stack<State>();
            string solutionString = "";
            Stack<State> toVisit = new Stack<State>();
            toVisit.Push(SolutionState);
            NumberOfVisitedStates = 1;
            while (toVisit.Count != 0 && !solutionFound)
            {
                State currentState = toVisit.Pop();

                if (currentState.depth > MaxDepth)
                    MaxDepth = currentState.depth;
                
                if ((this as ISolver).IsPuzzleSolution(currentState, Solved))
                {
                    solutionFound = true;
                    solutionString = currentState.moveSet;
                }
                else
                {
                    if (currentState.depth > maxPossibleDepth)
                    {
                        continue;
                    }
                    visited.Push(currentState);
                    currentState.GenerateNextStates(NeighborhoodSearchOrder);
                    List<State> nextStates = currentState.nextStates;
                    nextStates.Reverse();
                    foreach (State state in currentState.nextStates)
                    {
                        if (toVisit.Concat(visited).All(p => !p.Equals(state)))
                        {
                            toVisit.Push(state);
                        }
                    }
                }
                Console.WriteLine(currentState);
                Console.WriteLine("\n");
            }

            NumberOfProcessedStates = visited.Count;

            return solutionFound == true ? solutionString : "No solution found!";

        }

    }
}
