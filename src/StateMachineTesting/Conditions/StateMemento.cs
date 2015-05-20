using System;
using System.Collections.Generic;

namespace StateMachineTesting.Conditions
{
    public class StateMemento
    {
        public int Id { get; private set; }

        public IDictionary<string, object> Data { get; private set; }
        public State CurrentState { get; private set; }
        public IList<Tuple<State, Object>> PreviousStates { get; private set; }

        public void UpdateState(IDictionary<string, object> data, State state)
        {
            Id = (int)data["Id"];

            if (CurrentState != null)
            {
                if (PreviousStates == null)
                {
                    PreviousStates = new List<Tuple<State, object>>();
                }

                PreviousStates.Add(new Tuple<State, object>(CurrentState, Data));
            }

            Data = data;
            CurrentState = state;
        }
    }
}