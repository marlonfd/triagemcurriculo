using Microsoft.EntityFrameworkCore;
using TriagemCurriculos.Domain.Entites;
using TriagemCurriculos.Infraestructure;
using TriagemCurriculos.Infraestructure.Interface;
using TriagemCurriculos.Repositories.Interface;

namespace TriagemCurriculos.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly MainDbContext _db;

        public CandidateRepository(MainDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Candidate candidate, CancellationToken cancellationToken = default)
        {
            await _db.Candidates.AddAsync(candidate, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task<IList<Candidate>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Candidates
                .Include(c => c.JobPosition)
                .Include(c => c.StatusType)
                .ToListAsync(cancellationToken);
        }

        public async Task<IList<Candidate>> GetByJobPositionIdAsync(long jobPositionId, CancellationToken cancellationToken = default)
        {
            return await _db.Candidates
                .Include(c => c.JobPosition)
                .Include(c => c.StatusType)
                .Where(c => c.JobPositionId == jobPositionId)
                .ToListAsync(cancellationToken);
        }

        public async Task<Candidate?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await _db.Candidates
                .Include(c => c.JobPosition)
                .Include(c => c.StatusType)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task UpdateAsync(Candidate candidate, CancellationToken cancellationToken = default)
        {
            _db.Entry(candidate).State = EntityState.Modified;
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Candidate candidate, CancellationToken cancellationToken = default)
        {
            _db.Candidates.Remove(candidate);
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
