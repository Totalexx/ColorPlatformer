using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using RougeBuilder.Component;
using RougeBuilder.Component.Impl;
using RougeBuilder.Global;
using RougeBuilder.Model;

namespace RougeBuilder.Entity.Impl;

public class ChestKey : AbstractEntity
{
    protected override IEnumerable<AbstractComponent> InitializeComponents()
    {
        var texture = Graphics.Content.Load<Texture2D>("key");

        return new AbstractComponent[]
        {
            new Positional(-1, -1),
            new Drawable(texture),
            new Collider(texture.Bounds, Collider.EntityCollisionType.DYNAMIC),
            new KeyMarker()
        };
    }
}