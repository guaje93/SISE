using SISE.Model;
using System.Drawing;

namespace SISE.Logic
{
    class Mediator
    {
        #region Fields

        IMoveable _moveUp;
        IMoveable _moveDown;
        IMoveable _moveLeft;
        IMoveable _moveRight;

        #endregion

        #region Constructor

        public Mediator()
        {
            _moveUp = new MoveUp();
            _moveDown = new MoveDown();
            _moveLeft = new MoveLeft();
            _moveRight = new MoveRight();
        }

        #endregion

        #region Properties

        public State Move(State state, char direction)
        {
            switch (direction)
            {
                case 'u':
                    return _moveUp.Move(state, direction: direction);
                case 'd':
                    return _moveDown.Move(state, direction: direction);
                case 'l':
                    return _moveLeft.Move(state, direction: direction);
                case 'r':
                    return _moveRight.Move(state, direction: direction);
                default:
                    return null;
            }
        }

        public bool IsMoveAllowed(State state, char direction) => IsNotOutOfBound(state.ZeroIndex, direction) && IsNotTurningBack(state.Move, direction);

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
        
        #endregion
    }
}
