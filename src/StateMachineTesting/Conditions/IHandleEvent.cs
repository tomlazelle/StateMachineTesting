namespace StateMachineTesting.Conditions
{
    public interface IHandleEvent<T>
    {
        void Handle(T message);
    }
}