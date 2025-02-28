using UnityEngine;

namespace SimpleFSM.Common
{
    public class OnAnimationStateEventTransition : State
    {
        private AnimationStateEvent _transitionEvent;
        private Animator _animator;
        private string _animationStateName;
        private int _animationStateNameHash;
        private int _previousStateNameHash;
        private int _animationStateLayer;
        private State _transitionState;

        public OnAnimationStateEventTransition(
            AnimationStateEvent transitionEvent,
            Animator animator,
            string animationStateName,
            int animationStateLayer,
            State transitionState)
        {
            _transitionEvent = transitionEvent;
            _animator = animator;
            _animationStateName = animationStateName;
            _animationStateLayer = animationStateLayer;
            _animationStateNameHash = Animator.StringToHash(_animationStateName);
            _transitionState = transitionState;
        }

        protected override void OnEnter(State previousState)
        {
            AnimatorStateInfo currentStateInfo = _animator
                .GetCurrentAnimatorStateInfo(_animationStateLayer);

            _previousStateNameHash = currentStateInfo.shortNameHash;
        }

        protected override State OnUpdate(float deltaTime)
        {
            AnimatorStateInfo currentStateInfo = _animator
                .GetCurrentAnimatorStateInfo(_animationStateLayer);

            int currentStateNameHash = currentStateInfo.shortNameHash;

            if (_previousStateNameHash == _animationStateNameHash 
             && currentStateNameHash != _animationStateNameHash 
             && _transitionEvent == AnimationStateEvent.OnLeaveState)
            {
                return _transitionState;
            }

            if (_previousStateNameHash != _animationStateNameHash
             && currentStateNameHash == _animationStateNameHash
             && _transitionEvent == AnimationStateEvent.OnEnterState)
            {
                return _transitionState;
            }

            if (_animator.IsInTransition(_animationStateLayer))
            {
                _previousStateNameHash = currentStateNameHash;
            }

            return null;
        }
    }
}