using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI.DLA.Model;

namespace TestAPI.DLA.Repository
{
    public interface IBadDecisionRepository
    {
        bool Delete(long id);
        List<BadDecision> GetAll();
        BadDecision GetById(long id);
        BadDecision GetByDecision(string decision);
        BadDecision Create(BadDecision badDecision);
        BadDecision Update(BadDecision modifiedDecision);
    }
}
