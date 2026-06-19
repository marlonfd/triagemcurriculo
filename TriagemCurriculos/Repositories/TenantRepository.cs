using TriagemCurriculos.Domain.Entites;
using TriagemCurriculos.Infraestructure;
using TriagemCurriculos.Infraestructure.Interface;
using TriagemCurriculos.Repositories.Interface;

namespace TriagemCurriculos.Repositories
{
    public class TenantRepository : RepositoryBase<Tenant>, ITenantRepository
    {
        public TenantRepository(MainDbContext db) : base(db)
        {
        }
    }
}
