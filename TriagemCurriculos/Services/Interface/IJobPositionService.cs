using TriagemCurriculos.Domain.DTOs;
using TriagemCurriculos.Domain.Entites;

namespace TriagemCurriculos.Services.Interface
{
    public interface IJobPositionService
    {
        Task AddJobPosition(JobPositionRequestDTO dto, CancellationToken cancellationToken = default);
        Task UpdateJobPosition(long id, JobPositionRequestDTO dto, CancellationToken cancellationToken = default);
        Task DeactivateJobPosition(long id, CancellationToken cancellationToken = default);
        Task<IList<JobPositionResponseDTO>> GetAllJobPositionsActive(CancellationToken cancellationToken = default);
        Task<JobPositionResponseDTO?> GetById(long idJob, CancellationToken cancellationToken = default);
    }
}
