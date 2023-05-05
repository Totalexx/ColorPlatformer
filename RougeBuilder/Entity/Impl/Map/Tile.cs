using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RougeBuilder.Component;
using RougeBuilder.Component.Impl;

namespace RougeBuilder.Model.Impl.Map;

public class Tile : AbstractEntity
{

    public Tile(Vector2 position, Texture2D texture)
    {
        GetComponent<Positional>().Position = position;
        GetComponent<Drawable>().Texture = texture;
    }
    
    protected override IEnumerable<AbstractComponent> InitializeComponents()
    {
        return new AbstractComponent[]
        {
            new Positional(),
            new Drawable(),
        };
    }
}