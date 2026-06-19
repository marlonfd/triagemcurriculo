using TriagemCurriculos.Domain.Entites;
using TriagemCurriculos.Infraestructure;
using TriagemCurriculos.Infraestructure.Interface;
using TriagemCurriculos.Repositories.Interface;

namespace TriagemCurriculos.Repositories
{
    public class SystemTypeRepository : RepositoryBase<SystemType>, ISystemTypeRepository
    {
        public SystemTypeRepository(MainDbContext db) : base(db)
        {
        }
    }
}
