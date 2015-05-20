using System;
using Newtonsoft.Json;

namespace StateMachineTesting.Conditions
{
    public class ConsolePersister : IStatePersister
    {
        public void Write(StateMemento memento)
        {
            Console.WriteLine(JsonConvert.SerializeObject(memento));
            //            Debug.WriteLine(JsonConvert.SerializeObject(memento));
        }

        public StateMemento Get(int id)
        {
            return new StateMemento();
        }

    }
}