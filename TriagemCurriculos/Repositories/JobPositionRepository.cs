using Microsoft.EntityFrameworkCore;
using TriagemCurriculos.Domain.DTOs;
using TriagemCurriculos.Domain.Entites;
using TriagemCurriculos.Infraestructure;
using TriagemCurriculos.Infraestructure.Interface;
using TriagemCurriculos.Repositories.Interface;

namespace TriagemCurriculos.Repositories
{
    public class JobPositionRepository : IJobPositionRepository
    {
        private readonly MainDbContext _db;
        public JobPositionRepository(MainDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(JobPosition job, CancellationToken cancellationToken = default)
        {
            await _db.JobPositions.AddAsync(job, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task<IList<JobPositionResponseDTO>> GetAllActiveJobs(CancellationToken cancellationToken = default)
        {
            return await _db.JobPositions
                .Where(c => c.Active)
                .Select(c => new JobPositionResponseDTO 
                { 
                    Id = c.Id, 
                    TenantId = c.TenantId, 
                    Title = c.Title, 
                    Description = c.Description, 
                    IsActive = c.Active, 
                    CreatedAt = c.CreatedAt 
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<JobPosition?> GetById(long id, CancellationToken cancellationToken = default)
        {
            return await _db.JobPositions
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }

  

        public async Task UpdateAsync(JobPosition job, CancellationToken cancellationToken = default)
        {
            _db.Entry(job).State = EntityState.Modified;
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
