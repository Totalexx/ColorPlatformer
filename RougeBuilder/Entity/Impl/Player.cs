using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using RougeBuilder.Component;
using RougeBuilder.Component.Impl;
using RougeBuilder.Global;

namespace RougeBuilder.Model.Impl;

public class Player : AbstractEntity
{
    protected override IEnumerable<AbstractComponent> InitializeComponents()
    {
        var texture = Graphics.Content.Load<Texture2D>("player");
        
        return new AbstractComponent[]
        {
            new Positional(100, 100),
            new Drawable(texture),
            new Movable(),
            new PlayerControllable(),
            new CameraFollows(),
            // new Collider(new RectangleF());
        };
    }
}