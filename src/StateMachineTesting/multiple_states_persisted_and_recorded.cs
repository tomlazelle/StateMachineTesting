using Machine.Specifications;
using Raven.Client.Embedded;
using StateMachineTesting.Conditions;
using StateMachineTesting.TestCode;

namespace StateMachineTesting
{
    [Subject(typeof(StateMachine<>))]
    public class multiple_states_persisted_and_recorded     
    {
        private static EmbeddableDocumentStore store;
        
        private static SimpleState stateMachine;

        Establish create_state_machine_and_set_state = () =>
        {
            store = new EmbeddableDocumentStore
            {
                RunInMemory = true
            };
            store.Initialize();

            
            stateMachine = new SimpleState(new RavenDbPersister(store));
            stateMachine.TurnOn(new Lamp { Name = "Skippy",Id = 1});

        };

        Because of = () =>
        {
            stateMachine.TurnOff(new Lamp{Id=1,Name = "Skippy"});
        };

        It state_should_be_off = () => stateMachine.State.Name.ShouldEqual("Off");

        It state_count_should_be_one = () =>
        {
            using (var session = store.OpenSession())
            {
                var memento = session.Load<StateMemento>(1);

                memento.PreviousStates.Count.ShouldEqual(1);
            }
        };

        private Cleanup cleanup = () => store.Dispose();
    }
}