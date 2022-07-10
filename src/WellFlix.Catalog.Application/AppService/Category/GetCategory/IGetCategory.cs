using ErrorOr;
using MediatR;

namespace WellFlix.Catalog.Application.AppService.Category.GetCategory;

public interface IGetCategory : IRequestHandler<GetCategoryInput, ErrorOr<CategoryOutput>>
{
}