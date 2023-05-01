using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using RougeBuilder.Component.Impl;
using RougeBuilder.Global;
using RougeBuilder.Model;
using RougeBuilder.Model.Impl.Map;
using RougeBuilder.Utils;

namespace RougeBuilder.System.Impl;

public class MapGenerationSystem : AbstractSystem<MapMarker>
{
    private const int TILE_WIDTH = 16;
    private const int TILE_HEIGHT = 16;
    
    private const int MAP_TILES_WIDTH = 64;
    private const int MAP_TILES_HEIGHT = 48;

    private const int MIN_AREA = 256;
    private const float ASPECT_RATIO = 0.35f;

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

    public BinaryTree<Rectangle> BSP()
    {
        return GenerateBSPTree();
    }
    
    private BinaryTree<Rectangle> GenerateBSPTree()
    {
        var mapBounds = new Rectangle(0, 0, MAP_TILES_WIDTH, MAP_TILES_HEIGHT);
        var BSPTree = new BinaryTree<Rectangle>(SplitRectangle(mapBounds));

        return BSPTree;
    }

    private Node<Rectangle> SplitRectangle(Rectangle area)
    {
        if (area.Width * area.Height <= MIN_AREA)
            return new Node<Rectangle>(area);
        
        var firstChildWidth = area.Width;
        var firstChildHeight = area.Height;
        var secondChildWidth = area.Width;
        var secondChildHeight = area.Height;
        var offsetX = 0;
        var offsetY = 0;
        
        var biggerSide = area.Width >= area.Height ? area.Width : area.Height;
        
        var minSideLength = (int) Math.Round(ASPECT_RATIO * biggerSide);
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