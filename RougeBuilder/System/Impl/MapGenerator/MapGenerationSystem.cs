using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using RougeBuilder.Component.Impl;
using RougeBuilder.Model;
using RougeBuilder.Model.Impl.Map;
using RougeBuilder.Utils;

namespace RougeBuilder.System.Impl;

public class MapGenerationSystem : AbstractSystem<MapMarker>
{
    public const int TILE_WIDTH = 16;
    public const int TILE_HEIGHT = 16;

    private readonly MapAreaGenerator areaGenerator = new();
    private readonly RoomGenerator roomGenerator = new();
    private readonly CorridorGenerator corridorGenerator = new();

    public Dictionary<Node<Rectangle>, Rectangle> Room;
    public LinkedList<LinkedList<Vector2>> Corr;
    public BinaryTree<Rectangle> BSP;
    
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