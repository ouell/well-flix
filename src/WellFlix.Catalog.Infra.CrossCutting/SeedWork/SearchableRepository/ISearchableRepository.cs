using WellFlix.Infra.CrossCutting.DomainObjects;

namespace WellFlix.Catalog.Infra.CrossCutting.SeedWork.SearchableRepository;

public interface ISearchableRepository<T> where T : Entity
{
    Task<SearchOutput<T>> Search(
        SearchInput input,
        CancellationToken cancellationToken
    );
}