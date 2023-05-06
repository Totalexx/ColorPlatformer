using System.Linq;
using RougeBuilder.Component.Impl;
using RougeBuilder.Model;
using RougeBuilder.Model.Impl.Map;

namespace RougeBuilder.System.Impl;

public class MapGenerationSystem : AbstractSystem<MapMarker>
{
    private readonly MapAreaGenerator areaGenerator = new();
    private readonly RoomGenerator roomGenerator = new();
    private readonly CorridorGenerator corridorGenerator = new();

    protected override void UpdateEntity(AbstractEntity entity)
    {
        GenerateMap();
        var roomTiles = roomGenerator.GenerateTiles();
        var corridorTiles = corridorGenerator.GenerateTiles();

        foreach (var tile in roomTiles)
            entity.GetComponent<EntityCollector<Tile>>().Collection.AddLast(tile);            

        foreach (var tile in corridorTiles)
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