using System;
using StateMachineTesting.Conditions;

namespace StateMachineTesting.TestCode
{
    public class SimpleState : StateMachine<Lamp>
    {

        public SimpleState(IStatePersister persister)
            : base(persister)
        {
            TurnOn = CreateEvent(WhenTurnedOn, On);
            TurnOff = CreateEvent(WhenTurnedOff, Off);
        }

        private bool WhenTurnedOff(Lamp arg)
        {
            return true;
        }

        public Action<Lamp> TurnOn;
        public Action<Lamp> TurnOff;

        private State On = new State("On");
        private State Off = new State("Off");

        public SimpleState()
        {
            TurnOn = CreateEvent(WhenTurnedOn, On);          
        }


        private bool WhenTurnedOn(Lamp data)
        {
            Console.WriteLine("I am on");

            return true;
        }


    }

    public class Lamp : IStateMessage
    {
        public int Id { get; set; }
        public string Name { get; set; }        
    }
}