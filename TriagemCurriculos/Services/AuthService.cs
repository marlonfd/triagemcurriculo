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
        private readonly ITenantRepository _tenantRepository;
        private readonly ITokenService _tokenService;

        public AuthService(IUserRepository userRepository, ITenantRepository tenantRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tenantRepository = tenantRepository;
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
            Tenant? tenant = null;

            if (!string.IsNullOrWhiteSpace(request.TenantId))
            {
                tenant = await _tenantRepository.GetByIdAsync(request.TenantId);
                if (tenant == null)
                {
                    throw new ArgumentException("O Tenant informado não existe.");
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(request.CompanyName))
                {
                    throw new ArgumentException("Para criar um novo Tenant, envie o campo 'companyName'.");
                }
                
                string newTenantId = Guid.NewGuid().ToString();
                tenant = new Tenant(newTenantId, request.CompanyName);
                await _tenantRepository.AddAsync(tenant);
            }

            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("Este e-mail já está em uso.");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var newUser = new User(
                tenant.Id,
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
