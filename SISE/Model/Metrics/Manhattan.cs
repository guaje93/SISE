using System;
using System.Collections.Generic;
using System.Text;

namespace SISE.Model
{
    class Manhattan : IMetric
    {
        #region Methods

        public int GetDistanceFromSolution(State from)
        {
            int distance = 0;
            int solutionValue = 1;
            for (int i = 0; i < State.Width; i++)
            {
                for (int j = 0; j < State.Height; j++)
                {
                    int value = from.Puzzle[i, j];
                    if (i == State.Height - 1 && j == State.Width - 1)
                    {
                        solutionValue = 0;
                    }
                    if (value != 0 && solutionValue != 0)
                    {
                        distance += Math.Abs(solutionValue - value);
                    }
                    solutionValue++;
                }
            }

            return distance;
        }

        #endregion
        
    }
}
