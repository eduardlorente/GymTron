
using GymTron.Domain.Events;
using GymTron.Domain.ValueObjects;
using MediatR;

namespace GymTron.Domain.Entities;

public abstract class Entity<TId>
{


    public TId? Id { get; protected set; }
    public EntityStatus Status { get; protected set; } = EntityStatus.New();
    public List<DomainEvent> DomainEvents { get; protected set; } = [];
    


    public Entity()
    {
        Status.Create();
    }


    public Entity(TId id)
    {
        Id = id;
    }


    public async Task RaiseEvents(IMediator eventBus)
    {
        foreach (DomainEvent domainEvent in DomainEvents)
            await eventBus.Publish(domainEvent);
    }


    public void AddDomainEvent(DomainEvent domainEvent)
    {
        DomainEvents.Add(domainEvent);
    }
}
