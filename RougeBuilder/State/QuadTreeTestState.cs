using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using RougeBuilder.Global;
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
        
        qt.Add(new RectangleF(180, 180, 5, 5));
        qt.Add(new RectangleF(160, 180, 5, 5));
        qt.Add(new RectangleF(190, 180, 5, 5));
        qt.Add(new RectangleF(180, 190, 5, 5));
        qt.Add(new RectangleF(160, 160, 5, 5));
    }
    
    public override GameState Update()
    {
        if (Keyboard.GetState().IsKeyUp(Keys.Q) && previousKeyState == KeyState.Down)
        {
            qt.Add(new RectangleF(_random.Next(0, 180), _random.Next(0, 180), _random.Next(3, 18), _random.Next(3, 18)));
        }
        foreach (var bound in qt.GetBounds())
        {
            Graphics.SpriteBatch.DrawRectangle(bound, Color.Red);
        }
        
        foreach (var bound in qt)
        {
            Graphics.SpriteBatch.DrawRectangle(bound, Color.Aqua);
        }
        
        Graphics.SpriteBatch.DrawRectangle(new RectangleF(0, 0, 200, 200), Color.Blue);
        previousKeyState = Keyboard.GetState()[Keys.Q];
        return this;
    }
}