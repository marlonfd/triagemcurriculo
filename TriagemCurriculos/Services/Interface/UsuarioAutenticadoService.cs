using TriagemCurriculos.Domain.Entites;

namespace TriagemCurriculos.Services.Interface
{
    public class UsuarioAutenticadoService : IUsuarioAutenticadoService
    {

        public Task<Tenant> GetTenantDoUsuario()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUsuarioAutenticado()
        {
            throw new NotImplementedException();
        }
    }
}
