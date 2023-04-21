using RougeBuilder.Exception;

namespace RougeBuilder.Component.Impl;

public class PlayerControllable : AbstractComponent
{
    public override void CheckCorrectComponent()
    {
        if (!HasOwnerComponent<Movable>())
            throw new OwnerHasNotDependentComponentException("Movable required for PlayerControllable");
    }
}