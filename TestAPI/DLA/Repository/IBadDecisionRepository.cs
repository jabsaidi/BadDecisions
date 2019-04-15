using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI.DLA.Model;

namespace TestAPI.DLA.Repository
{
    public interface IBadDecisionRepository
    {
        List<BadDecision> GetAll();
        BadDecision GetById(long id);
        BadDecision Create(BadDecision badDecision);
        BadDecision Update(BadDecision modifiedDecision);
        bool Delete(long id);
    }
}
