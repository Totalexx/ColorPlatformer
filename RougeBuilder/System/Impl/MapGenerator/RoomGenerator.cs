using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RougeBuilder.Global;
using RougeBuilder.Model.Impl.Map;
using RougeBuilder.Utils;

namespace RougeBuilder.System.Impl;

public class RoomGenerator
{
    private const int MIN_ROOM_S = 16;
    private const float ASPECT_RATIO_ROOM = 1.1f;

    private readonly Random random = new ();
    
    public Dictionary<Node<Rectangle>, Rectangle> Rooms { get; private set; }

    public void GenerateRooms(IEnumerable<Node<Rectangle>> roomAreas)
    {
        var nodeAndRoom = new Dictionary<Node<Rectangle>, Rectangle>();

        foreach (var area in roomAreas)
            nodeAndRoom[area] = GenerateOneRoom(area.Value);

        Rooms = nodeAndRoom;
    }

    public LinkedList<Tile> GenerateTiles()
    {
        var roomsTiles = new LinkedList<Tile>();
        foreach (var roomAndArea in Rooms)
            foreach (var tile in GenerateOneTileRoom(roomAndArea.Value))
                roomsTiles.AddLast(tile);

        return roomsTiles;
    }

    private IEnumerable<Tile> GenerateOneTileRoom(Rectangle room)
    {
        var tiles = new LinkedList<Tile>();
        for (var x = room.X; x < room.X + room.Width; x++)
        {
            for (var y = room.Y; y < room.Y + room.Height; y++)
            {
                var position = new Vector2(x * MapGenerationSystem.TILE_WIDTH, y * MapGenerationSystem.TILE_HEIGHT);
                var texture = Graphics.Content.Load<Texture2D>("map/floor");
                tiles.AddLast(new Tile(position, texture));
            }
        }

        return tiles;
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
}