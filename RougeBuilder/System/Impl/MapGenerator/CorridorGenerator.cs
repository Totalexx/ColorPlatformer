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

        var isFirst = true;
        Rectangle prevRoom = default;
        foreach (var room in rooms)
        {
            if (isFirst)
            {
                prevRoom = room;
                isFirst = false;
                continue;
            }
            corridors.AddLast(GenerateOneCorridor(prevRoom, room));
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
                {
                    tiles.AddLast(CreateCorridorTile(step.X, step.Y - HALF_CORRIDOR_WIDTH + i));
                }
            }
            else
            {
                for (var i = 0; i < CORRIDOR_WIDTH; i++)
                {
                    tiles.AddLast(CreateCorridorTile(step.X - HALF_CORRIDOR_WIDTH + i, step.Y));
                }
            }
            
            if (isSecond)
            {
                if (prevStep.Y == step.Y)
                {
                    for (var i = 0; i < CORRIDOR_WIDTH; i++)
                    {
                        tiles.AddLast(CreateCorridorTile(prevStep.X, prevStep.Y - HALF_CORRIDOR_WIDTH + i));
                    }
                }
                else
                {
                    for (var i = 0; i < CORRIDOR_WIDTH; i++)
                    {
                        tiles.AddLast(CreateCorridorTile(prevStep.X - HALF_CORRIDOR_WIDTH + i, prevStep.Y));
                    }
                }
                isSecond = false;
            }
            
            prevStep = step;
            
        }

        return tiles;
    }

    private Tile CreateCorridorTile(float x, float y)
    {
        var position = new Vector2(x * MapGenerationSystem.TILE_WIDTH, y * MapGenerationSystem.TILE_HEIGHT);
        var texture = Graphics.Content.Load<Texture2D>("map/floor");
        return new Tile(position, texture);
    }

    private LinkedList<Vector2> GenerateOneCorridor(Rectangle firstRoom, Rectangle secondRoom)
    {
        var firstRoomCenter = firstRoom.Center.ToVector2();
        var secondRoomCenter = secondRoom.Center.ToVector2();
        var corridorPath = new LinkedList<Vector2>();

        var roomsPath = Searcher
            .FindWidthPathTo(firstRoomCenter, secondRoomCenter)
            .Where(step => !(firstRoom.Contains(step) || secondRoom.Contains(step)));
        
        foreach (var step in roomsPath)
            corridorPath.AddLast(step);
        
        return corridorPath;
    }
}