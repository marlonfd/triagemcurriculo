using TriagemCurriculos.Repositories;
using TriagemCurriculos.Repositories.Interface;

namespace TriagemCurriculos.Infraestructure
{
    public static class ConfigureRepositories
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICandidateRepository, CandidateRepository>();
            services.AddScoped<IJobPositionRepository, JobPositionRepository>();
            services.AddScoped<ISystemTypeRepository, SystemTypeRepository>();
            services.AddScoped<ITenantRepository, TenantRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
