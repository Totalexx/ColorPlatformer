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
    private const int MIN_ROOM_S = 128;
    private const float ASPECT_RATIO_ROOM = 1.1f;

    private readonly Random random = new (42);
    
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
                var position = new Vector2(x, y) * MapTiles.TileSize;
                tiles.AddLast(new Tile(position, MapTiles.Floor));
            }
        }

        for (var x = room.X; x < room.X + room.Width; x++)
        {
            var positionTop = new Vector2(x, room.Y - 1) * MapTiles.TileSize;
            var positionBottom = new Vector2(x , room.Y + room.Height) * MapTiles.TileSize;
            tiles.AddLast(new Tile(positionTop, MapTiles.WallTop));
            tiles.AddLast(new Tile(positionBottom, MapTiles.WallTop));
        }
        for (var y = room.Y; y < room.Y + room.Height; y++)
        {
            var positionLeft = new Vector2(room.X-1, y) * MapTiles.TileSize;
            var positionRight = new Vector2(room.X + room.Width, y) * MapTiles.TileSize;
            tiles.AddLast(new Tile(positionLeft, MapTiles.WallLeft));
            tiles.AddLast(new Tile(positionRight, MapTiles.WallRight));
        }

        var positionTopLeft = new Vector2(room.X-1, room.Y-1) * MapTiles.TileSize;
        var positionTopRight = new Vector2(room.X + room.Width, room.Y - 1) * MapTiles.TileSize;
        var positionBottomLeft = new Vector2(room.X-1, room.Y + room.Height) * MapTiles.TileSize;
        var positionBottomRight = new Vector2(room.X + room.Width, room.Y + room.Height) * MapTiles.TileSize;
        tiles.AddLast(new Tile(positionTopLeft, MapTiles.WallLeftTop));
        tiles.AddLast(new Tile(positionTopRight, MapTiles.WallRightTop));
        tiles.AddLast(new Tile(positionBottomLeft, MapTiles.WallLeftBottom));
        tiles.AddLast(new Tile(positionBottomRight, MapTiles.WallRightBottom));

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

        var width = random.Next(minWidth > maxWidth ? maxWidth : minWidth, maxWidth);
        var height = random.Next(minHeight > maxHeight ? maxHeight : minHeight, maxHeight);

        var maxX = roomArea.X + maxWidth - width;
        var maxY = roomArea.Y + maxHeight - height;
        
        var x = random.Next(roomArea.X == maxX ? roomArea.X : roomArea.X + 1, maxX);
        var y = random.Next(roomArea.Y == maxY ? roomArea.Y : roomArea.Y + 1, maxY);

        return new Rectangle(x, y, width, height);
    }
}