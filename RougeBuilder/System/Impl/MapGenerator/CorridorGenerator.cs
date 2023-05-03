using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using RougeBuilder.Utils;

namespace RougeBuilder.System.Impl;

public class CorridorGenerator
{
    private const int CORRIDOR_WIDTH = 3;
    
    public LinkedList<LinkedList<Vector2>> GenerateCorridors(
        BinaryTree<Rectangle> bsp, 
        Dictionary<Node<Rectangle>, Rectangle> rooms)
    {
        var corridors = new LinkedList<LinkedList<Vector2>>();

        bool isFirst = true;
        Rectangle prev = default;
        foreach (var parent in rooms)
        {
            if (isFirst)
            {
                prev = parent.Value;
                isFirst = false;
                continue;
            }
            corridors.AddLast(GenerateOneCorridor(prev, parent.Value));
            prev = parent.Value;
        }
        
        return corridors;
    }
    
    private LinkedList<Vector2> GenerateOneCorridor(Rectangle firstRoom, Rectangle secondRoom)
    {
        var firstRoomCenter = firstRoom.Center.ToVector2();
        var secondRoomCenter = secondRoom.Center.ToVector2();
        var corridorPath = Searcher.FindWidthPathTo(firstRoomCenter, secondRoomCenter);

        return corridorPath;
    }
}