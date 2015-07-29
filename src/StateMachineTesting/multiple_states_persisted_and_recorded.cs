using Raven.Client.Embedded;
using Should;
using StateMachineTesting.Conditions;
using StateMachineTesting.TestCode;

namespace StateMachineTesting
{
    public class multiple_states_persisted_and_recorded_Test
    {
        private EmbeddableDocumentStore store;

        private SimpleState stateMachine;

        public void Setup()
        {
            store = new EmbeddableDocumentStore
            {
                RunInMemory = true
            };
            store.Initialize();


            stateMachine = new SimpleState(new RavenDbPersister(store));
            stateMachine.TurnOn(new Lamp {Name = "Skippy", Id = 1});
        }


        public void state_should_be_off()
        {
            stateMachine.TurnOff(new Lamp {Id = 1, Name = "Skippy"});

            stateMachine.State.Name.ShouldEqual("Off");
        }

        public void database_state_count_should_be_one()
        {
            using (var session = store.OpenSession())
            {
                var memento = session.Load<StateMemento>(1);

                memento.PreviousStates.Count.ShouldEqual(1);
            }
        }

    public void TearDown() { store.Dispose(); }
    }
}