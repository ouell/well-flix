using Microsoft.EntityFrameworkCore;
using WellFlix.Catalog.Domain.Entities;
using WellFlix.Catalog.Domain.Repository;
using WellFlix.Catalog.Infra.CrossCutting.SeedWork.SearchableRepository;

namespace WellFlix.Catalog.Infra.Data.Repository;

public class GenreRepository : IGenreRepository
{
    private readonly WellFlixContext _context;
    private DbSet<Genre> Genres => _context.Set<Genre>();

    public GenreRepository(WellFlixContext context) => _context = context;

    public async Task Insert(Genre aggregate, CancellationToken cancellationToken)
    {
        await Genres.AddAsync(aggregate, cancellationToken);
    }

    public async Task<Genre?> Get(Guid id, CancellationToken cancellationToken)
    {
        return await Genres.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task Delete(Genre aggregate, CancellationToken cancellationToken)
    {
        return Task.FromResult(Genres.Remove(aggregate));
    }

    public Task Update(Genre aggregate, CancellationToken cancellationToken)
    {
        return Task.FromResult(Genres.Update(aggregate));
    }

    public async Task<SearchOutput<Genre>> Search(SearchInput input, CancellationToken cancellationToken)
    {
        var toSkip = (input.Page - 1) * input.PerPage;
        
        var query = Genres.AsNoTracking();
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
    
    private static IQueryable<Genre> AddOrderToQuery(IQueryable<Genre> query,
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