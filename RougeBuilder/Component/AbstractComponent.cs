using RougeBuilder.Entity;
using RougeBuilder.Model;

namespace RougeBuilder.Component;

public abstract class AbstractComponent
{
    public AbstractEntity Owner { get; set; }

    public virtual void CheckCorrectComponent() {}

    protected bool HasOwnerComponent<OtherComponent>() where OtherComponent : AbstractComponent
    {
        return Owner.HasComponent<OtherComponent>();
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        return GetHashCode() == obj.GetHashCode();
    }

    public override int GetHashCode()
    {
        return (Owner != null ? Owner.GetHashCode() : 0) + GetType().GetHashCode() * 37;
    }
}