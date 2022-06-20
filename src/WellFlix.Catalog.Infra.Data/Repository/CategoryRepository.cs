using Microsoft.EntityFrameworkCore;
using WellFlix.Catalog.Domain.Entities;
using WellFlix.Catalog.Domain.Repository;
using WellFlix.Catalog.Infra.CrossCutting.SeedWork.SearchableRepository;

namespace WellFlix.Catalog.Infra.Data.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly WellFlixContext _context;
    private DbSet<Category> Categories => _context.Set<Category>();
    
    public CategoryRepository(WellFlixContext context) =>  _context = context; 

    public async Task Insert(Category aggregate, CancellationToken cancellationToken)
    {
        await Categories.AddAsync(aggregate, cancellationToken);
    }

    public async Task<Category?> Get(Guid id, CancellationToken cancellationToken)
    {
        return await Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task Delete(Category aggregate, CancellationToken cancellationToken)
    {
        return Task.FromResult(Categories.Remove(aggregate));
    }

    public Task Update(Category aggregate, CancellationToken cancellationToken)
    {
        return Task.FromResult(Categories.Update(aggregate));
    }

    public async Task<SearchOutput<Category>> Search(SearchInput input, CancellationToken cancellationToken)
    {
        var toSkip = (input.Page - 1) * input.PerPage;
        
        var query = Categories.AsNoTracking();
        query = AddOrderToQuery(query, input.OrderBy, input.Order);
        
        if(!string.IsNullOrWhiteSpace(input.Search))
        {
            query = query.Where(x => x.Name.Contains(input.Search));
        }

        var total = await query.CountAsync(cancellationToken: cancellationToken);
        var items = await query.Skip(toSkip)
                               .Take(input.PerPage)
                               .ToListAsync(cancellationToken: cancellationToken);
        
        return new(input.Page, input.PerPage, total, items);
    }

    public Task<IReadOnlyList<Guid>> GetIdsListByIds(List<Guid> ids, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    
    private static IQueryable<Category> AddOrderToQuery(IQueryable<Category> query,
                                                        string orderProperty,
                                                        SearchOrder order
    )
    {
        var orderedQuery = (orderProperty.ToLower(), order) switch
        {
            ("name", SearchOrder.Asc) => query.OrderBy(x => x.Name).ThenBy(x => x.Id),
            ("name", SearchOrder.Desc) => query.OrderByDescending(x => x.Name).ThenByDescending(x => x.Id),
            ("id", SearchOrder.Asc) => query.OrderBy(x => x.Id),
            ("id", SearchOrder.Desc) => query.OrderByDescending(x => x.Id),
            ("createdat", SearchOrder.Asc) => query.OrderBy(x => x.CreatedAt),
            ("createdat", SearchOrder.Desc) => query.OrderByDescending(x => x.CreatedAt),
            _ => query.OrderBy(x => x.Name).ThenBy(x => x.Id)
        };
        
        return orderedQuery;
    }
}