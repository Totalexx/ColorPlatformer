using Microsoft.Xna.Framework;

namespace RougeBuilder.Component.Impl;

public class Positional : AbstractComponent
{
    public Vector2 Position { get; set; }
    public float RotateAngle { get; set; }

    public Positional(float x = 0f, float y = 0f, float rotateAngle = 0f)
    {
        Position = new Vector2(x, y);
        RotateAngle = rotateAngle;
    }
}