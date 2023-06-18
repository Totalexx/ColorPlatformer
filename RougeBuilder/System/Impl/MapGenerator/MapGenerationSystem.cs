using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using RougeBuilder.Component.Impl;
using RougeBuilder.Entity.Impl;
using RougeBuilder.Model;
using RougeBuilder.Model.Impl;
using RougeBuilder.Model.Impl.Map;
using RougeBuilder.State;
using NotImplementedException = System.NotImplementedException;

namespace RougeBuilder.System.Impl;

public class MapGenerationSystem : AbstractSystem<MapMarker>
{
    private readonly Dictionary<Vector2, Tile> mapTiles = new(); 
    
    private readonly MapAreaGenerator areaGenerator = new();
    private readonly RoomGenerator roomGenerator = new();
    private readonly CorridorGenerator corridorGenerator = new();
    private WallGenerator wallGenerator;


    private GameLoadingState _gameLoadingState;
    
    public MapGenerationSystem(GameLoadingState gameLoadingState)
    {
        _gameLoadingState = gameLoadingState;
    }

    protected override void UpdateEntity(AbstractEntity entity)
    {
        GenerateMap();
        SetMapTiles(roomGenerator.GenerateTiles());
        SetMapTiles(corridorGenerator.GenerateFloorTiles());
        var walls = GenerateWalls();
        SetMapTiles(walls);
        AddTilesToMap(entity);
        var player = GeneratePlayer();
        GenerateEnemies(player);
        GenerateWin();
    }

    private void GenerateEnemies(Player player)
    {
        var random = new Random();
        foreach (var room in roomGenerator.Rooms.Skip(1))
        {
            var countEnemies = random.Next(5, 10);
            for (var i = 0; i < countEnemies; i++)
            {
                var enemy = new Enemy(player);
                enemy.GetComponent<Positional>().Position = new Vector2(random.Next(room.Value.Left, room.Value.Right),
                    random.Next(room.Value.Top, room.Value.Bottom)) * MapTiles.TileSize;
                _gameLoadingState.toAdd.Add(enemy);
            }
        }
    }
    
    private Player GeneratePlayer()
    {
        var player = new Player();
        player.GetComponent<Positional>().Position = roomGenerator.Rooms.First().Value.Center.ToVector2() * 16;
        _gameLoadingState.toAdd.Add(player);
        return player;
    }
    
    private void GenerateWin()
    {
        var key = new ChestKey();
        var random = new Random();
        key.GetComponent<Positional>().Position =
            roomGenerator.Rooms.Skip(random.Next(3, 5)).First().Value.Center.ToVector2() * 16;
        
        var win = new WinChest();
        win.GetComponent<Positional>().Position = roomGenerator.Rooms.Last().Value.Center.ToVector2() * 16;
        _gameLoadingState.toAdd.Add(win);
        _gameLoadingState.toAdd.Add(key);
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

    private Dictionary<Vector2, Tile> GenerateWalls()
    {
        var boundaryTiles = new HashSet<Vector2>();
        
        var corridorBoundaryTiles = corridorGenerator.BoundaryTiles;
        var roomBoundaryTiles = roomGenerator.BoundaryTiles;

        boundaryTiles.UnionWith(corridorBoundaryTiles);
        boundaryTiles.UnionWith(roomBoundaryTiles);
        
        wallGenerator = new WallGenerator(mapTiles, boundaryTiles);
        return wallGenerator.GenerateWalls();
    }
}