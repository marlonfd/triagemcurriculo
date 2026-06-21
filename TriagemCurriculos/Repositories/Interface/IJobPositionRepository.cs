using System.Numerics;
using TriagemCurriculos.Domain.DTOs;
using TriagemCurriculos.Domain.Entites;
using TriagemCurriculos.Infraestructure.Interface;

namespace TriagemCurriculos.Repositories.Interface
{
    public interface IJobPositionRepository
    {

        Task AddAsync(JobPosition job, CancellationToken cancellationToken);
        Task UpdateAsync(JobPosition job, CancellationToken cancellationToken);
        Task<IList<JobPositionResponseDTO>> GetAllActiveJobs(CancellationToken cancellationToken);
        Task<JobPosition?> GetById(long id, CancellationToken cancellationToken);

    }
}
