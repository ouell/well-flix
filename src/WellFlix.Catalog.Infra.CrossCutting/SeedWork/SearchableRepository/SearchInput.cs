namespace WellFlix.Catalog.Infra.CrossCutting.SeedWork.SearchableRepository;

public record struct SearchInput(int Page, int PerPage, string Search, string OrderBy, SearchOrder Order);