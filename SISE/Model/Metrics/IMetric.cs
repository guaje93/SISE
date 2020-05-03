namespace SISE.Model
{
    public interface IMetric
    {
        int GetDistanceFromSolution(State state);
    }
}