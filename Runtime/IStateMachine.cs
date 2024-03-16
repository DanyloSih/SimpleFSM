namespace SimpleFSM
{
    public interface IStateMachine
    {
        public bool IsActive { get; }
        public IState CurrentState { get; }

        public void Start(IState state);
        public void Stop();
    }
}