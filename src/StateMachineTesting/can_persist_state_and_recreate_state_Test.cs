using Ploeh.AutoFixture;
using Raven.Client.Embedded;
using Should;
using StateMachineTesting.Conditions;
using StateMachineTesting.TestCode;

namespace StateMachineTesting
{
    public class can_persist_state_and_recreate_state_Test : BaseTest
    {
        private static EmbeddableDocumentStore store;
        private static string id = "1";
        private static SimpleState firstState;


        private IFixture _registry;


        public void simple_state_should_be_on()
        {
            firstState.State.Name.ShouldEqual("On");
        }

        public override void FixtureSetup(IFixture registry)
        {
            _registry = registry;

            store = new EmbeddableDocumentStore
            {
                RunInMemory = true
            };
            store.Initialize();

            firstState = new SimpleState(new RavenDbPersister(store));
            firstState.TurnOn(new Lamp
            {
                Name = "Skippy", Id = 1
            });
        }

        public override void FixtureTeardown()
        {
            store.Dispose();
        }
    }
}