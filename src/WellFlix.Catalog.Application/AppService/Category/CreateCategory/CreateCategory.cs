using WellFlix.Catalog.Domain.Repository;
using WellFlix.Catalog.Infra.CrossCutting.Notification;
using WellFlix.Infra.CrossCutting.Interfaces;
using WellFlix.Infra.CrossCutting.Notification;
using WellFlix.Infra.CrossCutting.Notification.Interfaces;

namespace WellFlix.Catalog.Application.AppService.Category.CreateCategory;

/// <summary>
/// Class responsible for create a category
/// </summary>
public class CreateCategory : ICreateCategory
{
    /// <summary>
    /// Unit of work
    /// </summary>
    private readonly IUnitOfWork _unitOfWork;
    
    /// <summary>
    /// Category repository
    /// </summary>
    private readonly ICategoryRepository _categoryRepository;

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
    /// <param name="input">Create a category input <seealso cref="CreateCategoryInput"/></param>
    /// <param name="cancellationToken">Cancellation Token <seealso cref="CancellationToken"/></param>
    /// <returns>
    ///     IOperation of category output
    /// </returns>
    public async Task<IOperation<CategoryOutput>> Handle(CreateCategoryInput input,
                                                         CancellationToken cancellationToken)
    {
        var category = new Domain.Entities.Category(input.Name,
                                                    input.Description,
                                                    input.IsActive);

        if (!category.IsValid)
        {
            Result.CreateFailure(
                (List<MessagesDetail>) category.Notifications.Select(n => new MessagesDetail(n.Field, n.Message)));
        }

        await _categoryRepository.Insert(category, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return Result.CreateSuccess(CategoryOutput.FromCategory(category));
    }
}