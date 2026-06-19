using TriagemCurriculos.Domain.DTOs;

namespace TriagemCurriculos.Services.Interface
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
    }
}
