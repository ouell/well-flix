using MediatR;
using ErrorOr;

namespace WellFlix.Catalog.Application.AppService.Category.CreateCategory;

public interface ICreateCategory : IRequestHandler<CreateCategoryInput, ErrorOr<CategoryOutput>>
{
}