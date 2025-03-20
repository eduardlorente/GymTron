using MediatR;

namespace GymTron.Domain.Events;

public class DomainEvent(Guid correlationId) : INotification
{


    public Guid CorrelationId { get; private set; } = correlationId;
    public DateTime OccurredOn { get; private set; } = DateTime.Now;
}
