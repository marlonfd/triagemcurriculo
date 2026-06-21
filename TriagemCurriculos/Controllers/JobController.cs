using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TriagemCurriculos.Domain.DTOs;
using TriagemCurriculos.Services.Interface;

namespace TriagemCurriculos.Controllers
{    
    [Route("api/jobs")]
    [ApiController]
    [Authorize]
    public class JobController(IJobPositionService service) : ControllerBase
    {
        private readonly IJobPositionService _service = service;

        [HttpGet()]
        public async Task<IActionResult> GetJobPositions(CancellationToken cancellationToken)
        {
            var jobs = await _service.GetAllJobPositionsActive(cancellationToken);
            return Ok(jobs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobPositionById(long id, CancellationToken cancellationToken)
        {
            var job = await _service.GetById(id, cancellationToken);
            if (job == null)
            {
                return NotFound();
            }
            return Ok(job);
        }

        [HttpPost()]
        public async Task<IActionResult> AddJobPosition([FromBody] JobPositionRequestDTO dto, CancellationToken cancellationToken)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data.");
            }

            await _service.AddJobPosition(dto, cancellationToken);
            return Created(string.Empty, null); // Ideally, we'd return CreatedAtAction with the new ID if AddJobPosition returned it.
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobPosition(long id, [FromBody] JobPositionRequestDTO dto, CancellationToken cancellationToken)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                await _service.UpdateJobPosition(id, dto, cancellationToken);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeactivateJobPosition(long id, CancellationToken cancellationToken)
        {
            try
            {
                await _service.DeactivateJobPosition(id, cancellationToken);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
