namespace WellFlix.Catalog.Application.AppService.Category.CreateCategory;

public record struct CreateCategoryInput(string Name, string Description, bool IsActive);