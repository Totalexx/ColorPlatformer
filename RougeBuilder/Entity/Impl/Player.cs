using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using RougeBuilder.Component;
using RougeBuilder.Component.Impl;
using RougeBuilder.Global;

namespace RougeBuilder.Model.Impl;

public class Player : AbstractEntity
{
    public Player()
    {
        var texture = GlobalHolder.Content.Load<Texture2D>("player");
        
        List<AbstractComponent> components = new () 
        {
            new Positional(50, 50),
            new Drawable(texture),
            new Movable(),
            new PlayerControllable(),
        };
        
        AddComponents(components);
    }
}