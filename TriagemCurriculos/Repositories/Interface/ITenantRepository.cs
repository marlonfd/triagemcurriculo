using TriagemCurriculos.Domain.Entites;
using TriagemCurriculos.Infraestructure.Interface;

namespace TriagemCurriculos.Repositories.Interface
{
    public interface ITenantRepository
    {
        Task<Tenant?> GetByIdAsync(string id);
        Task AddAsync(Tenant tenant);

    }
}
