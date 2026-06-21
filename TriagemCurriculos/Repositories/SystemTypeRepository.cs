using TriagemCurriculos.Domain.Entites;
using TriagemCurriculos.Infraestructure;
using TriagemCurriculos.Infraestructure.Interface;
using TriagemCurriculos.Repositories.Interface;

namespace TriagemCurriculos.Repositories
{
    public class SystemTypeRepository : ISystemTypeRepository
    {
        private readonly MainDbContext _db;
        public SystemTypeRepository(MainDbContext db) 
        {
            _db = db;
        }
    }
}
