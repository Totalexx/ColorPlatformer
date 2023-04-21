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
}