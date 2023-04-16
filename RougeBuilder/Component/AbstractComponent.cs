using RougeBuilder.Model;

namespace RougeBuilder.Component;

public abstract class AbstractComponent
{
    private AbstractEntity owner;

    protected bool HasOwnerComponent<OtherComponent>() where OtherComponent : AbstractComponent
    {
        return owner.HasComponent<OtherComponent>();
    }
}