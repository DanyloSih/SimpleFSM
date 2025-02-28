using System;

namespace SimpleFSM
{
    public class StateMachine
    {
        private State _currentState;

        public State CurrentState { get => _currentState; }

        public StateMachine(State initialState)
        {
            if (initialState == null)
            {
                throw new ArgumentNullException();
            }

            _currentState = initialState;
            _currentState.Enter(null);
        }

        public void Update(float deltaTime)
        {
            State nextState = _currentState.Update(deltaTime);
            if (nextState != null)
            {
                _currentState.Exit();
                nextState.Enter(_currentState);
                _currentState = nextState;
            }
        }
    }
}