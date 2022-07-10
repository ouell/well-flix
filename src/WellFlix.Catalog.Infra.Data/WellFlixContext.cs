using Microsoft.EntityFrameworkCore;
using WellFlix.Catalog.Domain.Entities;
using WellFlix.Catalog.Infra.Data.Configuration;

namespace WellFlix.Catalog.Infra.Data;

public class WellFlixContext : DbContext
{
    public WellFlixContext(DbContextOptions<WellFlixContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new GenreConfiguration());

        modelBuilder.ApplyConfiguration(new GenresCategoriesConfiguration());
    }
}