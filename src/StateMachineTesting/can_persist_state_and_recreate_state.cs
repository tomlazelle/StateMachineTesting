using Raven.Client.Embedded;
using Should;
using StateMachineTesting.Conditions;
using StateMachineTesting.TestCode;

namespace StateMachineTesting
{
    public class can_persist_state_and_recreate_state_Test
    {
        private EmbeddableDocumentStore store;
        private string id = "1";
        private SimpleState firstState;

        public void Setup()
        {
            store = new EmbeddableDocumentStore
            {
                RunInMemory = true
            };
            store.Initialize();
            firstState = new SimpleState(new RavenDbPersister(store));
        }

        public void simple_state_should_be_on()
        {
            
            firstState.TurnOn(new Lamp {Name = "Skippy", Id = 1});
            firstState.State.Name.ShouldEqual("On");
        }

        public void TearDown()
        {
            store.Dispose();
        }
    }
}