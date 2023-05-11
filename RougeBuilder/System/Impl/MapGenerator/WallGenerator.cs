using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;

namespace RougeBuilder.System.Impl;

public class WallGenerator
{

    private readonly Dictionary<bool[], Texture2D> WallTile = new ()
    {
        
    };

    public Dictionary<Vector2, Tile> GenerateWalls(Dictionary<Vector2, Tile> floorTiles, HashSet<Vector2> boundaryTiles)
    {
        var wallTiles = new Dictionary<Vector2, Tile>();
        var wallPositions = GenerateWallPositions(boundaryTiles);
        
        foreach (var wallPosition in wallPositions)
        {
            
        }

        return wallTiles;
    }

    private HashSet<Vector2> GenerateWallPositions(HashSet<Vector2> boundaryTiles)
    {
        
    }
    
    private Tile Create
    
}