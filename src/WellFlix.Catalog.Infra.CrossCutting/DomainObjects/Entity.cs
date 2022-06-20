using WellFlix.Catalog.Infra.CrossCutting.DomainObjects;
using WellFlix.Infra.CrossCutting.Message;

namespace WellFlix.Infra.CrossCutting.DomainObjects;

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