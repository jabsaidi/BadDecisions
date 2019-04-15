using TestAPI.DLA.Model;
using TestAPI.DLA.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IBadDecisionRepository _badDecisionRepository;
        private readonly IBadDecisionFileRepository _badDecisionFileRepository;

        public ValuesController(IBadDecisionRepository badDecisionRepository, IBadDecisionFileRepository badDecisionFileRepository)
        {
            _badDecisionRepository = badDecisionRepository;
            _badDecisionFileRepository = badDecisionFileRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<BadDecision> badDecisions = _badDecisionRepository.GetAll();
            List<BadDecision> fromCsv = _badDecisionFileRepository.GetAll();

            if (badDecisions == null)
                return NotFound();

            return Ok(badDecisions);
        }

        // GET api/values/id
        [HttpGet("{id}", Name = "Get BadDecisionId")]
        public IActionResult GetById(long id)
        {
            var badDecision = _badDecisionRepository.GetById(id);
            BadDecision fileDecision = _badDecisionFileRepository.GetById(id);

            if (badDecision == null)
                return NotFound();

            return Ok(badDecision);
        }

        [HttpPut("{id}", Name = "Modify decision")]
        public IActionResult Update(long id, JsonBody body)
        {

            string decision = body.decision;

            BadDecision toBeModified = new BadDecision()
            {
                Id = id,
                Decision = decision
            };
            BadDecision modified = _badDecisionRepository.ModifyDecision(toBeModified);
            BadDecision modifieCsv = _badDecisionFileRepository.Update(toBeModified);
            return Ok(toBeModified);
        }

        [HttpPost("create")]
        public IActionResult Create(JsonBody body)
        {
            List<BadDecision> decisions = _badDecisionRepository.GetAll();
            string decision = body.decision;

            BadDecision badDecision = new BadDecision()
            {
                Id = decisions.Count + 1,
                Decision = decision
            };

            BadDecision fileDecision = _badDecisionFileRepository.Create(badDecision);
            BadDecision newDecision = _badDecisionRepository.Create(badDecision);

            if (newDecision == null)
                return BadRequest();

            return Ok(newDecision);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(long id)
        {
            BadDecision deletedDecision = _badDecisionRepository.Delete(id);
            bool deleted = _badDecisionFileRepository.Delete(id);

            if (deletedDecision == null)
                return BadRequest();

            return Ok();
        }
    }
}
