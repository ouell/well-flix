using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WellFlix.Catalog.Infra.Data.Models;

namespace WellFlix.Catalog.Infra.Data.Configuration;

internal class GenresCategoriesConfiguration : IEntityTypeConfiguration<GenresCategories>
{
    public void Configure(EntityTypeBuilder<GenresCategories> builder)
    {
        builder.HasKey(relation => new
        {
            relation.CategoryId,
            relation.GenreId
        });
    }
}