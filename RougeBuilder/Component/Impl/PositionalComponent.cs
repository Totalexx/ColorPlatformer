using Microsoft.Xna.Framework;

namespace RougeBuilder.Component.Impl;

public class PositionalComponent : IComponent
{
    private Vector2 position;

    public PositionalComponent() : this(0, 0) {}
    
    public PositionalComponent(float x, float y)
    {
        position = new Vector2(x, y);
    }

    public void MoveOn(Vector2 moveOn)
    {
        position += moveOn;
    }
    
    public void SetPosition(Vector2 newPosition)
    {
        position.X = newPosition.X;
        position.Y = newPosition.Y;
    }

    public Vector2 GetPosition()
    {
        return new Vector2(position.X, position.Y);
    }
}