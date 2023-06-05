using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace RougeBuilder.Utils;

public class QuadTree : IEnumerable<RectangleF>
{
    private readonly QuadTreeNode Root;
    private RectangleF bounds;
    
    public QuadTree(Vector2 bounds)
    {
        this.bounds = new RectangleF(Vector2.Zero, bounds);
        Root = new QuadTreeNode(null, bounds / 2);
    }

    public void Add(RectangleF rectangle)
    {
        if (rectangle.Intersects(bounds))
            Root.Add(rectangle);
    }

    public void Remove(RectangleF rectangle)
    {
        Root.Remove(rectangle);
    }

    public LinkedList<(RectangleF, RectangleF)> GetIntersections()
    {
        return Root.GetIntersections();
    }

    public class QuadTreeNode : IEnumerable<RectangleF>
    {
        private readonly List<RectangleF> values = new();
        private QuadTreeNode[] children = new QuadTreeNode[4];
        private readonly QuadTreeNode parent;
        private readonly Vector2 halfBounds;

        public QuadTreeNode(QuadTreeNode parent, Vector2 halfBounds)
        {
            this.parent = parent;
            this.halfBounds = halfBounds;
        }

        public void Add(RectangleF rectangle)
        {
            var value = new RectangleF(rectangle.Position, rectangle.Size);
            
            if (values.Count < 4 && children[0] == null)
                values.Add(value);
            else
                BalanceNode(value);
        }

        public void Remove(RectangleF rectangle)
        {
            if (children[0] == null)
            {
                if (!values.Contains(rectangle))
                    return;
                    // throw new KeyNotFoundException();

                values.Remove(rectangle);
                return;
            }

            if (CanBeValueInChild(rectangle, out var child))
                child.Remove(rectangle);
            else
                values.Remove(rectangle);
        }

        public LinkedList<(RectangleF, RectangleF)> GetIntersections()
        {
            return GetIntersectionReq();
        }

        private LinkedList<(RectangleF, RectangleF)> GetIntersectionReq()
        {
            var intersections = new LinkedList<(RectangleF, RectangleF)>();
            if (values.Count == 0 && children[0] == null)
                return new LinkedList<(RectangleF, RectangleF)>();

            if (children[0] == null)
            {
                foreach (var intersection in CheckIntersections())
                    intersections.AddLast(intersection);
                return intersections;
            }

            for (var i = 0; i < 4; i++)
            {
                foreach (var intersection in children[i].GetIntersectionReq())
                    intersections.AddLast(intersection);
                
            }

            return intersections;
        }

        private IEnumerable<(RectangleF, RectangleF)> CheckIntersections()
        {
            for (var i = 0; i < values.Count - 1; i++)
                for (var j = i + 1; j <  values.Count; j++)
                    if (values[i].Intersects(values[j]))
                        yield return (values[i], values[j]);
        }
        
        // private IEnumerable<(RectangleF, RectangleF)> CheckIntersectionsWithParentValues()
        // {
        //     foreach (var rectangle in values)
        //     {
        //         
        //     }
        // }

        // private LinkedList<RectangleF> GetChildrenValues()
        // {
        //     var 
        // }

        private void BalanceNode(RectangleF rectangle)
        {
            if (children[0] == null)
            {
                CreateChildren();
                
                var valuesNow = new List<RectangleF>(values); 
                foreach (var value in valuesNow)
                    AddValue(value);
                
                foreach (var value in valuesNow)
                    values.Remove(value);
            }

            AddValue(rectangle);
        }

        private void AddValue(RectangleF rectangle)
        {
            if (CanBeValueInChild(rectangle, out var child))
                child.Add(rectangle);
            else
                values.Add(rectangle);
        }

        private bool CanBeValueInChild(RectangleF rectangle, out QuadTreeNode child)
        {
            var childForTopLeft = GetChildByPoint(rectangle.TopLeft);
            var childForBottomRight = GetChildByPoint(rectangle.BottomRight);

            child = childForTopLeft;

            return childForTopLeft != null && childForTopLeft == childForBottomRight;
        }

        private QuadTreeNode GetChildByPoint(Vector2 point)
        {
            if (Math.Abs(point.X - halfBounds.X) < 0.0001 || Math.Abs(point.Y - halfBounds.Y) < 0.0001)
                return null;

            var childrenNumber = point.X > halfBounds.X
                ? point.Y > halfBounds.Y ? 3 : 1
                : point.Y > halfBounds.Y ? 2 : 0;

            return children[childrenNumber];
        }

        private void CreateChildren()
        {
            children[0] = new QuadTreeNode(this, halfBounds / 2);
            children[1] = new QuadTreeNode(this, new Vector2(halfBounds.X * 1.5f, halfBounds.Y / 2));
            children[2] = new QuadTreeNode(this, new Vector2(halfBounds.X / 2, halfBounds.Y  * 1.5f));
            children[3] = new QuadTreeNode(this, halfBounds * 1.5f);
        }

        public IEnumerator<RectangleF> GetEnumerator()
        {
            foreach (var value in values)
                yield return value;
        
            if (children[0] == null) yield break;
            foreach (var child in children)
                foreach (var value in child)
                    yield return value;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public IEnumerator<RectangleF> GetEnumerator()
    {
        foreach (var val in Root)
            yield return val;
    }
    
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}