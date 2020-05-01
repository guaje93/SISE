using System;
using System.Collections.Generic;
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
        public int numberOfVisitedStates;
        public int numberOfProcessedStates;
        public int MaxDepth { get; private set; }
        public State SolutionState { get; private set; }
        public string NeighborhoodSearchOrder { get; private set; }

        public string Solve()
        {
            bool solutionFound = false;

            Dictionary<State, State> visitedStates = new Dictionary<State, State>();
            string solutionString = "";
            Stack<State> toVisit = new Stack<State>();
            toVisit.Push(SolutionState);
            numberOfVisitedStates = 1;
            while (toVisit.Count != 0 && !solutionFound)
            {
                State currentState = toVisit.Pop();
                if (currentState.depth > MaxDepth)
                    MaxDepth = currentState.depth;
                if (IsSolution(currentState))
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

                    if (visitedStates.ContainsKey(currentState))
                    {
                        if (currentState.depth >= visitedStates[currentState].depth)
                        {
                            continue;
                        }
                        else
                        {
                            visitedStates.Remove(currentState);
                        }
                    }

                    visitedStates.Add(currentState, currentState);
                    currentState.GenerateNextStates(NeighborhoodSearchOrder);
                    List<State> nextStates = currentState.nextStates;
                    nextStates.Reverse();
                    foreach (State s in nextStates)
                    {
                        toVisit.Push(s);
                        numberOfVisitedStates++;
                    }
                }
                Console.WriteLine(currentState);
                Console.WriteLine("\n");
            }

            numberOfProcessedStates = visitedStates.Count;

            return solutionFound == true ? solutionString : "No solution found!";

        }

        private bool IsSolution(State currentState) => currentState.Equals(Solved);
    }
}
