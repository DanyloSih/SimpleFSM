namespace SimpleFSM.Common
{
    public abstract class OnStartStopTransition : State
    {
        protected abstract bool IsStart { get; }

        private TransitionMode _transitionMode;
        private State _nextState;

        public OnStartStopTransition(
            TransitionMode transitionMode,
            State nextState)
        {
            _transitionMode = transitionMode;
            _nextState = nextState;
        }

        protected override State OnUpdate(float deltaTime)
        {
            if ((IsStart && _transitionMode == TransitionMode.OnStart) 
            || (!IsStart && _transitionMode == TransitionMode.OnStop))
            {
                return _nextState;
            }

            return null;
        }
    }
}