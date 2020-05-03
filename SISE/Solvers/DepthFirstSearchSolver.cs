using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SISE.Solution
{
    public class DepthFirstSearchSolver : ISolver
    {
        #region Fields

        private readonly int _maxPossibleDepth = 20;

        #endregion

        #region Properties

        public State Solved { get; }
        public State InitialState { get; private set; }
        public int MaxDepth { get; private set; }
        public string SearchOrder { get; private set; }
        public int StatesProcessedAmount { get; private set; }
        public int StatesVisitedAmount { get; private set; }

        #endregion

        #region Constructors

        public DepthFirstSearchSolver(State _initialState, string _neighborhoodSearchOrder, State _solved)
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
            bool solutionFound = false;
            string solutionString = "";

            Stack<State> visited = new Stack<State>();
            Stack<State> toVisit = new Stack<State>();
            toVisit.Push(InitialState);
            StatesVisitedAmount = 1;
            while (toVisit.Count != 0 )
            {
                State currentState = toVisit.Pop();

                if (currentState.Depth > MaxDepth)
                    MaxDepth = currentState.Depth;
                
//                Console.WriteLine(currentState);
//                Console.WriteLine("\n");

                if ((this as ISolver).IsPuzzleSolution(currentState, Solved))
                {
                    solutionFound = true;
                    solutionString = currentState.MoveSet;
                    break;
                }
                else
                {
                    if (currentState.Depth > _maxPossibleDepth)
                    {
                        continue;
                    }
                    visited.Push(currentState);
                    currentState.GenerateNextStates(SearchOrder);
                    List<State> nextStates = currentState.NextStates;
                    nextStates.Reverse();
                    foreach (State state in currentState.NextStates)
                    {
                        if (!toVisit.Concat(visited).Any(p => p.Equals(state)))
                        {
                            toVisit.Push(state);
                        }
                    }
                }
            }

            StatesProcessedAmount = visited.Count;

            return solutionFound == true ? solutionString : "No solution found!";

        }

        #endregion
    }
}
