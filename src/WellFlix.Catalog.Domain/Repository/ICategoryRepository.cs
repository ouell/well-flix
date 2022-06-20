using WellFlix.Catalog.Domain.Entities;
using WellFlix.Catalog.Infra.CrossCutting.SeedWork;
using WellFlix.Catalog.Infra.CrossCutting.SeedWork.SearchableRepository;

namespace WellFlix.Catalog.Domain.Repository;

public interface ICategoryRepository : IGenericRepository<Category>,
                                       ISearchableRepository<Category>
{
    public Task<IReadOnlyList<Guid>> GetIdsListByIds(List<Guid> ids,
                                                     CancellationToken cancellationToken);
}