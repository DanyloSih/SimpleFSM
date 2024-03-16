using System;

namespace SimpleFSM
{
    public delegate void StateSwitchHandler(IState nextState);

    public class StatesMachine : IStatesMachine
    {
        private IState _currentState;

        public StatesMachine(IState initialState)
        {
            SwitchState(initialState);
        }

        public void SwitchState(IState nextState)
        {
            if (nextState == null)
            {
                throw new ArgumentNullException();
            }

            if (_currentState != null)
            {
                _currentState.Exit();
            }

            _currentState = nextState;
            _currentState.Enter(SwitchState);
        }
    }
}
