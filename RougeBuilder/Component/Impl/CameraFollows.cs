using Microsoft.Xna.Framework;
using RougeBuilder.Exception;

namespace RougeBuilder.Component.Impl;

public class CameraFollows : AbstractComponent
{
    public float NeededDistanceBetweenTrackingObject = 20;
    public Point MovingAreaWithoutFollowing = new Point(150, 70);
    public float CameraSpeed = 0.15f;

    public override void CheckCorrectComponent()
    {
        if (!HasOwnerComponent<Positional>())
            throw new OwnerHasNotDependentComponentException("Positional is required for CameraFollows");
    }
}