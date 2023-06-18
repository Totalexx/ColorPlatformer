using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using RougeBuilder.Component;
using RougeBuilder.Component.Impl;
using RougeBuilder.Global;
using RougeBuilder.Model;
using NotImplementedException = System.NotImplementedException;

namespace RougeBuilder.Entity.Impl;

public class WinChest : AbstractEntity
{
    protected override IEnumerable<AbstractComponent> InitializeComponents()
    {
        var texture = Graphics.Content.Load<Texture2D>("closedChest");

        return new AbstractComponent[]
        {
            new Positional(),
            new Drawable(texture),
            new Collider(texture.Bounds, Collider.EntityCollisionType.DYNAMIC),
            new WinMarker()
        };
    }
}