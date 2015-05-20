namespace StateMachineTesting.Conditions
{
    public interface IStatePersister
    {
        void Write(StateMemento memento);
        StateMemento Get(int id);
    }
}