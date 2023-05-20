using MonoGame.Extended;

namespace RougeBuilder.Component.Impl;

public class Collider : AbstractComponent
{
    public RectangleF Bounds { get; set; }
    public EntityCollisionType CollisionType { get; set; }

    public Collider(RectangleF bounds, EntityCollisionType collisionType)
    {
        Bounds = bounds;
        CollisionType = collisionType;
    }

    public enum EntityCollisionType
    {
        STATIC,
        DYNAMIC
    }
}