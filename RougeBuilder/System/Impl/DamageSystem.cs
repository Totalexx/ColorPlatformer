using System.Collections.Generic;
using RougeBuilder.Component.Impl;
using RougeBuilder.Model;
using RougeBuilder.System.Impl;
using NotImplementedException = System.NotImplementedException;

namespace RougeBuilder.System;

public class DamageSystem : ISystem
{
    private readonly CollisionsCheckSystem _collisionsCheckSystem;

    public DamageSystem(CollisionsCheckSystem collisionsCheckSystem)
    {
        _collisionsCheckSystem = collisionsCheckSystem;
    }

    public void Update(IEnumerable<AbstractEntity> entities)
    {
        var collisions = _collisionsCheckSystem.GetCollisions();
        foreach (var collision in collisions)
        {
            if (collision.Item1.IsStatic && collision.Item2.IsStatic)
                continue;

            if (collision.Item1.Owner.HasComponent<DamageDealer>() && collision.Item2.Owner.HasComponent<Health>()
                || collision.Item2.Owner.HasComponent<DamageDealer>() && collision.Item1.Owner.HasComponent<Health>())
            {
                var firstIsDamageDealer = collision.Item1.Owner.HasComponent<DamageDealer>();
                var damageDealer = firstIsDamageDealer ? collision.Item1.Owner : collision.Item2.Owner;
                var damageTaker = firstIsDamageDealer ? collision.Item2.Owner : collision.Item1.Owner;

                damageTaker.GetComponent<Health>().health -= damageDealer.GetComponent<DamageDealer>().damage;
            }
        }
    }
}