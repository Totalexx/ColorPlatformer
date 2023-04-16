using Microsoft.Xna.Framework;

namespace RougeBuilder.Component.Impl;

public class PositionalComponent : AbstractComponent
{
    public Vector2 Position { get; set; }
    public float RotateAngle { get; set; }

    public PositionalComponent() : this(0f, 0f, 0f) {}
    
    public PositionalComponent(float x, float y, float rotateAngle)
    {
        Position = new Vector2(x, y);
        RotateAngle = rotateAngle;
    }
}