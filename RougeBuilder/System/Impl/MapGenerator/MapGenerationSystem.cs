using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RougeBuilder.Component.Impl;
using RougeBuilder.Global;
using RougeBuilder.Model;
using RougeBuilder.Model.Impl.Map;
using RougeBuilder.Utils;

namespace RougeBuilder.System.Impl;

public class MapGenerationSystem : AbstractSystem<MapMarker>
{

    private const int TILE_WIDTH = 16;
    private const int TILE_HEIGHT = 16;

    private readonly MapAreaGenerator areaGenerator = new();
    private readonly RoomGenerator roomGenerator = new();
    private readonly CorridorGenerator corridorGenerator = new();

    public Dictionary<Node<Rectangle>, Rectangle> Room;
    public LinkedList<LinkedList<Vector2>> Corr;
    public BinaryTree<Rectangle> BSP;

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

    public void TestGenerate()
    {
        var bspTree = areaGenerator.GenerateBSPTree();
        var rooms = roomGenerator.GenerateRooms(bspTree.GetLeafs());
        var corridors = corridorGenerator.GenerateCorridors(bspTree, rooms);

        Corr = corridors;
        Room = rooms;
        BSP = bspTree;
    }
}