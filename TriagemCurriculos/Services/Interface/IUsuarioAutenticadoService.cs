using TriagemCurriculos.Domain.Entites;

namespace TriagemCurriculos.Services.Interface
{
    public interface IUsuarioAutenticadoService
    {
        Task<User> GetUsuarioAutenticado();
        Task<Tenant> GetTenantDoUsuario();
    }
}
