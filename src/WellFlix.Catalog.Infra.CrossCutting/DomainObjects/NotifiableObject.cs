using FluentValidation;
using FluentValidation.Results;

namespace WellFlix.Catalog.Infra.CrossCutting.DomainObjects;

public abstract class NotifiableObject
{
    public bool IsValid { get; private set; }

    public IEnumerable<WellFlix.Infra.CrossCutting.DomainObjects.Notification> Notifications { get; private set; }

    public void Validate<TModel>(TModel model, AbstractValidator<TModel> validator)
    {
        var validationResult = validator.Validate(model);
        IsValid = validationResult.IsValid;
        Notifications = validationResult.Errors.Select<ValidationFailure, WellFlix.Infra.CrossCutting.DomainObjects.Notification>(error => new WellFlix.Infra.CrossCutting.DomainObjects.Notification(error.PropertyName, error.ErrorMessage, error.AttemptedValue));
    }
}