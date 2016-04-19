using Ploeh.AutoFixture;
using Should;
using StateMachineTesting.Conditions;
using StateMachineTesting.TestCode;

namespace StateMachineTesting
{
    public class SimpleState_Test: BaseTest
    {
        private static SimpleState machine;

        private IFixture _registry;


        public void the_current_state_should_be_waiting()
        {
            machine.TurnOn(new Lamp());
            machine.State.Name.ShouldEqual("On");
        }

        public override void FixtureSetup(IFixture registry)
        {
            _registry = registry;
            machine = new SimpleState(new ConsolePersister());
        }

        public override void FixtureTeardown()
        {
        }
    }
}