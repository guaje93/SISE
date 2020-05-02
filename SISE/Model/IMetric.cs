namespace SISE.Model
{
    public interface IMetric
    {
        int GetDistanceToSolution(State state);
    }
}