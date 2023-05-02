using System;
using System.Collections.Generic;
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
    public BinaryTree<Rectangle> BSP;

    private const int TILE_WIDTH = 16;
    private const int TILE_HEIGHT = 16;
    
    private const int MAP_TILES_WIDTH = 32;
    private const int MAP_TILES_HEIGHT = 32;

    private const int MIN_AREA_S = 128;
    private const float ASPECT_RATIO_AREA = 0.40f;

    private const int MIN_ROOM_S = 16;
    private const float ASPECT_RATIO_ROOM = 1.1f;

    private readonly Random random = new ();
    
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

    public List<Rectangle> GenerateRoomsTest()
    {
        return GenerateRooms();
    }
    
    private List<Rectangle> GenerateRooms()
    {
        var roomAreas = GenerateBSPTree();

        BSP = roomAreas;
        
        var rooms = new List<Rectangle>(roomAreas.CountLeaf());
        foreach (var area in roomAreas.GetLeafs())
            rooms.Add(GenerateOneRoom(area));

        return rooms;
    }

    private Rectangle GenerateOneRoom(Rectangle roomArea)
    {
        var minHeight = 0;
        var minWidth = 0;

        if (roomArea.Width > roomArea.Height)
        {
            minHeight = (int) Math.Floor(Math.Sqrt(MIN_ROOM_S * ASPECT_RATIO_ROOM));
            minWidth = (int) Math.Floor(ASPECT_RATIO_ROOM * minHeight);
        }
        else
        {
            minWidth = (int) Math.Floor(Math.Sqrt(MIN_ROOM_S * 1/ASPECT_RATIO_ROOM));
            minHeight = (int) Math.Floor(1/ASPECT_RATIO_ROOM * minWidth);
        }

        var maxWidth = roomArea.Width - 1;
        var maxHeight = roomArea.Height - 1; 

        var width = random.Next(minWidth, maxWidth);
        var height = random.Next(minHeight, maxHeight);

        var maxX = roomArea.X + maxWidth - width;
        var maxY = roomArea.Y + maxHeight - height;
        
        var x = random.Next(roomArea.X == maxX ? roomArea.X : roomArea.X + 1, maxX);
        var y = random.Next(roomArea.Y == maxY ? roomArea.Y : roomArea.Y + 1, maxY);

        return new Rectangle(x, y, width, height);
    }

    private List<Rectangle> GenerateCorridors(BinaryTree<Rectangle> bsp)
    {
        var corridors = new List<Rectangle>();
        
        
        
        return corridors;
    }

    private BinaryTree<Rectangle> GenerateBSPTree()
    {
        var mapBounds = new Rectangle(0, 0, MAP_TILES_WIDTH, MAP_TILES_HEIGHT);
        var BSPTree = new BinaryTree<Rectangle>(SplitRectangle(mapBounds));

        return BSPTree;
    }

    private Node<Rectangle> SplitRectangle(Rectangle area)
    {
        if (area.Width * area.Height <= MIN_AREA_S)
            return new Node<Rectangle>(area);
        
        var firstChildWidth = area.Width;
        var firstChildHeight = area.Height;
        var secondChildWidth = area.Width;
        var secondChildHeight = area.Height;
        var offsetX = 0;
        var offsetY = 0;
        
        var biggerSide = area.Width >= area.Height ? area.Width : area.Height;
        
        var minSideLength = (int) Math.Round(ASPECT_RATIO_AREA * biggerSide);
        var maxSideLength = biggerSide - minSideLength;

        var firstChildSide = random.Next(minSideLength, maxSideLength);
        
        if (area.Width >= area.Height)
        {
            firstChildWidth = firstChildSide;
            offsetX = firstChildSide;
            secondChildWidth = area.Width - firstChildSide;
        }
        else
        {
            firstChildHeight = firstChildSide;
            offsetY = firstChildSide;
            secondChildHeight = area.Height - firstChildSide;
        }

        var firstChild = new Rectangle(area.X, area.Y, firstChildWidth, firstChildHeight);
        var secondChild = new Rectangle(area.X + offsetX, area.Y + offsetY, secondChildWidth, secondChildHeight);

        var partOfArea = new Node<Rectangle>(area);
        partOfArea.Add(SplitRectangle(firstChild));
        partOfArea.Add(SplitRectangle(secondChild));

        return partOfArea;
    } 
    
}