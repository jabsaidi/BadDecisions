using System.Collections.Generic;
using TestAPI.DLA.Model;

namespace TestAPI.DLA.Repository
{
    public interface IBadDecisionFileRepository
    {
        BadDecision Create(BadDecision badDecision);
        BadDecision GetById(long id);
        BadDecision Update(BadDecision badDecision);
        bool Delete(long id);
        List<BadDecision> GetAll();
    }
}
