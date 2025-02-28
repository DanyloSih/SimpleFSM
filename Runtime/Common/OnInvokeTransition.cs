namespace SimpleFSM
{
    public class OnInvokeTransition : State
    {
        private State _transitionState;
        private bool invokeFlag = false;

        public OnInvokeTransition(State transitionState)
        {
            _transitionState = transitionState;
        }

        protected override void OnEnter(State previousState)
        {
            invokeFlag = false;
        }

        public void Invoke()
        {
            invokeFlag = true;
        }

        protected override State OnUpdate(float deltaTime)
        {
            if (invokeFlag)
            {
                invokeFlag = false;
                return _transitionState;
            }
            else
            {
                return null;
            }
        }
    }
}