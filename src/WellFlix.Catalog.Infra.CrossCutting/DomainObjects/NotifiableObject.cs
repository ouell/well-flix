using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;
using FluentValidation.Results;

namespace WellFlix.Catalog.Infra.CrossCutting.DomainObjects;

public abstract class NotifiableObject
{
    [NotMapped]
    public bool IsValid { get; private set; }
    [NotMapped]
    public string Messages { get; private set; }
    
    public void Validate<TModel>(TModel model, AbstractValidator<TModel> validator)
    {
        var validationResult = validator.Validate(model);
        IsValid = validationResult.IsValid;
        Messages = string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage));
    }
}