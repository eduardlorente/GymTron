using GymTron.Domain.Enums;

namespace GymTron.Domain.ValueObjects;

public class EntityStatus : ValueObject<EntityStatus>
{


    public EntityStatusTypes Status { get; private set; }
    public DateTime CreatedOn { get; protected set; }
    public DateTime? ModifiedOn { get; protected set; } = null;
    public DateTime? DeletedOn { get; protected set; } = null;
    public bool IsActive { get => Status == EntityStatusTypes.ACTIVE; }
    public bool IsDeleted { get => Status == EntityStatusTypes.DELETED; }
    public bool IsCompleted { get => Status == EntityStatusTypes.COMPLETED; }


    private EntityStatus(EntityStatusTypes status, DateTime createdOn)
    {
        Status = status;
        CreatedOn = createdOn;
    }


    protected override bool EqualsCore(EntityStatus? other)
    {
        EntityStatus? valueObject = other;

        return Status == valueObject?.Status
            && CreatedOn == valueObject?.CreatedOn
            && ModifiedOn == valueObject?.ModifiedOn
            && DeletedOn == valueObject?.DeletedOn;
    }


    protected override int GetHashCodeCore()
    {
        return Status.GetHashCode()
            ^ CreatedOn.GetHashCode()
            ^ ModifiedOn.GetHashCode()
            ^ DeletedOn.GetHashCode();
    }


    internal void Create()
    {
        Status = EntityStatusTypes.ACTIVE;
        CreatedOn = DateTime.Now;
    }


    internal static EntityStatus New()
    {
        return new EntityStatus(EntityStatusTypes.ACTIVE, DateTime.Now);
    }


    internal static EntityStatus FromDatabase(DateTime createdOn)
    {
        return new EntityStatus(EntityStatusTypes.ACTIVE, createdOn);
    }


    internal static EntityStatus FromDatabase(EntityStatusTypes status, DateTime createdOn)
    {
        return new EntityStatus(status, createdOn);
    }


    internal void Update()
    {
        ModifiedOn = DateTime.Now;
    }


    internal void Update(EntityStatusTypes statusType)
    {
        ModifiedOn = DateTime.Now;
        Status = statusType;
    }


    internal void Delete()
    {
        Status = EntityStatusTypes.DELETED;
        DeletedOn = DateTime.Now;
    }
}
