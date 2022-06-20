using WellFlix.Catalog.Domain.Entities;
using WellFlix.Catalog.Infra.CrossCutting.SeedWork;
using WellFlix.Catalog.Infra.CrossCutting.SeedWork.SearchableRepository;

namespace WellFlix.Catalog.Domain.Repository;

public interface IGenreRepository : IGenericRepository<Genre>,
                                    ISearchableRepository<Genre>
{
}