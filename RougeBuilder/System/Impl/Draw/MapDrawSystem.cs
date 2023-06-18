using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RougeBuilder.Component.Impl;
using RougeBuilder.Entity;
using RougeBuilder.Global;
using RougeBuilder.Model;
using RougeBuilder.Model.Impl.Map;

namespace RougeBuilder.System.Impl;

public class MapDrawSystem : AbstractSystem<MapMarker>
{

    private readonly SpriteBatch mapSpriteBatch = Graphics.SpriteBatch;

    protected override void UpdateEntity(AbstractEntity entity)
    {
        var tiles = entity.GetComponent<EntityCollector<Tile>>().Collection;

        foreach (var tile in tiles)
        {
            var positional = tile.GetComponent<Positional>();
            var drawable = tile.GetComponent<Drawable>();
            
            mapSpriteBatch.Draw(
                drawable.Texture, 
                positional.Position, 
                null, 
                Color.White, 
                0, 
                new Vector2(0, 0), 
                Vector2.One, 
                SpriteEffects.None, 
                drawable.LayerDepth);
        }
    }
}