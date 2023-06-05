using MonoGame.Extended;

namespace RougeBuilder.Component.Impl;

public class Collider : AbstractComponent
{
    public RectangleF Bounds
    {
        get
        {
            if (IsStatic) return _bounds;
            var positional = Owner.GetComponent<Positional>();
            return new RectangleF(positional.Position.ToSize() - _bounds.Size  / 2, _bounds.Size);
        }
    }

    private readonly RectangleF _bounds;
    
    public EntityCollisionType CollisionType { get; set; }

    public Collider(RectangleF bounds, EntityCollisionType collisionType)
    {
        _bounds = bounds;
        CollisionType = collisionType;
    }

    public bool IsStatic => CollisionType == EntityCollisionType.STATIC;
    public bool IsDynamic => CollisionType == EntityCollisionType.DYNAMIC;
    
    public enum EntityCollisionType
    {
        STATIC,
        DYNAMIC
    }
}