using SISE.Model;

namespace SISE.Logic

{
    public interface IMoveable
    {
        State Move(State state, char direction);
    }
}
