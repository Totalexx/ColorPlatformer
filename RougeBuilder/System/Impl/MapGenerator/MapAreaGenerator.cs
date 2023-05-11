using System;
using Microsoft.Xna.Framework;
using RougeBuilder.Utils;

namespace RougeBuilder.System.Impl;

public class MapAreaGenerator
{
    public BinaryTree<Rectangle> AreaTree { get; private set; }

    private const int MAP_TILES_WIDTH = 160;
    private const int MAP_TILES_HEIGHT = 160;

    private const int MIN_AREA_S = 2048;
    private const float ASPECT_RATIO_AREA = 0.40f;

    private readonly Random random = new ();

    public void GenerateBSPTree()
    {
        var mapBounds = new Rectangle(0, 0, MAP_TILES_WIDTH, MAP_TILES_HEIGHT);
        var BSPTree = new BinaryTree<Rectangle>(SplitRectangle(mapBounds));

        AreaTree = BSPTree;
    }

    private Node<Rectangle> SplitRectangle(Rectangle area)
    {
        if (area.Width * area.Height <= MIN_AREA_S)
            return new Node<Rectangle>(area);
        
        var firstChildWidth = area.Width;
        var firstChildHeight = area.Height;
        var secondChildWidth = area.Width;
        var secondChildHeight = area.Height;
        var offsetX = 0;
        var offsetY = 0;
        
        var biggerSide = area.Width >= area.Height ? area.Width : area.Height;
        
        var minSideLength = (int) Math.Round(ASPECT_RATIO_AREA * biggerSide);
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