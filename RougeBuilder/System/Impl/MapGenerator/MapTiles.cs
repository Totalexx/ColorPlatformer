using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RougeBuilder.Global;
using RougeBuilder.Utils;

namespace RougeBuilder.System.Impl;

public static class MapTiles
{
    public static readonly Vector2 TileSize = new (16, 16);

    public static readonly Texture2D Floor = Graphics.Content.Load<Texture2D>("map/floor");
    public static readonly Texture2D FloorWithCrack = Graphics.Content.Load<Texture2D>("map/floor2");
    public static readonly Texture2D WallBottomLeft = Graphics.Content.Load<Texture2D>("map/wall-bottom-left");
    public static readonly Texture2D WallBottomRight = Graphics.Content.Load<Texture2D>("map/wall-bottom-right");
    public static readonly Texture2D WallLeft = Graphics.Content.Load<Texture2D>("map/wall-left");
    public static readonly Texture2D WallLeftBottom = Graphics.Content.Load<Texture2D>("map/wall-left-bottom");
    public static readonly Texture2D WallLeftTop = Graphics.Content.Load<Texture2D>("map/wall-left-top");
    public static readonly Texture2D WallRight = Graphics.Content.Load<Texture2D>("map/wall-right");
    public static readonly Texture2D WallRightBottom = Graphics.Content.Load<Texture2D>("map/wall-right-bottom");
    public static readonly Texture2D WallRightTop = Graphics.Content.Load<Texture2D>("map/wall-right-top");
    public static readonly Texture2D WallTop = Graphics.Content.Load<Texture2D>("map/wall-top");
    public static readonly Texture2D WallTopLeft = Graphics.Content.Load<Texture2D>("map/wall-top-left");
    public static readonly Texture2D WallTopRight = Graphics.Content.Load<Texture2D>("map/wall-top-right");
    public static readonly Texture2D Error = Graphics.Content.Load<Texture2D>("map/error");
    
    public class TilePatterns
    {
        public readonly Dictionary<bool[,], Texture2D> WallTexturePatterns = new(new BoolArray3x3EqualityComparer());

        public TilePatterns()
        {
            AddPattern(WallTopPatterns);
            AddPattern(WallLeftPatterns);
            AddPattern(WallRightPatterns);
            AddPattern(WallCornersPatterns);
            AddPattern(WallBottomLeftPatterns);
            AddPattern(WallBottomRightPatterns);
            AddPattern(WallTopLeftPatterns);
            AddPattern(WallTopRightPatterns);
        }

        private void AddPattern(Dictionary<bool[,], Texture2D> pattern)
        {
            foreach (var tile in pattern)
            {
                if (WallTexturePatterns.ContainsKey(tile.Key))
                    throw new InvalidOperationException("Тайл с таким паттерном уже существует");
                WallTexturePatterns[tile.Key] = tile.Value;
            }
        }
        
        private readonly Dictionary<bool[,], Texture2D> WallLeftPatterns = new()
        {
            [new [,]
            {
                {false, false, false},
                {false, false, true},
                {false, false, true}
            }] = WallLeft,
            [new [,]
            {
                {false, false, true},
                {false, false, true},
                {false, false, true}
            }] = WallLeft,
            [new [,]
            {
                {false, false, true},
                {false, false, true},
                {false, false, false}
            }] = WallLeft,
            [new [,]
            {
                {false, false, false},
                {false, false, true},
                {false, false, false}
            }] = WallLeft,
        };
        
        private readonly Dictionary<bool[,], Texture2D> WallRightPatterns = new()
        {
            [new [,]
            {
                {false, false, false},
                {true, false, false},
                {true, false, false}
            }] = WallRight,
            [new [,]
            {
                {true, false, false},
                {true, false, false},
                {true, false, false}
            }] = WallRight,
            [new [,]
            {
                {true, false, false},
                {true, false, false},
                {false, false, false}
            }] = WallRight,
            [new [,]
            {
                {false, false, false},
                {true, false, false},
                {false, false, false}
            }] = WallRight,
        };
        
