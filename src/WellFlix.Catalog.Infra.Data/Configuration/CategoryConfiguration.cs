using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WellFlix.Catalog.Domain.Entities;

namespace WellFlix.Catalog.Infra.Data.Configuration;

internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(k => k.Id);
        
        builder.Property(p => p.Name)
               .HasMaxLength(255)
               .IsRequired();
        
        builder.Property(p => p.Description)
               .HasMaxLength(10_000);

        builder.Property(p => p.CreatedAt)
               .IsRequired();
    }
}