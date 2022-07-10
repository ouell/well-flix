using ErrorOr;
using WellFlix.Catalog.Application.Common;
using WellFlix.Catalog.Domain.Repository;
using WellFlix.Catalog.Infra.CrossCutting.Interfaces;

namespace WellFlix.Catalog.Application.AppService.Category.CreateCategory;

/// <summary>
/// Class responsible for create a category
/// </summary>
public class CreateCategory : ICreateCategory
{
    /// <summary>
    /// Category repository
    /// </summary>
    private readonly ICategoryRepository _categoryRepository;

    /// <summary>
    /// Unit of work
    /// </summary>
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="unitOfWork">Unit of work</param>
    /// <param name="categoryRepository">Category repository</param>
    public CreateCategory(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
    {
        _unitOfWork = unitOfWork;
        _categoryRepository = categoryRepository;
    }

    /// <summary>
    /// Handle responsible for create a category
    /// </summary>
    /// <param name="input">Create a category input <seealso cref="CreateCategoryInput" /></param>
    /// <param name="cancellationToken">Cancellation Token <seealso cref="CancellationToken" /></param>
    /// <returns>
    /// IOperation of category output
    /// </returns>
    public async Task<ErrorOr<CategoryOutput>> Handle(CreateCategoryInput input,
                                                      CancellationToken cancellationToken)
    {
        var category = new Domain.Entities.Category(input.Name,
                                                    input.Description,
                                                    input.IsActive);

        if (!category.IsValid)
            return Errors.Category.InvalidCategory(category.Messages);

        await _categoryRepository.Insert(category, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return CategoryOutput.FromCategory(category);
    }
}