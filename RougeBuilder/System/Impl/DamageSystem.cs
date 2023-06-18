using System.Collections.Generic;
using System.Linq;
using RougeBuilder.Component.Impl;
using RougeBuilder.Model;
using RougeBuilder.System.Impl;
using Enumerable = System.Linq.Enumerable;
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
                var secondIsDamageDealer = collision.Item2.Owner.HasComponent<DamageDealer>();

                if (firstIsDamageDealer && secondIsDamageDealer)
                {
                    TakeDamage(collision.Item1.Owner, collision.Item2.Owner);
                    TakeDamage(collision.Item2.Owner, collision.Item1.Owner);
                    continue;
                }
                
                var damageDealer = firstIsDamageDealer ? collision.Item1.Owner : collision.Item2.Owner;
                var damageTaker = firstIsDamageDealer ? collision.Item2.Owner : collision.Item1.Owner;

                TakeDamage(damageDealer, damageTaker);
            }
        }
    }

    private void TakeDamage(AbstractEntity damageDealer, AbstractEntity damageTaker)
    {
        var damageTakerComponents = damageTaker.GetAllComponents().Select(p => p.Key);
        var goNext = damageTakerComponents.Any(component => damageDealer.GetComponent<DamageDealer>().IgnoreComponents.Contains(component));

        if (goNext)
            return;
                
        if (damageTaker.HasComponent<Health>())
            damageTaker.GetComponent<Health>().HealthSize -= damageDealer.GetComponent<DamageDealer>().Damage;
    }
}