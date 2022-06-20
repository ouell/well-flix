using FluentValidation;
using WellFlix.Catalog.Domain.Entities;

namespace WellFlix.Catalog.Domain.Validation;

public class GenreValidator : AbstractValidator<Genre>
{
    public static readonly GenreValidator Instance = new();

    public GenreValidator()
    {
        RuleFor(x => x.Name)
           .NotNull()
           .NotEmpty()
               .WithMessage("Name should not be empty or null");
    }
}