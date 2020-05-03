namespace SISE.Model
{
    class Hamming : IMetric
    {
        #region Methods

        public int GetDistanceFromSolution(State from)
        {
            int distance = 0;
            for (int i = 0; i < State.Height; i++)
            {
                for (int j = 0; j < State.Width; j++)
                {
                    int expectedValue;
                    if (i == State.Height - 1 && j == State.Width - 1)
                    {
                        expectedValue = 0;
                    }
                    else
                    {
                        expectedValue = i * State.Height + j + 1;
                    }
                    if (from.Puzzle[i, j] != expectedValue)
                    {
                        distance++;
                    }
                }
            }
            return distance;
        }

        #endregion
    }
}
