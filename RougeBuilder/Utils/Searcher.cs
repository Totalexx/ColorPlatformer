using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace RougeBuilder.Utils;

public static class Searcher
{

    public static LinkedList<Vector2> FindWidthPathTo(
        Vector2 from, 
        Vector2 to, 
        HashSet<Vector2> walls = null, 
        float step = 1f, 
        bool canDiagonalMove = false)
    {
        var visitedPoints = new HashSet<Vector2>();
        var trackToFrom = new Dictionary<Vector2, Vector2?>
        {
            [from] = null
        };
        
        var stepQueue = new Queue<Vector2>();
        stepQueue.Enqueue(from);
        visitedPoints.Add(from);
        
        while (stepQueue.Count <= 10000)
        {
            if (trackToFrom.ContainsKey(to))
                return GetPathFromTrack(trackToFrom, to);
            
            var point = stepQueue.Dequeue();

            for (var dx = -step; dx <= step; dx += step)
            {
                for (var dy = -step; dy <= step; dy += step)
                {
                    if (dx == 0 && dy == 0)
                        continue;
                    
                    var nextPoint = new Vector2(point.X + dx, point.Y + dy);
                    
                    if(visitedPoints.Contains(nextPoint) 
                       || walls != null && walls.Contains(nextPoint)
                       || !canDiagonalMove && dx != 0 && dy != 0)
                        continue;
                    
                    stepQueue.Enqueue(nextPoint);
                    visitedPoints.Add(nextPoint);
                    trackToFrom[nextPoint] = point;
                }
            }
        }

        return null;
    }

    private static LinkedList<Vector2> GetPathFromTrack(Dictionary<Vector2, Vector2?> track, Vector2 to)
    {
        var path = new LinkedList<Vector2>();
        path.AddLast(to);
        var nextStep = track[to];
        
        while (nextStep.HasValue)
        {
            path.AddLast(nextStep.Value);
            nextStep = track[nextStep.Value];
        }

        return path;
    }
}