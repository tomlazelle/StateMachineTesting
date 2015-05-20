using System;

namespace StateMachineTesting.Conditions
{
    public class State : IComparable<State>
    {
        private readonly string _name;

        public State(string name)
        {
            _name = name;
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public override bool Equals(object obj)
        {
            var equals = obj as State;

            return this.Name == equals.Name;
        }

        public override int GetHashCode()
        {
            return _name.GetHashCode();
        }

        public int CompareTo(State other)
        {
            if (other == null) return 1;

            return _name.CompareTo(other.Name);
        }
    }
}