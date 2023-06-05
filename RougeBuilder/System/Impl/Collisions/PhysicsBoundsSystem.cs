using System.Collections.Generic;
using Microsoft.Xna.Framework;
using RougeBuilder.Component.Impl;
using RougeBuilder.Model;

namespace RougeBuilder.System.Impl;

public class PhysicsBoundsSystem : ISystem
{

    private readonly CollisionsCheckSystem _collisionsCheckSystem;

    public PhysicsBoundsSystem(CollisionsCheckSystem collisionsCheckSystem)
    {
        _collisionsCheckSystem = collisionsCheckSystem;
    }

    public void Update(IEnumerable<AbstractEntity> entities)
    {
        var collisions = _collisionsCheckSystem.GetCollisions();
        foreach (var collision in collisions)
        {
            if (collision.Item1.IsStatic && collision.Item2.IsStatic 
                || collision.Item1.IsDynamic && collision.Item2.IsDynamic)
                continue;

            var movableEntity = collision.Item1.IsDynamic 
                ? collision.Item1
                : collision.Item2;

            var staticEntity = collision.Item1.IsDynamic 
                ? collision.Item2
                : collision.Item1;

            newPosition(movableEntity, staticEntity);
        }
    }

    private void newPosition(Collider dynamicEntity, Collider staticEntity)
    {
        var dp = (staticEntity.Bounds.Center - dynamicEntity.Bounds.Center) * 0.012f;
        dynamicEntity.Owner.GetComponent<Movable>().Velocity = dynamicEntity.Owner.GetComponent<Movable>().Velocity - dp;
    }
}