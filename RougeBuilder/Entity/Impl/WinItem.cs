using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using RougeBuilder.Component;
using RougeBuilder.Component.Impl;
using RougeBuilder.Global;
using RougeBuilder.Model;
using NotImplementedException = System.NotImplementedException;

namespace RougeBuilder.Entity.Impl;

public class WinItem : AbstractEntity
{
    protected override IEnumerable<AbstractComponent> InitializeComponents()
    {
        var texture = Graphics.Content.Load<Texture2D>("chest");

        return new AbstractComponent[]
        {
            new Positional(100, 100),
            new Drawable(texture),
            new Collider(texture.Bounds, Collider.EntityCollisionType.DYNAMIC),
            new WinMarker()
        };
    }
}