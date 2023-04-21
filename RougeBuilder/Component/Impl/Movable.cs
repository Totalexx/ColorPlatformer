using System.Numerics;
using RougeBuilder.Exception;

namespace RougeBuilder.Component.Impl;

public class Movable : AbstractComponent
{
    public Vector2 Velocity { get; set; }

    public override void CheckCorrectComponent()
    {
        if (!HasOwnerComponent<Positional>())
            throw new OwnerHasNotDependentComponentException("Positional is required for Movable");
    }
}