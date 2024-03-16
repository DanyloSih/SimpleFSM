using System.Collections.Generic;

namespace SimpleFSM
{
    public abstract class State : EnterableObject<StateSwitchHandler>, IState
    {
        private List<ITransition> _transitions = new List<ITransition>();

        protected override void OnEnter()
        {
            int counter = 0;

            while (counter < _transitions.Count)
            {
                _transitions[counter].Enter(Context);
                counter++;
            }

            Start();
        }

        protected override void OnExit()
        {
            int counter = 0;

            while (counter < _transitions.Count)
            {
                _transitions[counter].Exit();
                counter++;
            }

            Stop();
        }

        protected abstract void Stop();
       
        protected abstract void Start();

        public void AddTransition(ITransition transition)
            => _transitions.Add(transition);

        public void ClearTransitions()
            => _transitions.Clear();

        public void AddTransitions(ICollection<ITransition> transitions)
        {
            foreach (var transition in transitions)
            {
                _transitions.Add(transition);
            }
        }
    }
}
