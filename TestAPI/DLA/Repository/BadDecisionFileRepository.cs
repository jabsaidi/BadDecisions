using System.Collections.Generic;
using TestAPI.DLA.Model;

namespace TestAPI.DLA.Repository
{
    public class BadDecisionFileRepository : BaseFileRepository, IBadDecisionFileRepository
    {
        private readonly object _fileLock = new object();
        private Persistor<BadDecision> _persistor = new Persistor<BadDecision>();

        public BadDecisionFileRepository(Persistor<BadDecision> persistor)
        {
            _persistor = persistor;
        }

        public BadDecisionFileRepository()
        {
            lock (_fileLock)
            {
                InitFile("decision", "id");
            }
        }

        public BadDecision Create(BadDecision badDecision)
        {
            lock (_fileLock)
            {
                return _persistor.Create(badDecision);
            }
        }

        public BadDecision GetById(long id)
        {
            lock (_fileLock)
            {
                return _persistor.GetById(id);
            }
        }

        public BadDecision Update(BadDecision badDecision)
        {
            lock (_fileLock)
            {
                return _persistor.Update(badDecision);
            }
        }

        public bool Delete(long id)
        {
            lock (_fileLock)
            {
                return _persistor.Delete(id);
            }
        }

        public List<BadDecision> GetAll()
        {
            lock (_fileLock)
            {
                return _persistor.GetAll();
            }
        }
    }
}
