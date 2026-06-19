using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TriagemCurriculos.Domain.Entites;

namespace TriagemCurriculos.Infraestructure.Configuration
{
    public class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
    {
        private readonly string _tenantId;

        public CandidateConfiguration(string tenantId)
        {
            _tenantId = tenantId;
        }

        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
            builder.ToTable("candidates");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("id").ValueGeneratedOnAdd();

            builder.Property(c => c.TenantId).HasColumnName("tenant_id").IsRequired().HasMaxLength(50);
            builder.Property(c => c.JobPositionId).HasColumnName("job_position_id").IsRequired();
            builder.Property(c => c.StatusTypeId).HasColumnName("status_type_id").IsRequired();

            builder.Property(c => c.CandidateName).HasColumnName("candidate_name").IsRequired().HasMaxLength(100);
            builder.Property(c => c.CandidateEmail).HasColumnName("candidate_email").IsRequired().HasMaxLength(150);

            // Mapeamento do campo JSON string do banco
            builder.Property(c => c.ExtractedSkillsJson)
                .HasColumnName("extracted_skills")
                .HasColumnType("json"); // MySQL 8.4

            builder.Property(c => c.AiMatchScore).HasColumnName("ai_match_score");
            builder.Property(c => c.AiAnalysisSummary).HasColumnName("ai_analysis_summary").HasColumnType("text");
            builder.Property(c => c.ResumeFileUrl).HasColumnName("resume_file_url").HasMaxLength(255);

            builder.Property(c => c.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Relacionamentos (FKs)
            builder.HasOne(c => c.Tenant)
                .WithMany(t => t.Candidates)
                .HasForeignKey(c => c.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.JobPosition)
                .WithMany(j => j.Candidates)
                .HasForeignKey(c => c.JobPositionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.StatusType)
                .WithMany() 
                .HasForeignKey(c => c.StatusTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(c => new { c.TenantId, c.JobPositionId, c.AiMatchScore })
                .HasDatabaseName("idx_tenant_candidate_search");

            // Filtro global de Multi-tenancy
            builder.HasQueryFilter(c => c.TenantId == _tenantId);
        }
    }
}
