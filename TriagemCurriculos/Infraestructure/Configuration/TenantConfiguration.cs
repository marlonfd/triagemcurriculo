using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TriagemCurriculos.Domain.Entites;
using TriagemCurriculos.Domain.Enums;

namespace TriagemCurriculos.Infraestructure.Configuration
{
    public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.ToTable("tenants");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("id")
                .HasMaxLength(50);

            builder.Property(t => t.CompanyName)
                .HasColumnName("company_name")
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(t => t.PlanTier)
                .HasColumnName("plan_tier")
                .HasMaxLength(20)
                .HasConversion<string>() // Salva o Enum como String no banco (FREE, PREMIUM...)
                .HasDefaultValue(PlanTier.Free);

            builder.Property(t => t.IsActive)
                .HasColumnName("is_active")
                .HasDefaultValue(true);

            builder.Property(t => t.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
