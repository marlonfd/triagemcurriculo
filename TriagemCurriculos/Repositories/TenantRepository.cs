using TriagemCurriculos.Domain.Entites;
using TriagemCurriculos.Infraestructure;
using TriagemCurriculos.Infraestructure.Interface;
using TriagemCurriculos.Repositories.Interface;

namespace TriagemCurriculos.Repositories
{
    public class TenantRepository :  ITenantRepository
    {
        private readonly MainDbContext _db;
        public TenantRepository(MainDbContext db)
        {
            _db = db;
        }

        public async Task<Tenant?> GetByIdAsync(string id)
        {
            return await _db.Tenants.FindAsync(id);
        }

        public async Task AddAsync(Tenant tenant)
        {
            await _db.Tenants.AddAsync(tenant);
            await _db.SaveChangesAsync();
        }
    }
}
