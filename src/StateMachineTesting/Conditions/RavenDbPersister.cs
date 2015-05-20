using Raven.Client;

namespace StateMachineTesting.Conditions
{
    public class RavenDbPersister:IStatePersister
    {
        private readonly IDocumentStore _documentStore;

        public RavenDbPersister(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public void Write(StateMemento memento)
        {
            using (var session = _documentStore.OpenSession())
            {
                session.Store(memento);
                session.SaveChanges();
            }
        }

        public StateMemento Get(int id)
        {
            StateMemento result;
            
            using (var session = _documentStore.OpenSession())
            {
                result = session.Load<StateMemento>(id);
            }

            return result;
        }
    }
}