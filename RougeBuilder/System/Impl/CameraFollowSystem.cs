using MonoGame.Extended;
using RougeBuilder.Component.Impl;
using RougeBuilder.Global;
using RougeBuilder.Model;
using NotImplementedException = System.NotImplementedException;

namespace RougeBuilder.System.Impl;

public class CameraFollowSystem : AbstractSystem<CameraFollows>
{
    protected override void UpdateEntity(AbstractEntity entity)
    {
        var cameraFollows = entity.GetComponent<CameraFollows>();
        var entityPosition = entity.GetComponent<Positional>();

        var distanceBetweenCameraEntity = Graphics.Camera.Center - entityPosition.Position;
        var distanceDiff = distanceBetweenCameraEntity.Length() - cameraFollows.NeededDistanceBetweenTrackingObject;

        var moveOn = -distanceBetweenCameraEntity.NormalizedCopy() * distanceDiff * cameraFollows.CameraSpeed;
        
        Graphics.Camera.Move(moveOn);
    }
}