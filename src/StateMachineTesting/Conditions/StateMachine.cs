using System;

namespace StateMachineTesting.Conditions
{
    public class StateMachine<T>
    {
        private readonly IStatePersister _persister;
        private State _currentState;

        public StateMachine()
        {
        }


        public StateMachine(IStatePersister persister)
        {
            _persister = persister;
        }

        public State State
        {
            get
            {
                return _currentState;
            }
        }

        protected void SetCurrentState(State state)
        {
            _currentState = state;
        }

        public Action<T> CreateEvent(Func<T, bool> doAction, State state)
        {
            if (_persister != null)
            {
                return x => new StateEvent<T>(doAction, state, SetCurrentState, _persister).Raise(x);
            }

            return x => new StateEvent<T>(doAction, state, SetCurrentState).Raise(x);
        }


    }

    public interface IStateMessage
    {
        int Id { get; set; }
    }
}