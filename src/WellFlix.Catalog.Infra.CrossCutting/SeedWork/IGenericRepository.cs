using WellFlix.Infra.CrossCutting.DomainObjects;

namespace WellFlix.Catalog.Infra.CrossCutting.SeedWork;

public interface IGenericRepository<T> : IRepository where T : Entity
{
    public Task Insert(T aggregate, CancellationToken cancellationToken);
    public Task<T?> Get(Guid id, CancellationToken cancellationToken);
    public Task Delete(T aggregate, CancellationToken cancellationToken);
    public Task Update(T aggregate, CancellationToken cancellationToken);
}