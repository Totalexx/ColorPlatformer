using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RougeBuilder.Component.Impl;
using RougeBuilder.Model;
using Keyboard = Microsoft.Xna.Framework.Input.Keyboard;

namespace RougeBuilder.System.Impl;

public class PlayerControlSystem : AbstractSystem<PlayerControllable>
{
    private float playerVelocity = 1f;

    protected override void UpdateEntity(AbstractEntity entity)
    {
        var movable = entity.GetComponent<Movable>();
        var keyboardState = Keyboard.GetState();

        var velocity = new Vector2();
        
        if (keyboardState.IsKeyDown(Keys.W))
            velocity.Y = -1;
        
        if (keyboardState.IsKeyDown(Keys.S))
            velocity.Y = 1;
        
        if (keyboardState.IsKeyDown(Keys.A))
            velocity.X = -1;
        
        if (keyboardState.IsKeyDown(Keys.D))
            velocity.X = 1;

        if (!velocity.Equals(Vector2.Zero))
        {
            velocity.Normalize();
            velocity *= playerVelocity;            
        }
        
        movable.Velocity = velocity;
    }
}