using System.Collections.Generic;

namespace SimpleFSM
{
    public interface IState : IEnterableObject<StateSwitchHandler>
    {
        void AddTransition(ITransition transition);

        void AddTransitions(ICollection<ITransition> transitions);

        void ClearTransitions();
    }
}