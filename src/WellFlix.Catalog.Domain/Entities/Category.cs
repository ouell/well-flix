using WellFlix.Catalog.Domain.Validation;
using WellFlix.Catalog.Infra.CrossCutting.DomainObjects;

namespace WellFlix.Catalog.Domain.Entities;

public class Category : Entity
{
    public Category(string name, string description, bool isActive = true)
    {
        Name = name;
        Description = description;
        IsActive = isActive;
        
        Validate(this, CategoryValidator.Instance);
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool IsActive { get; private set; }

    public void Activate()
    {
        IsActive = true;
        Validate(this, CategoryValidator.Instance);
    }

    public void Deactivate()
    {
        IsActive = false;
        Validate(this, CategoryValidator.Instance);
    }

    public void Update(string name, string? description = null)
    {
        Name = name;
        Description = description ?? Description;
        Validate(this, CategoryValidator.Instance);
    }
}