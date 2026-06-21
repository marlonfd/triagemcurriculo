using TriagemCurriculos.Domain.Entites;
using TriagemCurriculos.Infraestructure.Interface;

namespace TriagemCurriculos.Repositories.Interface
{
    public interface ICandidateRepository 
    {
        Task AddAsync(Candidate candidate, CancellationToken cancellationToken);
        Task UpdateAsync(Candidate candidate, CancellationToken cancellationToken);
        Task DeleteAsync(Candidate candidate, CancellationToken cancellationToken);
        Task<IList<Candidate>> GetAllAsync(CancellationToken cancellationToken);
        Task<IList<Candidate>> GetByJobPositionIdAsync(long jobPositionId, CancellationToken cancellationToken);
        Task<Candidate?> GetByIdAsync(long id, CancellationToken cancellationToken);
    }
}
