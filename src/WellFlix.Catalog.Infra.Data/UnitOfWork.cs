using WellFlix.Infra.CrossCutting.Interfaces;

namespace WellFlix.Catalog.Infra.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly WellFlixContext _context;
    
    public UnitOfWork(WellFlixContext context) => _context = context;

    public Task Commit(CancellationToken cancellationToken) => _context.SaveChangesAsync(cancellationToken);

    public Task Rollback(CancellationToken cancellationToken) => Task.CompletedTask;
}