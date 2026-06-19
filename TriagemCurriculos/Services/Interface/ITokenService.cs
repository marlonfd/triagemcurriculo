using TriagemCurriculos.Domain.Entites;

namespace TriagemCurriculos.Services.Interface
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
