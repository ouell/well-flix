namespace WellFlix.Catalog.Application.AppService.Category;

public record struct CategoryOutput(Guid Id,
                                    string Name,
                                    string Description,
                                    bool IsActive,
                                    DateTime CreatedAt)
{
    public static CategoryOutput FromCategory(Domain.Entities.Category category)
        => new(
            category.Id,
            category.Name,
            category.Description,
            category.IsActive,
            category.CreatedAt
        );
}