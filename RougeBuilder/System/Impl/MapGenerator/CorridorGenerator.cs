using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RougeBuilder.Global;
using RougeBuilder.Model.Impl.Map;
using RougeBuilder.Utils;

namespace RougeBuilder.System.Impl;

public class CorridorGenerator
{
    public LinkedList<LinkedList<Vector2>> Corridors { get; private set; }
    
    private const int CORRIDOR_WIDTH = 3;
    private const int HALF_CORRIDOR_WIDTH = CORRIDOR_WIDTH / 2;

    public void GenerateSimpleCorridors(IEnumerable<Rectangle> rooms)
    {
        var corridors = new LinkedList<LinkedList<Vector2>>();
        var roomAndBounds = CreateBoundsFromRooms(rooms);

        var isFirst = true;
        Rectangle prevRoom = default;
        foreach (var room in roomAndBounds.Select(r=> r.Key))
        {
            if (isFirst)
            {
                prevRoom = room;
                isFirst = false;
                continue;
            }
            corridors.AddLast(GenerateOneCorridor(roomAndBounds, prevRoom, room));
            prevRoom = room;
        }

        Corridors = corridors;
    }

    public LinkedList<Tile> GenerateTiles()
    {
        var tiles = new LinkedList<Tile>();
        foreach (var corridor in Corridors)
            foreach (var tile in GenerateOneTileCorridor(corridor))
                tiles.AddLast(tile);

        return tiles;
    }

    private LinkedList<Tile> GenerateOneTileCorridor(LinkedList<Vector2> corridor)
    {
        var tiles = new LinkedList<Tile>();

        var isFirst = true;
        var isSecond = false;
        Vector2 prevStep = default;
        
        foreach (var step in corridor)
        {
            if (isFirst)
            {
                prevStep = step;
                isFirst = false;
                isSecond = true;
                continue;
            }

            if (prevStep.Y == step.Y)
            {
                for (var i = 0; i < CORRIDOR_WIDTH; i++)
                    tiles.AddLast(CreateCorridorTile(step.X, step.Y - HALF_CORRIDOR_WIDTH + i));
                
                // var wallTop = new Vector2(step.X, step.Y  - HALF_CORRIDOR_WIDTH - 1) * MapTiles.TileSize;
                // var wallBottom = new Vector2(step.X, step.Y + HALF_CORRIDOR_WIDTH + 1) * MapTiles.TileSize;
                //
                // tiles.AddLast(new Tile(wallTop, MapTiles.WallTop));
                // tiles.AddLast(new Tile(wallBottom, MapTiles.WallTop));
            }
            else
            {
                
                for (var i = 0; i < CORRIDOR_WIDTH; i++)
                    tiles.AddLast(CreateCorridorTile(step.X - HALF_CORRIDOR_WIDTH + i, step.Y));

                // var wallLeft = new Vector2(step.X - HALF_CORRIDOR_WIDTH - 1, step.Y) * MapTiles.TileSize;
                // var wallRight = new Vector2(step.X + HALF_CORRIDOR_WIDTH + 1, step.Y) * MapTiles.TileSize;
                //
                // tiles.AddLast(new Tile(wallLeft, MapTiles.WallLeft));
                // tiles.AddLast(new Tile(wallRight, MapTiles.WallRight));
            }
            
            if (isSecond)
            {
                if (prevStep.Y == step.Y)
                {
                    for (var i = 0; i < CORRIDOR_WIDTH; i++)
                        tiles.AddLast(CreateCorridorTile(prevStep.X, prevStep.Y - HALF_CORRIDOR_WIDTH + i));
                }
                else
                    for (var i = 0; i < CORRIDOR_WIDTH; i++)
                        tiles.AddLast(CreateCorridorTile(prevStep.X - HALF_CORRIDOR_WIDTH + i, prevStep.Y));
                isSecond = false;
            }
            
            prevStep = step;
            
        }

        return tiles;
    }

    private Tile CreateCorridorTile(float x, float y)
    {
        var position = new Vector2(x, y) * MapTiles.TileSize;
        var texture = MapTiles.Floor;
        return new Tile(position, texture);
    }

    private LinkedList<Vector2> GenerateOneCorridor(Dictionary<Rectangle, HashSet<Vector2>> allBounds, Rectangle firstRoom, Rectangle secondRoom)
    {
        var firstRoomCenter = firstRoom.Center.ToVector2();
        var secondRoomCenter = secondRoom.Center.ToVector2();
        var corridorPath = new LinkedList<Vector2>();

        var bounds = allBounds.Where(r => r.Key != firstRoom && r.Key != secondRoom).SelectMany(r => r.Value);
        var hashSetBounds = new HashSet<Vector2>(bounds);

        var roomsPath = Searcher
            .FindWidthPathTo(firstRoomCenter, secondRoomCenter, walls: hashSetBounds)
            .Where(step => !(firstRoom.Contains(step) || secondRoom.Contains(step)));
        
        foreach (var step in roomsPath)
            corridorPath.AddLast(step);
        
        return corridorPath;
    }

    private Dictionary<Rectangle, HashSet<Vector2>> CreateBoundsFromRooms(IEnumerable<Rectangle> rooms)
    {
        var roomAndBounds = new Dictionary<Rectangle, HashSet<Vector2>>();

        foreach (var room in rooms)
        {
            var bounds = new HashSet<Vector2>();

            var boundsTopLeft = new Vector2(room.X - CORRIDOR_WIDTH - 1, room.Y - CORRIDOR_WIDTH - 1);
            var boundsBottomRight = new Vector2(room.X + room.Width + CORRIDOR_WIDTH, room.Y + room.Height + CORRIDOR_WIDTH);

            for (var x = boundsTopLeft.X; x <= boundsBottomRight.X; x++)
            {
                bounds.Add(new Vector2(x, boundsTopLeft.Y));
                bounds.Add(new Vector2(x, boundsBottomRight.Y));
            }
            
            for (var y = boundsTopLeft.Y; y < boundsBottomRight.Y; y++)
            {
                bounds.Add(new Vector2(boundsTopLeft.X, y));
                bounds.Add(new Vector2(boundsBottomRight.X, y));
            }

            roomAndBounds[room] = bounds;
        }

        return roomAndBounds;
    }
}