using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace RougeBuilder.Utils;

public class QuadTree
{
    private QuadTreeNode Root;

    public class QuadTreeNode
    {
        private LinkedList<RectangleF> values;
        private QuadTreeNode[] children = new QuadTreeNode[4];
        private QuadTreeNode parent;
        private Vector2 halfBounds;

        public QuadTreeNode(QuadTreeNode parent, Vector2 halfBounds)
        {
            this.parent = parent;
            this.halfBounds = halfBounds;
        }
        
        public void Add(RectangleF rectangle)
        {
            var value = new RectangleF(rectangle.Position, rectangle.Size);
            
            if (values.Count < 4)
                values.AddLast(value);
            else
                BalanceNode(value);
        }

        public void Remove(RectangleF rectangle)
        {
            if (children[0] == null)
            {
                if (!values.Contains(rectangle))
                    throw new KeyNotFoundException();

                values.Remove(rectangle);
                return;
            }
            
            var childForTopLeft = GetChildByPoint(rectangle.TopLeft);
            var childForBottomRight = GetChildByPoint(rectangle.BottomRight);

            if (childForTopLeft != null && childForTopLeft == childForBottomRight)
                childForTopLeft.Add(rectangle);
            else
                values.Remove(rectangle);
        }

        private void BalanceNode(RectangleF rectangle)
        {
            if (children[0] == null)
            {
                CreateChildren();
                foreach (var value in values)
                    AddValue(value);
                values.Clear();
            }
            
            AddValue(rectangle);
        }

        private void AddValue(RectangleF rectangle)
        {
            var childForTopLeft = GetChildByPoint(rectangle.TopLeft);
            var childForBottomRight = GetChildByPoint(rectangle.BottomRight);

            if (childForTopLeft != null && childForTopLeft == childForBottomRight)
                childForTopLeft.Add(rectangle);
            else
                values.AddLast(rectangle);
        }

        public bool HasValueInChild(RectangleF rectangle)
        {
            var childForTopLeft = GetChildByPoint(rectangle.TopLeft);
            var childForBottomRight = GetChildByPoint(rectangle.BottomRight);

            return childForTopLeft != null && childForTopLeft == childForBottomRight;
        }
        
        private QuadTreeNode GetChildByPoint(Vector2 point)
        {
            if (point.X == halfBounds.X || point.Y == halfBounds.Y)
                return null;
            
            var childrenNumber = point.X > halfBounds.X 
                ? point.Y > halfBounds.Y ? 3 : 1 
                : point.Y > halfBounds.Y ? 0 : 2;

            return children[childrenNumber];
        }

        private void CreateChildren()
        {
            children[0] = new QuadTreeNode(this, parent.halfBounds / 2);
            children[1] = new QuadTreeNode(this, new Vector2(parent.halfBounds.X * 1.5f, parent.halfBounds.Y / 2));
            children[2] = new QuadTreeNode(this, new Vector2(parent.halfBounds.X / 2, parent.halfBounds.Y * 1.5f));
            children[3] = new QuadTreeNode(this, parent.halfBounds * 1.5f);
        }
    }
}