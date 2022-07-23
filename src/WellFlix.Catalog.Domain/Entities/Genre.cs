using WellFlix.Catalog.Domain.Validation;
using WellFlix.Catalog.Infra.CrossCutting.DomainObjects;

namespace WellFlix.Catalog.Domain.Entities;

public class Genre : Entity
{
    public string Name { get; private set; }
    public bool IsActive { get; private set; }

    private List<Guid> _categories;
    public IReadOnlyList<Guid> Categories => _categories.AsReadOnly();

    public Genre(string name, bool isActive = true)
    {
        Name = name;
        IsActive = isActive;
        _categories = new List<Guid>();
        
        Validate(this, GenreValidator.Instance);
    }
    
    public void Activate()
    {
        IsActive = true;
        Validate(this, GenreValidator.Instance);
    }

    public void Deactivate()
    {
        IsActive = false;
        Validate(this, GenreValidator.Instance);
    }
    
    public void Update(string name)
    {
        Name = name;
        Validate(this, GenreValidator.Instance);
    }

    public void AddCategory(Guid categoryId)
    {
        _categories.Add(categoryId);
        Validate(this, GenreValidator.Instance);
    }

    public void RemoveCategory(Guid categoryId)
    {
        _categories.Remove(categoryId);
        Validate(this, GenreValidator.Instance);
    }

    public void RemoveAllCategories()
    {
        _categories.Clear();
        Validate(this, GenreValidator.Instance);
    }
}