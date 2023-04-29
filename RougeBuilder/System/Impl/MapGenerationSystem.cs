using System.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RougeBuilder.Component.Impl;
using RougeBuilder.Model;
using RougeBuilder.Model.Impl.Map;
using Graphics = RougeBuilder.Global.Graphics;

namespace RougeBuilder.System.Impl;

public class MapGenerationSystem : AbstractSystem<MapMarker>
{
    protected override void UpdateEntity(AbstractEntity entity)
    {
        var mapTiles = entity.GetComponent<EntityCollector<Tile>>().Collection;
        for (var i = 0; i < 50; i++)
        {
            for (var j = 0; j < 50; j++)
            {
                var tile = new Tile();
                tile.GetComponent<Positional>().Position = new Vector2(i * 16 , j * 16);
                tile.GetComponent<Drawable>().Texture = Graphics.Content.Load<Texture2D>("map/floor");
                tile.GetComponent<Drawable>().LayerDepth = 0;
                mapTiles.AddLast(tile);
            }
        }
    }
    
    
    
}