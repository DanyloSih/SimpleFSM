namespace SimpleFSM
{
    public interface ITransition : IEnterableObject<StateSwitchHandler>
    {
        IState NextState { get; set; }
    }
}