using System.Collections.Generic;
using System.Linq;
using TestAPI.DLA.Model;

namespace TestAPI.DLA.Repository
{
    public class BadDecisionRepository : IBadDecisionRepository
    {
        private static List<BadDecision> _bd = new List<BadDecision>();
        public BadDecisionRepository()
        {
            if (_bd.Count == 0)
            {
                CreateData();
            }
        }

        public BadDecision Create(BadDecision badDecision)
        {
            BadDecision exists = _bd.SingleOrDefault(b => b.Id == badDecision.Id);

            if (exists == null)
            {
                _bd.Add(badDecision);
                return badDecision;
            }
            else
                return null;
        }

        public void CreateData()
        {
            _bd.Add(new BadDecision() { Id = 1, Decision = "Couper mes twists" });
            _bd.Add(new BadDecision() { Id = 2, Decision = "Dire à ma  blonde que j'aime pas ses nouveaux cheveux" });
            _bd.Add(new BadDecision() { Id = 3, Decision = "Ignorer ma blonde" });
            _bd.Add(new BadDecision() { Id = 4, Decision = "Ne pas répondre aux textos de ma blonde quand je sors" });
            _bd.Add(new BadDecision() { Id = 5, Decision = "Ne pas dire bonne nuit à ma blonde avant de me coucher" });
        }

        public BadDecision Delete(long id)
        {
            BadDecision badDecision = _bd.SingleOrDefault(b => b.Id == id);

            if (badDecision == null)
            {
                return null;
            }

            _bd.Remove(badDecision);
            return badDecision;
        }

        public List<BadDecision> GetAll()
        {
            return _bd;
        }

        public BadDecision GetById(long id)
        {
            return _bd.FirstOrDefault(b => b.Id == id);
        }

        public BadDecision ModifyDecision(BadDecision modifiedDecision)
        {
            BadDecision toBeModified = GetById(modifiedDecision.Id);

            toBeModified.Decision = modifiedDecision.Decision;
            return toBeModified;
        }
    }
}
