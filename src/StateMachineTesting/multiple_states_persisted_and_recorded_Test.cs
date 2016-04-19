using Ploeh.AutoFixture;
using Raven.Client;
using Raven.Client.Embedded;
using Should;
using StateMachineTesting.Conditions;
using StateMachineTesting.TestCode;

namespace StateMachineTesting
{
    public class multiple_states_persisted_and_recorded_Test : BaseTest
    {
        private static IDocumentStore store;

        private static SimpleState stateMachine;


        


       
        [OrderBy(Index = 1)]
        public void state_should_be_turned_off()
        {
            stateMachine.TurnOff(new Lamp
            {
                Id = 1,
                Name = "Skippy"
            });

            stateMachine.State.ShouldEqual(stateMachine.Off);
       
           

       
        }

        [OrderBy(Index = 2)]
        public void last(){
            using (var session = store.OpenSession())
            {
                var memento = session.Load<StateMemento>(1);

                memento.PreviousStates.Count.ShouldEqual(1);
            }
        }

        public override void FixtureSetup(IFixture registry)
        {

            store = new EmbeddableDocumentStore
            {
                RunInMemory = true
            }.Initialize();


            stateMachine = new SimpleState(new RavenDbPersister(store));
            stateMachine.TurnOn(new Lamp
            {
                Name = "Skippy",
                Id = 1
            });


        }

        public override void FixtureTeardown()
        {
//            store.Dispose();
        }
    }
}