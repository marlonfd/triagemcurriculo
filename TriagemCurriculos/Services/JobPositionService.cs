using TriagemCurriculos.Domain.Entites;
using TriagemCurriculos.Infraestructure;
using TriagemCurriculos.Repositories.Interface;

namespace TriagemCurriculos.Services
{
    public class JobPositionService(MainDbContext db,
    IJobPositionRepository jobPositionRep)
    {
        private readonly MainDbContext _db = db;
        private readonly IJobPositionRepository _jobPositionRep = jobPositionRep;

        public async Task<IList<JobPosition>> getAllJobPositions() {
            return null;
        }
    }
}
