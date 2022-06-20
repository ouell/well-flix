using WellFlix.Infra.CrossCutting.DomainObjects;

namespace WellFlix.Catalog.Infra.CrossCutting.SeedWork.SearchableRepository;

public class SearchOutput<T> where T : Entity
{
    public int CurrentPage { get; set; }
    public int PerPage { get; set; }
    public int Total { get; set; }
    public IReadOnlyList<T> Items { get; set; }
    
    public SearchOutput(int currentPage, 
                        int perPage, 
                        int total, 
                        IReadOnlyList<T> items)
    {
        CurrentPage = currentPage;
        PerPage = perPage;
        Total = total;
        Items = items;
    }
}