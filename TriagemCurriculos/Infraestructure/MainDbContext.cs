using Microsoft.EntityFrameworkCore;
using RecrutamentoIA.Domain.Common;
using System.Linq.Expressions;
using System.Reflection;
using TriagemCurriculos.Domain.Entites;
using TriagemCurriculos.Infraestructure.Configuration;
using TriagemCurriculos.Infraestructure.Data;
using TriagemCurriculos.Infraestructure.Interface;

namespace TriagemCurriculos.Infraestructure
{
    public class MainDbContext : DbContext
    {
        private readonly ITenantProvider _tenantProvider;

        public string CurrentTenantId => _tenantProvider.GetTenantId();

        public MainDbContext(DbContextOptions<MainDbContext> options, ITenantProvider tenantProvider) : base(options)
        {
            _tenantProvider = tenantProvider;
        }

        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<SystemType> SystemTypes { get; set; }
        public DbSet<JobPosition> JobPositions { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfiguration(new TenantConfiguration());
            modelBuilder.ApplyConfiguration(new JobPositionConfiguration());
            modelBuilder.ApplyConfiguration(new SystemTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CandidateConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            // Global Query Filters (Dynamic evaluation based on CurrentTenantId)
            modelBuilder.Entity<JobPosition>().HasQueryFilter(x => x.TenantId == CurrentTenantId);
            modelBuilder.Entity<Candidate>().HasQueryFilter(x => x.TenantId == CurrentTenantId);
            modelBuilder.Entity<User>().HasQueryFilter(x => x.TenantId == CurrentTenantId);
        }
    }
}
