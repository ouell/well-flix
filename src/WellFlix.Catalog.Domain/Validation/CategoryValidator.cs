using FluentValidation;
using WellFlix.Catalog.Domain.Entities;

namespace WellFlix.Catalog.Domain.Validation;

public class CategoryValidator : AbstractValidator<Category>
{
    public static readonly CategoryValidator Instance = new();

    private CategoryValidator()
    {
        RuleFor(x => x.Name)
           .NotNull()
           .NotEmpty()
               .WithMessage("Name should not be empty or null")
           .MinimumLength(3)
               .WithMessage("Name should be at least 3 characters long")
           .MaximumLength(255)
               .WithMessage("Name should be at most 255 characters long");
        
        RuleFor(x => x.Description)
           .NotNull()
           .NotEmpty()
               .WithMessage("Description should not be empty or null")
           .MaximumLength(10_000)
               .WithMessage("Description should be at most 10,000 characters long");
            
    }
}