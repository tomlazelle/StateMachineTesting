using Should;
using StateMachineTesting.Conditions;
using StateMachineTesting.TestCode;

namespace StateMachineTesting
{
    public class SimpleStateTest
    {
        private SimpleState machine;

        public void Setup()
        {
            machine = new SimpleState(new ConsolePersister());
        }


        public void the_current_state_should_be_waiting()
        {
            machine.TurnOn(new Lamp());
            machine.State.Name.ShouldEqual("On");
        }
    }
}