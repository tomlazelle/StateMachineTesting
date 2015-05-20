using System;
using StateMachineTesting.Common;

namespace StateMachineTesting.Conditions
{
    public class StateEvent<T>
    {
        private readonly Func<T, bool> _doTAction;
        private readonly Action<State> _stateChange;
        private readonly IStatePersister _persister;
        private readonly State _state;


        public StateEvent(Func<T, bool> doAction, State state, Action<State> stateChange)
        {
            _state = state;
            _doTAction = doAction;
            _stateChange = stateChange;
        }

        public StateEvent(Func<T, bool> doAction, State state, Action<State> stateChange, IStatePersister persister)
        {
            _state = state;
            _doTAction = doAction;
            _stateChange = stateChange;
            _persister = persister;
        }

        public bool Raise(T message)
        {
            var result = _doTAction(message);

            if (result)
            {
                _stateChange.Invoke(_state);

                var stateMemento = FetchState(((IStateMessage)message).Id);

                stateMemento.UpdateState(message.ToDynamic(), _state);

                PersistState(stateMemento);
            }

            return result;
        }

        private StateMemento FetchState(int id)
        {
            var foundState = _persister.Get(id);

            if (foundState != null) return foundState;

            return new StateMemento();
        }

        private void PersistState(StateMemento memento)
        {
            if (_persister != null)
            {
                _persister.Write(memento);
            }
        }
    }
}