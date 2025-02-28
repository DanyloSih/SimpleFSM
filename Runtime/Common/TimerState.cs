using UnityEngine;

namespace SimpleFSM.Common
{
    public class TimerState : State
    {
        private float _timerDuration;
        private State _timeoutState;
        private float _timer;

        public float TimerDuration { get => _timerDuration; }
        public float Timer { get => _timer; }
        public float TimerProgress { get => Mathf.Clamp01(_timer / _timerDuration); }

        public TimerState(float timerDuration, State timeoutState)
        {
            _timerDuration = timerDuration;
            _timeoutState = timeoutState;
        }

        protected override State OnUpdate(float deltaTime)
        {
            _timer += deltaTime;
            if (_timer > _timerDuration)
            {
                OnTimerTick();
                return _timeoutState;
            }

            OnTimerTick();
            return base.OnUpdate(deltaTime);
        }

        protected override void OnEnter(State previousState)
        {
            _timer = 0f;
            OnTimerTick();
        }

        protected virtual void OnTimerTick()
        {

        }
    }
}