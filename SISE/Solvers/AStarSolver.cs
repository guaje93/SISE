using SISE.Helpers;
using SISE.Model;
using SISE.Solution;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SISE
{
    public class AStarSolver : ISolver
    {
        #region Fields

        private readonly PriorityQueue<State> _priorityQueue = new PriorityQueue<State>();
        private readonly IMetric _metric;

        #endregion

        #region Properties

        public State Solved { get; }
        public State InitialState { get; private set; }
        public int MaxDepth { get; private set; }
        public int StatesVisitedAmount { get; private set; }
        public int StatesProcessedAmount { get; private set; }

        #endregion

        #region Constructors

        public AStarSolver(State _initialState, IMetric metric , State _solved)
        {
            MaxDepth = int.MinValue;
            this._metric = metric;
            InitialState = _initialState;
            Solved = _solved;
        }

        #endregion

        #region Methods

        public string Solve()
        {
            bool solutionFound = false;
            string solutionString = "";
            List<State> visitedStates = new List<State>();
            StatesProcessedAmount = 0;
            State currentState;
            _priorityQueue.Enqueue(InitialState);

            while (_priorityQueue.Count() > 0)
            {
                currentState = _priorityQueue.Dequeue();
                if (visitedStates.Any())
                {
                    while(visitedStates.Any(p=>p.Equals(currentState)))
                        currentState = _priorityQueue.Dequeue();
                }

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

                currentState.GenerateNextStates("lurd");
                foreach (State nextState in currentState.NextStates)
                {
                    StatesProcessedAmount++;
                    int heuresticValue = _metric.GetDistanceFromSolution(nextState) + nextState.Depth;
                    _priorityQueue.Enqueue(nextState, heuresticValue);
                }
                visitedStates.Add(currentState);
            }

            StatesVisitedAmount = visitedStates.Count();
            return solutionFound ? solutionString : "No solution found!";
        }

        #endregion
    }
}