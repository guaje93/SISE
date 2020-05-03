using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SISE.Model.Move
{
    class Mediator
    {
        IMoveable MoveUp;
        IMoveable MoveDown;
        IMoveable MoveLeft;
        IMoveable MoveRight;

        public Mediator()
        {
            MoveUp = new MoveUp();
            MoveDown = new MoveDown();
            MoveLeft = new MoveLeft();
            MoveRight = new MoveRight();
        }
        public State Move(State state, char direction)
        {
            switch (direction)
            {
                case 'u':
                    return MoveUp.Move(state, direction: direction);
                case 'd':
                    return MoveDown.Move(state, direction: direction);
                case 'l':
                    return MoveLeft.Move(state, direction: direction);
                case 'r':
                    return MoveRight.Move(state, direction: direction);
                default:
                    return null;
            }
        }

        public bool IsMoveAllowed(State state, char direction) => IsNotOutOfBound(state.zeroIndex, direction) && IsNotTurningBack(state.move, direction);

        private bool IsNotOutOfBound(Point zeroIndex, char direction)
        {
            return !((!"udlr".Contains(direction.ToString())) ||
                    (zeroIndex.X == 0 && direction == 'u') ||
                    (zeroIndex.X == State.Height - 1 && direction == 'd') ||
                    (zeroIndex.Y == 0 && direction == 'l') ||
                    (zeroIndex.Y == State.Width - 1 && direction == 'r'));
        }
        private bool IsNotTurningBack(char move, char direction) =>
                    !((move == 'l' && direction == 'r') ||
                     (move == 'u' && direction == 'd') ||
                     (move == 'r' && direction == 'l') ||
                     (move == 'd' && direction == 'u'));
    }
}
