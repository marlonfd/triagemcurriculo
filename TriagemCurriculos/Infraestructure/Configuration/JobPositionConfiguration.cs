using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TriagemCurriculos.Domain.Entites;

namespace TriagemCurriculos.Infraestructure.Configuration
{
    public class JobPositionConfiguration : IEntityTypeConfiguration<JobPosition>
    {
        public void Configure(EntityTypeBuilder<JobPosition> builder)
        {
            builder.ToTable("job_positions");

            builder.HasKey(j => j.Id);
            builder.Property(j => j.Id).HasColumnName("id").ValueGeneratedOnAdd();

            builder.Property(j => j.TenantId)
                .HasColumnName("tenant_id")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(j => j.Title)
                .HasColumnName("title")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(j => j.Description)
                .HasColumnName("description")
                .IsRequired()
                .HasColumnType("text");

            builder.Property(j => j.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Relacionamentos
            builder.HasOne(j => j.Tenant)
                .WithMany(t => t.JobPositions)
                .HasForeignKey(j => j.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            // Índices
            builder.HasIndex(j => j.TenantId).HasDatabaseName("idx_tenant_job");
        }
    }
}
