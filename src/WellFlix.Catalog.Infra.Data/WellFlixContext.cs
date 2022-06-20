using Microsoft.EntityFrameworkCore;
using WellFlix.Catalog.Domain.Entities;

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
    }
}