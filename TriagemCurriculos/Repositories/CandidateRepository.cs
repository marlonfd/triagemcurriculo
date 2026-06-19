using TriagemCurriculos.Domain.Entites;
using TriagemCurriculos.Infraestructure;
using TriagemCurriculos.Infraestructure.Interface;
using TriagemCurriculos.Repositories.Interface;

namespace TriagemCurriculos.Repositories
{
    public class CandidateRepository : RepositoryBase<Candidate>, ICandidateRepository
    {
        public CandidateRepository(MainDbContext db) : base(db)
        {
        }
    }
}
