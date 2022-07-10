using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WellFlix.Catalog.Domain.Entities;

namespace WellFlix.Catalog.Infra.Data.Configuration;

internal class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.HasKey(k => k.Id);

        builder.Property(p => p.CreatedAt)
               .IsRequired();
        
        builder.Property(p => p.Name)
               .HasMaxLength(255)
               .IsRequired();
    }
}