using TriagemCurriculos.Infraestructure.Interface;
using TriagemCurriculos.Infraestructure.Provider;
using TriagemCurriculos.Services;
using TriagemCurriculos.Services.Interface;

namespace TriagemCurriculos.Infraestructure
{
    public static class ConfigureServices
    {
        public static void RegisterServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddScoped<ITenantProvider, TenantProvider>();
            services.AddScoped<HttpContextService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJobPositionService, JobPositionService>();
            services.AddScoped<ICandidateService, CandidateService>();
            
            services.AddScoped<IPdfExtractionService, PdfExtractionService>();
            services.AddScoped<IAiResumeProcessorService, AiResumeProcessorService>();

            var openAiApiKey = builder.Configuration["OpenAI:ApiKey"] ?? string.Empty;
            services.AddSingleton(new OpenAI.Chat.ChatClient("gpt-4o-mini", openAiApiKey));
        }
    }
}
