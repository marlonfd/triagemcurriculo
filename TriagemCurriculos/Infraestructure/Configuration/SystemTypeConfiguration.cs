using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TriagemCurriculos.Domain.Entites;

namespace TriagemCurriculos.Infraestructure.Configuration
{
    public class SystemTypeConfiguration : IEntityTypeConfiguration<SystemType>
    {
        public void Configure(EntityTypeBuilder<SystemType> builder)
        {
            builder.ToTable("system_types");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(s => s.Category)
                .HasColumnName("category")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.Code)
                .HasColumnName("code")
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(s => s.DisplayName)
                .HasColumnName("display_name")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.IsActive)
                .HasColumnName("is_active")
                .HasDefaultValue(true);

            builder.HasIndex(s => new { s.Category, s.Code })
                .IsUnique()
                .HasDatabaseName("uq_category_code");
        }
    }
}
