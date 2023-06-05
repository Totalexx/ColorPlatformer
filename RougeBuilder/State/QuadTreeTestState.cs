using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using RougeBuilder.Utils;

namespace RougeBuilder.State;

public class QuadTreeTestState : GameState
{
    private QuadTree qt = new (new Vector2(200, 200));
    private Random _random = new Random();
    private KeyState previousKeyState;
    protected override void Start()
    {
        qt.Add(new RectangleF(5, 5, 5, 5));
        qt.Add(new RectangleF(4, 4, 5, 5));
        qt.Add(new RectangleF(10, 10, 5, 5));
        qt.Add(new RectangleF(50, 50, 5, 5));
        qt.Add(new RectangleF(53, 51, 5, 5));
        var i = qt.GetIntersections();
    }
    
    public override GameState Update()
    {
        
        return this;
    }
}