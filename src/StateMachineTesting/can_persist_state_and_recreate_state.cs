using Machine.Specifications;
using Raven.Client.Embedded;
using StateMachineTesting.Conditions;
using StateMachineTesting.TestCode;

namespace StateMachineTesting
{
    [Subject(typeof(StateMachine<>))]
    public class can_persist_state_and_recreate_state
    {
        private static EmbeddableDocumentStore store;
        private static string id = "1";
        private static SimpleState firstState;

        Establish create_state_machine_and_set_state = () =>
        {            
            store = new EmbeddableDocumentStore
            {
                RunInMemory = true
            };
            store.Initialize();
        };

        Because of = () =>
        {
            firstState = new SimpleState(new RavenDbPersister(store));
            firstState.TurnOn(new Lamp { Name = "Skippy", Id = 1 });
        };

        It simple_state_should_be_on = () => firstState.State.Name.ShouldEqual("On");

        private Cleanup cleanup = () => store.Dispose();
    }
}