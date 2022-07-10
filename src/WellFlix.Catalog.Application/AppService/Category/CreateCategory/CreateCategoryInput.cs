using MediatR;
using ErrorOr;

namespace WellFlix.Catalog.Application.AppService.Category.CreateCategory;

public record struct CreateCategoryInput(string Name, string Description, bool IsActive) :
    IRequest<ErrorOr<CategoryOutput>>;