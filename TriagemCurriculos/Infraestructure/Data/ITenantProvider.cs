namespace TriagemCurriculos.Infraestructure.Data
{

    using Microsoft.EntityFrameworkCore;
    using TriagemCurriculos.Domain.Entites;
    using TriagemCurriculos.Infraestructure.Configuration;

    public interface ITenantProvider
    {
        string GetTenantId();
    }

    public class ApplicationDbContext : DbContext
    {
        private readonly string _currentTenantId;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ITenantProvider tenantProvider)
            : base(options)
        {
            _currentTenantId = tenantProvider.GetTenantId();
        }

        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<SystemType> SystemTypes { get; set; }
        public DbSet<JobPosition> JobPositions { get; set; }
        public DbSet<Candidate> Candidates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aplica as configurações passando o Tenant atual para as tabelas que precisam de isolamento
            modelBuilder.ApplyConfiguration(new TenantConfiguration());
            modelBuilder.ApplyConfiguration(new SystemTypeConfiguration());
            modelBuilder.ApplyConfiguration(new JobPositionConfiguration(_currentTenantId));
            modelBuilder.ApplyConfiguration(new CandidateConfiguration(_currentTenantId));
        }
    }
}
