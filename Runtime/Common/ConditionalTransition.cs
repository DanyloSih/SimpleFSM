using System;

namespace SimpleFSM.Common
{
    public class ConditionalTransition : State
    {
        private Func<bool> _condition;
        private State _nextState;

        public ConditionalTransition(
            Func<bool> condition,
            State nextState)
        {
            _condition = condition;
            _nextState = nextState;
        }

        protected override State OnUpdate(float deltaTime)
        {
            if (_condition.Invoke() == true)
            {
                return _nextState;
            }

            return null;
        }
    }
}