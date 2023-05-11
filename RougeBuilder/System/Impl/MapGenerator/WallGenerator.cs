using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RougeBuilder.Model.Impl.Map;

namespace RougeBuilder.System.Impl;

public class WallGenerator
{
    private readonly HashSet<Vector2> circularPoints = new()
    {
        new Vector2(-1, -1),
        new Vector2(-1, 0),
        new Vector2(-1, 1),
        
        new Vector2(0, -1),
        new Vector2(0, 1),
        
        new Vector2(1, -1),
        new Vector2(1, 0),
        new Vector2(1, 1),
    };

    private readonly Dictionary<Vector2, Tile> floorTiles;
    private readonly HashSet<Vector2> boundaryTilesCandidates;
    private readonly Dictionary<bool[,], Texture2D> wallPatterns = new MapTiles.TilePatterns().WallTexturePatterns; 

    public WallGenerator(Dictionary<Vector2, Tile> floorTiles, HashSet<Vector2> boundaryTilesCandidates)
    {
        this.floorTiles = floorTiles;
        this.boundaryTilesCandidates = boundaryTilesCandidates;
    }

    public Dictionary<Vector2, Tile> GenerateWalls()
    {
        var wallTiles = new Dictionary<Vector2, Tile>();
        var wallPositions = GenerateWallPositions();
        
        foreach (var wallPosition in wallPositions)
        {
            var floorAround = CheckFloorAround(wallPosition);
            var wallTexture = !wallPatterns.ContainsKey(floorAround) ? MapTiles.Error : wallPatterns[floorAround];
            wallTiles[wallPosition] = new Tile(wallPosition, wallTexture);
        }

        return wallTiles;
    }

    private HashSet<Vector2> GenerateWallPositions()
    {
        var wallPositions = new HashSet<Vector2>();
        
        foreach (var boundaryTile in boundaryTilesCandidates)
        {
            foreach (var dP in circularPoints)
            {
                var nextWallPosition = boundaryTile + dP;
                if (!floorTiles.ContainsKey(nextWallPosition))
                    wallPositions.Add(nextWallPosition);                
            }
        }

        return wallPositions;
    }

    private bool[,] CheckFloorAround(Vector2 wallPosition)
    {
        var hasFloorAround = new bool[3,3];
        
        foreach (var dP in circularPoints)
            hasFloorAround[(int)dP.Y + 1, (int)dP.X + 1] = floorTiles.ContainsKey(wallPosition + dP);

        return hasFloorAround;
    }
}