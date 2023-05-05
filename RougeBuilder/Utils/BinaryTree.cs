using System;
using System.Collections;
using System.Collections.Generic;
using SharpDX;

namespace RougeBuilder.Utils;

public class BinaryTree<T> : IEnumerable<T>
{

    public Node<T> Root { get; private set; }

    public BinaryTree(Node<T> root)
    {
        Root = root;
    }

    public int CountLeaf()
    {
        return CountLeafRecursion(Root);
    }

    public IEnumerable<Node<T>> GetLeafsValues()
    {
        var leafs = new LinkedList<Node<T>>();
        AddLeaf(leafs, Root);
        return leafs;
    }

    private void AddLeaf(LinkedList<Node<T>> nodes, Node<T> node)
    {
        if (node == null)
            return;
        if (node.HasChildren)
        {
            AddLeaf(nodes, node.Left);
            AddLeaf(nodes, node.Right);
            
            return;
        }
        nodes.AddLast(node);
    }
    
    private int CountLeafRecursion(Node<T> node)
    {
        if (node == null)
            return 0;
        if (node.HasChildren)
            return CountLeafRecursion(node.Left) + CountLeafRecursion(node.Right);
        return 1;
    }
    
    public IEnumerator<T> GetEnumerator()
    {
        var leftRoot = Root.Children[0];
        var rigthRoot = Root.Children[0];

        var nodes = new LinkedList<T>();
        EnumerateNode(nodes, leftRoot);
        EnumerateNode(nodes, rigthRoot);
        nodes.AddLast(Root.Value);
        if (Root.Left != null)
            nodes.AddLast(Root.Left.Value);
        if (Root.Right != null)
            nodes.AddLast(Root.Right.Value);
        return nodes.GetEnumerator();
    }

    private void EnumerateNode(LinkedList<T> nodes, Node<T> node)
    {
        if (node == null || !node.HasChildren)
            return;

        EnumerateNode(nodes, node.Left);
        EnumerateNode(nodes, node.Right);
        if (node.Left != null)
            nodes.AddLast(node.Left.Value);
        if (node.Right != null)
            nodes.AddLast(node.Right.Value);
    } 

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class Node<T>
{
    public T Value { get; set; }
    public Node<T>[] Children { get; } = new Node<T>[2];

    public Node<T> Parent { get; private set; }

    public Node<T> Left => Children[0];
    public Node<T> Right => Children[1];
    public bool HasChildren => Left != null || Right != null;

    private int CountChild = 0;
    
    public Node(T value)
    {
        Value = value;
    }

    public void Add(Node<T> child)
    {
        if (CountChild == 2)
            throw new InvalidOperationException("Node can't has more two children");
        Children[CountChild++] = child;
        child.Parent = this;
    }

    private bool Equals(Node<T> other)
    {
        return EqualityComparer<T>.Default.Equals(Value, other.Value) 
               && Equals(Children, other.Children) 
               && Equals(Parent, other.Parent);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((Node<T>)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Value, Children, Parent);
    }
}