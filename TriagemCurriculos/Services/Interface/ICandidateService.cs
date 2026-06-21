using TriagemCurriculos.Domain.DTOs;

namespace TriagemCurriculos.Services.Interface
{
    public interface ICandidateService
    {
        Task AddCandidate(CandidateRequestDTO dto, CancellationToken cancellationToken = default);
        Task UpdateCandidate(long id, CandidateRequestDTO dto, CancellationToken cancellationToken = default);
        Task DeleteCandidate(long id, CancellationToken cancellationToken = default);
        Task<IList<CandidateResponseDTO>> GetAllCandidates(CancellationToken cancellationToken = default);
        Task<IList<CandidateResponseDTO>> GetCandidatesByJobPosition(long jobPositionId, CancellationToken cancellationToken = default);
        Task<CandidateResponseDTO?> GetById(long id, CancellationToken cancellationToken = default);
        Task<CandidateResponseDTO> AnalyzeCandidateResume(long candidateId, Stream pdfStream, CancellationToken cancellationToken = default);
    }
}
