using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SISE.Model.Move
{
    public interface IMoveable
    {
        State Move(State state, char direction);
    }
}
