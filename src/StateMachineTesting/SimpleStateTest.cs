using Machine.Specifications;
using StateMachineTesting.Conditions;
using StateMachineTesting.TestCode;

namespace StateMachineTesting
{
    [Subject(typeof (StateMachine<>))]
    public class SimpleStateTest
    {
        private static SimpleState machine;

        private Establish context = () =>
        {
            machine = new SimpleState(new ConsolePersister());
        };

        private Because of = () => machine.TurnOn(new Lamp());
        private It the_current_state_should_be_waiting = () => machine.State.Name.ShouldEqual("On");
    }
}