using Microsoft.Xna.Framework;
using RougeBuilder.Component.Impl;

namespace RougeBuilder.Model.Impl;

public class Player : AbstractEntity
{
    private readonly PositionalComponent _positionalComponent;
    
    public Player()
    {
        AddComponent(new PositionalComponent());
        _positionalComponent = GetComponent<PositionalComponent>();
    }

    public Vector2 TestGetPosition()
    {
        return _positionalComponent.GetPosition();
    }
    
    public void TestMovePlayer(Vector2 vector2)
    {
        _positionalComponent.MoveOn(vector2);
    }
}