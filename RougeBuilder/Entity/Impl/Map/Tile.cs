using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RougeBuilder.Component;
using RougeBuilder.Component.Impl;
using RougeBuilder.System.Impl;

namespace RougeBuilder.Model.Impl.Map;

public class Tile : AbstractEntity
{

    public Tile(Vector2 position, Texture2D texture)
    {
        GetComponent<Positional>().Position = position * MapTiles.TileSize;
        
        var drawable = GetComponent<Drawable>();
        drawable.Texture = texture;
        drawable.LayerDepth = 0.1f;
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