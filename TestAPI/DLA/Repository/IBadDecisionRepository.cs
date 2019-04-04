using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI.DLA.Model;

namespace TestAPI.DLA.Repository
{
    public interface IBadDecisionRepository
    {
        BadDecision GetById(long id);
        List<BadDecision> GetAll();

        BadDecision ModifyDecision(long id);
        BadDecision Create(BadDecision badDecision);

        BadDecision Delete(long id);
    }
}
