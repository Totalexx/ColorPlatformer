using RougeBuilder.Exception;

namespace RougeBuilder.Component.Impl;

public class CameraFollows : AbstractComponent
{
    public float NeededDistanceBetweenTrackingObject = 0;
    public float CameraSpeed = 0.05f;

    public override void CheckCorrectComponent()
    {
        if (!HasOwnerComponent<Positional>())
            throw new OwnerHasNotDependentComponentException("Positional is required for CameraFollows");
    }
}