using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using RougeBuilder.Component;
using RougeBuilder.Component.Impl;
using RougeBuilder.Entity;
using RougeBuilder.Global;

namespace RougeBuilder.Model;

public class Bullet : AbstractEntity
{
    protected override IEnumerable<AbstractComponent> InitializeComponents()
    {
        var texture = Graphics.Content.Load<Texture2D>("bullet");

        return new AbstractComponent[]
        {
            new Positional(-1, -1),
            new Drawable(texture),
            new Movable(),
            new Collider(texture.Bounds, Collider.EntityCollisionType.DYNAMIC),
            new DamageDealer(30, new HashSet<Type>{ typeof(PlayerControllable), typeof(MissileMarker) }),
            new MissileMarker(),
            new Health(5)
        };
    }
}