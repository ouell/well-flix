using MediatR;
using Microsoft.AspNetCore.Mvc;
using WellFlix.Catalog.Application.AppService.Category;
using WellFlix.Catalog.Application.AppService.Category.CreateCategory;
using WellFlix.Catalog.Application.AppService.Category.GetCategory;

namespace WellFlix.Catalog.Api.Controllers;

[Route("[controller]")]
public class CategoryController : ApiController
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CategoryOutput), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateCategoryInput input,
                                            CancellationToken cancellationToken)
    {
        var category = await _mediator.Send(input, cancellationToken);
        
        return category.Match(
            success => Created(success.Id.ToString(), success),
            Problem
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id,
                                             CancellationToken cancellationToken)
    {
        var category = await _mediator.Send(new GetCategoryInput(id), cancellationToken);

        return category.Match(
            success => Ok(success),
            Problem
        );
    }
}