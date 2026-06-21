using TriagemCurriculos.Domain.DTOs;
using TriagemCurriculos.Domain.Entites;
using TriagemCurriculos.Infraestructure;
using TriagemCurriculos.Infraestructure.Interface;
using TriagemCurriculos.Repositories.Interface;
using TriagemCurriculos.Services.Interface;

namespace TriagemCurriculos.Services
{
    public class JobPositionService(MainDbContext db,
    IJobPositionRepository jobPositionRep,ITenantProvider tenantProvider):IJobPositionService
    {
        private readonly MainDbContext _db = db;
        private readonly IJobPositionRepository _rep = jobPositionRep;
        private readonly ITenantProvider _tenantProvider = tenantProvider;


        public Task AddJobPosition(JobPositionRequestDTO dto,CancellationToken cancellationToken = default)
        {
            return _rep.AddAsync(new JobPosition(_tenantProvider.GetTenantId(),dto.Title,dto.Description,true),cancellationToken);
        }

        public async Task UpdateJobPosition(long id, JobPositionRequestDTO dto, CancellationToken cancellationToken = default)
        {
            var job = await _rep.GetById(id, cancellationToken);
            if (job == null) throw new ArgumentException("Vaga não encontrada.");

            job.UpdateDetails(dto.Title, dto.Description);
            await _rep.UpdateAsync(job, cancellationToken);
        }

        public async Task DeactivateJobPosition(long id, CancellationToken cancellationToken = default)
        {
            var job = await _rep.GetById(id, cancellationToken);
            if (job == null) throw new ArgumentException("Vaga não encontrada.");

            job.Active = false;
            await _rep.UpdateAsync(job, cancellationToken);
        }

        public async Task<IList<JobPositionResponseDTO>> GetAllJobPositionsActive(CancellationToken cancellationToken = default) {
            return await _rep.GetAllActiveJobs(cancellationToken);
        }

        public async Task<JobPositionResponseDTO?> GetById(long idJob,CancellationToken cancellationToken = default)
        {
            var job = await _rep.GetById(idJob, cancellationToken);
            if (job == null) return null;

            return new JobPositionResponseDTO 
            {
                Id = job.Id,
                TenantId = job.TenantId,
                Title = job.Title,
                Description = job.Description,
                IsActive = job.Active,
                CreatedAt = job.CreatedAt
            };
        }
    }
}
