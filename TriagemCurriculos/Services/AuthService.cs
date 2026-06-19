using BCrypt.Net;
using TriagemCurriculos.Domain.DTOs;
using TriagemCurriculos.Domain.Entites;
using TriagemCurriculos.Repositories.Interface;
using TriagemCurriculos.Services.Interface;

namespace TriagemCurriculos.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AuthService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            
            if (user == null || !user.IsActive)
            {
                throw new UnauthorizedAccessException("Credenciais inválidas.");
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            
            if (!isPasswordValid)
            {
                throw new UnauthorizedAccessException("Credenciais inválidas.");
            }

            var token = _tokenService.GenerateToken(user);

            return new AuthResponse
            {
                Token = token,
                Name = user.Name,
                Email = user.Email,
                TenantId = user.TenantId
            };
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("Este e-mail já está em uso.");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var newUser = new User(
                request.TenantId,
                request.Name,
                request.Email,
                passwordHash,
                request.RoleTypeId
            );

            await _userRepository.AddAsync(newUser);

            var token = _tokenService.GenerateToken(newUser);

            return new AuthResponse
            {
                Token = token,
                Name = newUser.Name,
                Email = newUser.Email,
                TenantId = newUser.TenantId
            };
        }
    }
}
