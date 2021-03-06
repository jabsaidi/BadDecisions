﻿using TestAPI.DLA.Model;
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

        [HttpGet("{id}", Name = "Get BadDecisionId")]
        public IActionResult GetById(long id)
        {
            BadDecision badDecision = _badDecisionRepository.GetById(id);
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
            BadDecision modified = _badDecisionRepository.Update(toBeModified);
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

            BadDecision newDecision = _badDecisionRepository.Create(badDecision);
            if (newDecision == null)
                return BadRequest();
            return Ok(newDecision);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(long id)
        {
            bool deletedDecision = _badDecisionRepository.Delete(id);
            if (!deletedDecision)
                return BadRequest();
            return Ok();
        }

        [HttpGet("decision/{decision}", Name ="Get by decision")]
        public IActionResult GetByDecision(string decision)
        {
            BadDecision badDecision = _badDecisionRepository.GetByDecision(decision);

            if (badDecision == null)
                return NotFound();
            return Ok(badDecision);
        }
    }
}