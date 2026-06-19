using Microsoft.EntityFrameworkCore;
using RecrutamentoIA.Domain.Common;
using System.Linq.Expressions;
using System.Reflection;
using TriagemCurriculos.Domain.Entites;
using TriagemCurriculos.Infraestructure.Data;
using TriagemCurriculos.Infraestructure.Interface;

namespace TriagemCurriculos.Infraestructure
{
    public class MainDbContext : DbContext
    {
        private readonly string _currentTenantId;

        public MainDbContext(DbContextOptions<MainDbContext> options, ITenantProvider tenantProvider) : base(options)
        {
            _currentTenantId = tenantProvider.GetTenantId();
        }

        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<SystemType> SystemTypes { get; set; }
        public DbSet<JobPosition> JobPositions { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfiguration(new TriagemCurriculos.Infraestructure.Configuration.TenantConfiguration());
            modelBuilder.ApplyConfiguration(new TriagemCurriculos.Infraestructure.Configuration.JobPositionConfiguration(_currentTenantId));
            modelBuilder.ApplyConfiguration(new TriagemCurriculos.Infraestructure.Configuration.SystemTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TriagemCurriculos.Infraestructure.Configuration.CandidateConfiguration(_currentTenantId));
            modelBuilder.ApplyConfiguration(new TriagemCurriculos.Infraestructure.Configuration.UserConfiguration(_currentTenantId));
        }
    }
}
