using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using RougeBuilder.Component;
using RougeBuilder.Component.Impl;
using RougeBuilder.Global;

namespace RougeBuilder.Model.Impl;

public class Enemy : AbstractEntity
{
    public Enemy(AbstractEntity target)
    {
        AddComponent(new MoveToTarget(target.GetComponent<Positional>()));
    }

    protected override IEnumerable<AbstractComponent> InitializeComponents()
    {
        var texture = Graphics.Content.Load<Texture2D>("enemy");
        
        return new AbstractComponent[]
        {
            new Positional(300, 400),
            new Drawable(texture),
            new Movable(),
            new Collider(texture.Bounds, Collider.EntityCollisionType.DYNAMIC),
            new Health()
        };
    }
}