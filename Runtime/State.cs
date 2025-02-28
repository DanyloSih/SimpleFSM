using System.Collections.Generic;

namespace SimpleFSM
{
    public abstract class State
    {
        private List<State> _substates = new ();

        /// <summary>
        /// All transitions will be executed in the order in which they were added.
        /// </summary>
        public void AddSubstate(State transition)
        {
            if (_substates.Contains(transition))
            {
                throw new System.ArgumentException(
                    $"{GetType().Name} already contains {transition.GetType().Name} transition! " +
                    $"It is prohibited to add the same transition twice.");
            }
            _substates.Add(transition);
        }

        /// <summary>
        /// <inheritdoc cref="AddTransition(Transition)"/>
        /// </summary>
        public void AddSubstates(params State[] transitions)
        {
            foreach (var transition in transitions)
            {
                AddSubstate(transition);
            }    
        }

        public void RemoveSubstate(State transition)
        {
            _substates.Remove(transition);
        }

        public bool HasSubstate(State transition)
        {
            return _substates.Contains(transition);
        }

        /// <summary>
        /// If this method returns something other than Null, 
        /// then the transition to this state will occur immediately.
        /// </summary>
        public State Update(float deltaTime)
        {
            foreach (var transition in _substates)
            {
                var transitionResult = transition.Update(deltaTime);
                if (transitionResult != null)
                {
                    return transitionResult;
                }
            }

            return OnUpdate(deltaTime);
        }

        /// <summary>
        /// Executed every time when this state become ACTIVE.
        /// </summary>
        /// <param name="previousState">Can be null if the current state is the initial state.</param>
        public void Enter(State previousState)
        {
            foreach (var transition in _substates)
            {
                transition.Enter(previousState); 
            }

            OnEnter(previousState);
        }

        /// <summary>
        /// Executed every time when this state become INACTIVE.
        /// </summary>
        public void Exit()
        {
            foreach (var transition in _substates)
            {
                transition.Exit();
            }

            OnExit();
        }

        /// <summary>
        /// <inheritdoc cref="Enter(State)"/>
        /// </summary>
        /// <param name="previousState"><inheritdoc cref="Enter(State)"/></param>
        protected virtual void OnEnter(State previousState) { }
        /// <summary>
        /// <inheritdoc cref="Exit"/>
        /// </summary>
        protected virtual void OnExit() { }
        /// <summary>
        /// <inheritdoc cref="Update(float)"/>
        /// </summary>
        protected virtual State OnUpdate(float deltaTime) { return null; }

    }
}