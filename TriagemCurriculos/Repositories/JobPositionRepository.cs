using TriagemCurriculos.Domain.Entites;
using TriagemCurriculos.Infraestructure;
using TriagemCurriculos.Infraestructure.Interface;
using TriagemCurriculos.Repositories.Interface;

namespace TriagemCurriculos.Repositories
{
    public class JobPositionRepository : RepositoryBase<JobPosition>, IJobPositionRepository
    {
        public JobPositionRepository(MainDbContext db) : base(db)
        {
        }
    }
}
