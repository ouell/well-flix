namespace WellFlix.Catalog.Infra.CrossCutting.DomainObjects;

public abstract class Entity : NotifiableObject
{
    public Guid Id { get; }
    public DateTime CreatedAt { get; }

    protected Entity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
    }
}