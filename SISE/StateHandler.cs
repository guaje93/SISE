using System;
using System.Collections.Generic;
using System.Text;

namespace SISE
{
    class StateHandler
    {
        public State PreviousState { get; set; }
        public State CurrentState { get; set; }
        public List<State> NextStates { get; set; }

        public char PreviousMove { get; set; }

        public bool IsMoveAllowed(char destination) => IsNotOutOfBound(destination) && IsNotTurningBack(destination);

        private bool IsNotTurningBack(char destinantion)
        {
            if ((this.PreviousMove == 'l' && destinantion == 'r') ||
                           (this.PreviousMove == 'u' && destinantion == 'd') ||
                           (this.PreviousMove == 'r' && destinantion == 'l') ||
                           (this.PreviousMove == 'd' && destinantion == 'u'))
            {
                return false;
            }
            return true;
        }

        public void GenerateNextStates(string order)
        {
            foreach (char direction in order)
            {
                if (IsMoveAllowed(direction))
                {
                    this.NextStates.Add(this.Move(direction));
                }
            }
        }

        private State Move(char direction)
        {
            throw new NotImplementedException();
        }

        private bool IsNotOutOfBound(char destination)
        {
            if ((!"udlr".Contains(destination.ToString())) ||
                            (CurrentState.zeroIndex.X == 0 && destination == 'u') ||
                            (CurrentState.zeroIndex.X == State.Height - 1 && destination == 'd') ||
                            (CurrentState.zeroIndex.Y == 0 && destination == 'l') ||
                            (CurrentState.zeroIndex.Y == State.Width - 1 && destination == 'r'))
            {
                return false;
            }
            return true;
        }
    }
}
