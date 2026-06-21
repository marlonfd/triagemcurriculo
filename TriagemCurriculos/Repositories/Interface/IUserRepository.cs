using TriagemCurriculos.Domain.Entites;
using TriagemCurriculos.Infraestructure.Interface;

namespace TriagemCurriculos.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task AddAsync(User user);
    }
}