        private readonly Dictionary<bool[,], Texture2D> WallTopPatterns = new()
        {
            [new [,]
            {
                {false, true, true},
                {false, false, false},
                {false, false, false}
            }] = WallTop,
            [new [,]
            {
                {true, true, true},
                {false, false, false},
                {false, false, false}
            }] = WallTop,
            [new [,]
            {
                {true, true, false},
                {false, false, false},
                {false, false, false}
            }] = WallTop,
            [new [,]
            {
                {false, true, false},
                {false, false, false},
                {false, false, false}
            }] = WallTop,
            [new [,]
            {
                {false, false, false},
                {false, false, false},
                {false, true, true}
            }] = WallTop,
            [new [,]
            {
                {false, false, false},
                {false, false, false},
                {true, true, true}
            }] = WallTop,
            [new [,]
            {
                {false, false, false},
                {false, false, false},
                {true, true, false}
            }] = WallTop,
            [new [,]
            {
                {false, false, false},
                {false, false, false},
                {false, true, false}
            }] = WallTop,
        };
        
        private readonly Dictionary<bool[,], Texture2D> WallCornersPatterns = new()
        {
            [new [,]
            {
                {false, false, true},
                {false, false, false},
                {false, false, false}
            }] = WallLeftBottom,
            [new [,]
            {
                {true, false, false},
                {false, false, false},
                {false, false, false}
            }] = WallRightBottom,
            [new [,]
            {
                {false, false, false},
                {false, false, false},
                {false, false, true}
            }] = WallLeftTop,
            [new [,]
            {
                {false, false, false},
                {false, false, false},
                {true, false, false}
            }] = WallRightTop,
        };
        
        private readonly Dictionary<bool[,], Texture2D> WallBottomLeftPatterns = new()
        {
            [new [,]
            {
                {false, true, true},
                {false, false, true},
                {false, false, false}
            }] = WallBottomLeft,
            [new [,]
            {
                {true, true, true},
                {false, false, true},
                {false, false, false}
            }] = WallBottomLeft,
            [new [,]
            {
                {false, true, true},
                {false, false, true},
                {false, false, true}
            }] = WallBottomLeft,
            [new [,]
            {
                {true, true, true},
                {false, false, true},
                {false, false, true}
            }] = WallBottomLeft,
        };
        
        private readonly Dictionary<bool[,], Texture2D> WallBottomRightPatterns = new()
        {
            [new [,]
            {
                {true, true, false},
                {true, false, false},
                {false, false, false}
            }] = WallBottomRight,
            [new [,]
            {
                {true, true, true},
                {true, false, false},
                {false, false, false}
            }] = WallBottomRight,
            [new [,]
            {
                {true, true, false},
                {true, false, false},
                {true, false, false}
            }] = WallBottomRight,
            [new [,]
            {
                {true, true, true},
                {true, false, false},
                {true, false, false}
            }] = WallBottomRight,
        };
        
        private readonly Dictionary<bool[,], Texture2D> WallTopLeftPatterns = new()
        {
            [new [,]
            {
                {false, false, false},
                {false, false, true},
                {false, true, true}
            }] = WallTopLeft,
            [new [,]
            {
                {false, false, true},
                {false, false, true},
                {false, true, true}
            }] = WallTopLeft,
            [new [,]
            {
                {false, false, false},
                {false, false, true},
                {true, true, true}
            }] = WallTopLeft,
            [new [,]
            {
                {false, false, true},
                {false, false, true},
                {true, true, true}
            }] = WallTopLeft,
        };
        
        private readonly Dictionary<bool[,], Texture2D> WallTopRightPatterns = new()
        {
            [new [,]
            {
                {false, false, false},
                {true, false, false},
                {true, true, false}
            }] = WallTopRight,
            [new [,]
            {
                {true, false, false},
                {true, false, false},
                {true, true, false}
            }] = WallTopRight,
            [new [,]
            {
                {false, false, false},
                {true, false, false},
                {true, true, true}
            }] = WallTopRight,
            [new [,]
            {
                {true, false, false},
                {true, false, false},
                {true, true, true}
            }] = WallTopRight,
        };
        
        private readonly Dictionary<bool[,], Texture2D> EmptyPattern = new()
        {
            [new [,]
            {
                {false, false, false},
                {false, false, false},
                {false, false, false}
            }] = Error,
            [new [,]
            {
                {false, false, false},
                {false, false, false},
                {false, false, false}
            }] = Error,
            [new [,]
            {
                {false, false, false},
                {false, false, false},
                {false, false, false}
            }] = Error,
        };
    }
}