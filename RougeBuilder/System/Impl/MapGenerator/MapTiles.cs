using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RougeBuilder.Global;

namespace RougeBuilder.System.Impl;

public class MapTiles
{
    public static readonly Vector2 TileSize = new (16, 16);

    public static readonly Texture2D Floor = Graphics.Content.Load<Texture2D>("map/floor");
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
}