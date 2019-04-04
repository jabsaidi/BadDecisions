using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using TestAPI.DLA.Model;
using TestAPI.DLA.Repository;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IBadDecisionRepository _badDecisionRepository;

        public ValuesController(IBadDecisionRepository badDecisionRepository)
        {
            _badDecisionRepository = badDecisionRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<BadDecision> badDecisions = _badDecisionRepository.GetAll();

            if (badDecisions == null)
                return NotFound();

            return Ok(badDecisions);
        }

        // GET api/values/id
        [HttpGet ("{id}", Name ="Get BadDecisionId")]
        public IActionResult GetById(long id)
        {
            var badDecision = _badDecisionRepository.GetById(id);

            if (badDecision == null)
                return NotFound();

            return Ok(badDecision);
        }

        [HttpPut ("{id}", Name ="Modify decision")]
        public IActionResult Update(long id, [FromBody] JObject body)
        {
            BadDecision toBeModified = _badDecisionRepository.ModifyDecision(id);

            var decision = (string)body.SelectToken("decision");

            toBeModified.Decision = decision;

            return Ok(toBeModified);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] JObject body)
        {
            var id = (long)body.SelectToken("id");
            var decision = (string)body.SelectToken("decision");

            BadDecision badDecision = new BadDecision()
            {
                Id = id,
                Decision = decision
            };

            BadDecision newDecision = _badDecisionRepository.Create(badDecision);

            if (newDecision == null)
                return BadRequest();

            return Ok(newDecision);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(long id)
        {
            BadDecision deletedDecision = _badDecisionRepository.Delete(id);

            if (deletedDecision == null)
                return BadRequest();

            return Ok();
        }
    }
}
