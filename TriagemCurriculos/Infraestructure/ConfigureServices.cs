using TriagemCurriculos.Infraestructure.Data;
using TriagemCurriculos.Infraestructure.Interface;
using TriagemCurriculos.Infraestructure.Provider;

namespace TriagemCurriculos.Infraestructure
{
    public static class ConfigureServices
    {
        public static void RegisterServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddScoped<ITenantProvider, TenantProvider>();
            services.AddScoped<HttpContextService>();
            services.AddScoped<TriagemCurriculos.Services.Interface.ITokenService, TriagemCurriculos.Services.TokenService>();
            services.AddScoped<TriagemCurriculos.Services.Interface.IAuthService, TriagemCurriculos.Services.AuthService>();
        }
    }
}
