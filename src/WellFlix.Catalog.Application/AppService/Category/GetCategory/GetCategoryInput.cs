using MediatR;
using ErrorOr;

namespace WellFlix.Catalog.Application.AppService.Category.GetCategory;

public record struct GetCategoryInput(Guid Id) : IRequest<ErrorOr<CategoryOutput>>;