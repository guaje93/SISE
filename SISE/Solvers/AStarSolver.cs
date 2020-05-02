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

        public int MaxDepth { get; private set; }
        public State SolutionState { get; private set; }
        public int NumberOfVisitedStates { get; private set; }
        public int NumberOfProcessedStates { get; private set; }
        public State Solved { get; }
        private PriorityQueue<State> priorityQueue;
        IMetric metric;

        public AStarSolver(State _initialState, IMetric metric , State _solved)
        {
            MaxDepth = int.MinValue;
            this.metric = metric;
            SolutionState = _initialState;
            Solved = _solved;
        }

        public string Solve()
        {
            priorityQueue = new PriorityQueue<State>();
            bool solutionFound = false;
            string solutionString = "";
            List<State> visitedStates = new List<State>();
            NumberOfProcessedStates = 0;
            State currentState;
            priorityQueue.Enqueue(SolutionState);

            while (priorityQueue.Count > 0)
            {
                currentState = priorityQueue.Dequeue();
                if (visitedStates.Any())
                {
                    if(visitedStates.All(p=>!p.Equals(currentState)))
                        currentState = priorityQueue.Dequeue();
                }

                if (currentState.depth > MaxDepth)
                    MaxDepth = currentState.depth;

                if ((this as ISolver).IsPuzzleSolution(currentState, Solved))
                {
                    solutionString = currentState.moveSet;
                    solutionFound = true;
                    break;
                }

                currentState.GenerateNextStates("lurd");
                foreach (State nextState in currentState.nextStates)
                {
                    NumberOfProcessedStates++;
                    int heuresticValue = metric.GetDistanceToSolution(nextState) + nextState.depth;
                    priorityQueue.Enqueue(nextState, heuresticValue);
                }
                visitedStates.Add(currentState);
            Console.WriteLine(currentState);
            Console.WriteLine("\n");
            }

            NumberOfVisitedStates = visitedStates.Count();
            return solutionFound ? solutionString : "No solution found!";
        }
    }
}