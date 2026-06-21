using TriagemCurriculos.Domain.DTOs;
using TriagemCurriculos.Domain.Entites;
using TriagemCurriculos.Infraestructure.Interface;
using TriagemCurriculos.Repositories.Interface;
using TriagemCurriculos.Services.Interface;

namespace TriagemCurriculos.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRep;
        private readonly IJobPositionRepository _jobPositionRep;
        private readonly ITenantProvider _tenantProvider;
        private readonly IPdfExtractionService _pdfExtractionService;
        private readonly IAiResumeProcessorService _aiResumeProcessorService;

        public CandidateService(
            ICandidateRepository candidateRep, 
            IJobPositionRepository jobPositionRep,
            ITenantProvider tenantProvider,
            IPdfExtractionService pdfExtractionService,
            IAiResumeProcessorService aiResumeProcessorService)
        {
            _candidateRep = candidateRep;
            _jobPositionRep = jobPositionRep;
            _tenantProvider = tenantProvider;
            _pdfExtractionService = pdfExtractionService;
            _aiResumeProcessorService = aiResumeProcessorService;
        }

        public async Task<CandidateResponseDTO> AnalyzeCandidateResume(long candidateId, Stream pdfStream, CancellationToken cancellationToken = default)
        {
            var candidate = await _candidateRep.GetByIdAsync(candidateId, cancellationToken);
            if (candidate == null) throw new ArgumentException("Candidato não encontrado.");

            var job = await _jobPositionRep.GetById(candidate.JobPositionId, cancellationToken);
            if (job == null) throw new ArgumentException("Vaga não encontrada.");

            // 1. Extract Text
            var text = _pdfExtractionService.ExtractTextFromPdfStream(pdfStream);

            // 2. Call AI
            var jobRequirements = $"Título: {job.Title}\nDescrição: {job.Description}";
            var evaluation = await _aiResumeProcessorService.EvaluateCandidateAsync(text, jobRequirements);

            // 3. Update Domain Entity
            candidate.UpdateAiAnalysis(evaluation.MatchScore, evaluation.Skills, evaluation.Justification);

            // 4. Save
            await _candidateRep.UpdateAsync(candidate, cancellationToken);

            return MapToDTO(candidate);
        }

        public async Task AddCandidate(CandidateRequestDTO dto, CancellationToken cancellationToken = default)
        {
            // Verify if JobPosition exists and belongs to the current tenant
            var job = await _jobPositionRep.GetById(dto.JobPositionId, cancellationToken);
            if (job == null || !job.Active)
            {
                throw new ArgumentException("A Vaga especificada não existe ou está inativa.");
            }

            var candidate = new Candidate(
                _tenantProvider.GetTenantId(),
                dto.JobPositionId,
                dto.StatusTypeId,
                dto.CandidateName,
                dto.CandidateEmail,
                dto.ResumeFileUrl
            );

            await _candidateRep.AddAsync(candidate, cancellationToken);
        }

        public async Task<IList<CandidateResponseDTO>> GetAllCandidates(CancellationToken cancellationToken = default)
        {
            var candidates = await _candidateRep.GetAllAsync(cancellationToken);
            
            return candidates.Select(MapToDTO).ToList();
        }

        public async Task<IList<CandidateResponseDTO>> GetCandidatesByJobPosition(long jobPositionId, CancellationToken cancellationToken = default)
        {
            var candidates = await _candidateRep.GetByJobPositionIdAsync(jobPositionId, cancellationToken);
            return candidates.Select(MapToDTO).ToList();
        }

        public async Task<CandidateResponseDTO?> GetById(long id, CancellationToken cancellationToken = default)
        {
            var candidate = await _candidateRep.GetByIdAsync(id, cancellationToken);
            if (candidate == null) return null;

            return MapToDTO(candidate);
        }

        public async Task UpdateCandidate(long id, CandidateRequestDTO dto, CancellationToken cancellationToken = default)
        {
            var candidate = await _candidateRep.GetByIdAsync(id, cancellationToken);
            if (candidate == null) throw new ArgumentException("Candidato não encontrado.");

            // Verify job position
            var job = await _jobPositionRep.GetById(dto.JobPositionId, cancellationToken);
            if (job == null || !job.Active)
            {
                throw new ArgumentException("A Vaga especificada não existe ou está inativa.");
            }

            // Note: Candidate currently has no Update details method in the Domain. 
            // In a real Clean Architecture, we'd add an Update method to Candidate.cs
            // For now we update Status as an example:
            candidate.UpdateStatus(dto.StatusTypeId);
            
            await _candidateRep.UpdateAsync(candidate, cancellationToken);
        }

        public async Task DeleteCandidate(long id, CancellationToken cancellationToken = default)
        {
            var candidate = await _candidateRep.GetByIdAsync(id, cancellationToken);
            if (candidate == null) throw new ArgumentException("Candidato não encontrado.");

            await _candidateRep.DeleteAsync(candidate, cancellationToken);
        }

        private CandidateResponseDTO MapToDTO(Candidate candidate)
        {
            return new CandidateResponseDTO
            {
                Id = candidate.Id,
                TenantId = candidate.TenantId,
                JobPositionId = candidate.JobPositionId,
                StatusTypeId = candidate.StatusTypeId,
                CandidateName = candidate.CandidateName,
                CandidateEmail = candidate.CandidateEmail,
                ExtractedSkills = candidate.ExtractedSkills ?? new List<string>(),
                AiMatchScore = candidate.AiMatchScore,
                AiAnalysisSummary = candidate.AiAnalysisSummary,
                ResumeFileUrl = candidate.ResumeFileUrl,
                CreatedAt = candidate.CreatedAt
            };
        }
    }
}
