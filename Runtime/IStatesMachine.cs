namespace SimpleFSM
{
    public interface IStatesMachine
    {
        void SwitchState(IState state);
    }
}