using TestAPI.DLA.Model;
using System.Collections.Generic;

namespace TestAPI.DLA.Repository
{
    public class BadDecisionFileRepository : IBadDecisionRepository
    {
        private readonly object _fileLock = new object();
        private readonly BaseFileRepository _baseFile = new BaseFileRepository("badDecisions.txt");
        private readonly Persistor<BadDecision> _persistor = new Persistor<BadDecision>("badDecisions.txt");

        public BadDecisionFileRepository()
        {
            lock (_fileLock)
            {
                _baseFile.InitFile("decision", "id");
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

        public BadDecision GetByDecision(string decision)
        {
            lock (_fileLock)
            {
                return _persistor.GetByDecision(decision);
            }
        }
    }
}