using System.Collections.Generic;
using Ecs.Exceptions;

namespace Ecs.FSM
{
    public class BaseStateMachine
    {
        private BaseState _currentState;
        private List<BaseState> _states;
        private Dictionary<BaseState, List<Transition>> _transitions;

        protected BaseStateMachine()
        {
            _states = new List<BaseState>();
            _transitions = new Dictionary<BaseState, List<Transition>>();
        }

        protected void SetInitialState(BaseState state)
        {
            _currentState = state;
        }

        protected void AddState(BaseState state, List<Transition> transitions) 
        {
            if (!_states.Contains(state))
            {
                _states.Add(state);
                _transitions.Add(state, transitions);
            }
            else
            {
                throw new AlreadyExistsException($"State {state.GetType()} already exists in state machine!");
            }
        }

        public void Update()
        {
            foreach (var transition in _transitions[_currentState])
            {
                if (transition.Condition())
                {
                    _currentState = transition.ToState;
                    break;
                }
            }
            _currentState.Execute();
        }
    }
}
