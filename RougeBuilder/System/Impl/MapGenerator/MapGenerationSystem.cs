using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using RougeBuilder.Component.Impl;
using RougeBuilder.Model;
using RougeBuilder.Model.Impl.Map;

namespace RougeBuilder.System.Impl;

public class MapGenerationSystem : AbstractSystem<MapMarker>
{
    private readonly Dictionary<Vector2, Tile> mapTiles = new(); 
    
    private readonly MapAreaGenerator areaGenerator = new();
    private readonly RoomGenerator roomGenerator = new();
    private readonly CorridorGenerator corridorGenerator = new();

    protected override void UpdateEntity(AbstractEntity entity)
    {
        GenerateMap();
        SetMapTiles(roomGenerator.GenerateTiles());
        SetMapTiles(corridorGenerator.GenerateFloorTiles());
        AddTilesToMap(entity);
    }

    private void SetMapTiles(Dictionary<Vector2, Tile> tiles)
    {
        foreach (var tile in tiles)
            mapTiles[tile.Key] = tile.Value;
    }

    private void AddTilesToMap(AbstractEntity entity)
    {
        foreach (var tile in mapTiles.Select(t => t.Value))
            entity.GetComponent<EntityCollector<Tile>>().Collection.AddLast(tile);
    }

    private void GenerateMap()
    {
        areaGenerator.GenerateBSPTree();
        roomGenerator.GenerateRooms(areaGenerator.AreaTree.GetLeafsValues());
        
        var rooms = roomGenerator.Rooms.Select(room => room.Value);
        corridorGenerator.GenerateSimpleCorridors(rooms);
    }
}