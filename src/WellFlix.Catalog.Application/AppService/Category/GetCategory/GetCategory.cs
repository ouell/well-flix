using ErrorOr;
using WellFlix.Catalog.Application.Common;
using WellFlix.Catalog.Domain.Repository;

namespace WellFlix.Catalog.Application.AppService.Category.GetCategory;

public class GetCategory : IGetCategory
{
    private readonly ICategoryRepository? _categoryRepository;

    public GetCategory(ICategoryRepository? categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<ErrorOr<CategoryOutput>> Handle(GetCategoryInput input,
                                                      CancellationToken cancellationToken)
    {
        var category = await _categoryRepository?.Get(input.Id, cancellationToken)!;
        if (category == null)
            return Errors.Category.NotFound($"Category not found with id: {input.Id}");

        return CategoryOutput.FromCategory(category);
    }
}