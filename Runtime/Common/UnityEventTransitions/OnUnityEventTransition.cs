using UnityEngine.Events;

namespace SimpleFSM.Common
{
    public class OnUnityEventTransition : State
    {
        private UnityEvent _unityEvent;
        private State _nextState;
        private bool _eventFlag;

        public OnUnityEventTransition(UnityEvent unityEvent, State nextState)
        {
            _unityEvent = unityEvent;
            _nextState = nextState;
        }

        protected override void OnEnter(State previousState)
        {
            _eventFlag = false;
            _unityEvent.AddListener(OnEvent);
        }

        protected override void OnExit()
        {
            _unityEvent.RemoveListener(OnEvent);
        }

        protected override State OnUpdate(float deltaTime)
        {
            if (_eventFlag)
            {
                _eventFlag = false;
                return _nextState;
            }

            return null;
        }

        private void OnEvent()
        {
            _eventFlag = true;
        }
    }
}