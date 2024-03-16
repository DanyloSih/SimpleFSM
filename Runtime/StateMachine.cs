using System;

namespace SimpleFSM
{
    public delegate void StateSwitchHandler(IState nextState);

    public class StateMachine : IStateMachine
    {
        public IState CurrentState { get; private set; }
        public bool IsActive { get; private set; }

        public void Start(IState startState)
        {
            if (IsActive)
            {
                throw new InvalidOperationException($"\"{this.GetType().Name}\" SM already started!");
            }

            if (startState == null)
            {
                throw new ArgumentNullException();
            }

            SwitchState(startState);
            IsActive = true;
        }

        public void Stop()
        {
            if (!IsActive)
            {
                throw new InvalidOperationException($"\"{this.GetType().Name}\" SM not started yet!");
            }

            if (CurrentState != null)
            {
                CurrentState.Exit();
                IsActive = false;
            }
        }

        protected void SwitchState(IState nextState)
        {
            if (nextState == null)
            {
                throw new ArgumentNullException();
            }

            if (CurrentState != null)
            {
                CurrentState.Exit();
            }

            OnBeforeSwitchingState(CurrentState, nextState);
            CurrentState = nextState;
            CurrentState.Enter(SwitchState);
        }

        protected virtual void OnBeforeSwitchingState(IState previousState, IState nextState)
        {

        }
    }
}
