using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TriagemCurriculos.Domain.DTOs;
using TriagemCurriculos.Services.Interface;

namespace TriagemCurriculos.Controllers
{
    [ApiController]
    [Route("api/candidates")]
    [Authorize]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCandidate([FromBody] CandidateRequestDTO dto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _candidateService.AddCandidate(dto, cancellationToken);
                return Created(string.Empty, null); 
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCandidates(CancellationToken cancellationToken)
        {
            var candidates = await _candidateService.GetAllCandidates(cancellationToken);
            return Ok(candidates);
        }

        [HttpGet("job/{jobPositionId}")]
        public async Task<IActionResult> GetCandidatesByJobPosition(long jobPositionId, CancellationToken cancellationToken)
        {
            var candidates = await _candidateService.GetCandidatesByJobPosition(jobPositionId, cancellationToken);
            return Ok(candidates);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCandidateById(long id, CancellationToken cancellationToken)
        {
            var candidate = await _candidateService.GetById(id, cancellationToken);
            if (candidate == null)
            {
                return NotFound("Candidato não encontrado.");
            }
            return Ok(candidate);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCandidate(long id, [FromBody] CandidateRequestDTO dto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _candidateService.UpdateCandidate(id, dto, cancellationToken);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCandidate(long id, CancellationToken cancellationToken)
        {
            try
            {
                await _candidateService.DeleteCandidate(id, cancellationToken);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("{id}/analyze")]
        public async Task<IActionResult> AnalyzeCandidateResume(long id, [FromForm] IFormFile file, CancellationToken cancellationToken)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("O arquivo PDF do currículo é obrigatório.");
            }

            try
            {
                using var stream = file.OpenReadStream();
                var result = await _candidateService.AnalyzeCandidateResume(id, stream, cancellationToken);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro durante a análise: {ex.Message}");
            }
        }
    }
}
