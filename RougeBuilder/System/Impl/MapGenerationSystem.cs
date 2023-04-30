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
    private const int TILE_WIDTH = 16;
    private const int TILE_HEIGHT = 16;

    protected override void UpdateEntity(AbstractEntity entity)
    {
        var mapTiles = entity.GetComponent<EntityCollector<Tile>>().Collection;
        for (var i = 0; i < 50; i++)
        {
            for (var j = 0; j < 50; j++)
            {
                var tile = new Tile();
                tile.GetComponent<Positional>().Position = new Vector2(i * TILE_WIDTH , j * TILE_HEIGHT);
                tile.GetComponent<Drawable>().Texture = Graphics.Content.Load<Texture2D>("map/floor");
                tile.GetComponent<Drawable>().LayerDepth = 0;
                mapTiles.AddLast(tile);
            }
        }
    }
    
    
    
}