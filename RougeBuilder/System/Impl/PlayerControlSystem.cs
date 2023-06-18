using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RougeBuilder.Component.Impl;
using RougeBuilder.Entity;
using RougeBuilder.Entity.Impl;
using RougeBuilder.Model;
using Keyboard = Microsoft.Xna.Framework.Input.Keyboard;

namespace RougeBuilder.System.Impl;

public class PlayerControlSystem : AbstractSystem<PlayerControllable>
{
    private float playerVelocity = 0.15f;
    private KeyState previousQState;
    private List<AbstractEntity> toAdd;

    public PlayerControlSystem(List<AbstractEntity> toAdd)
    {
        this.toAdd = toAdd;
    }

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

        if (keyboardState.IsKeyUp(Keys.Q) && previousQState == KeyState.Down)
        {
            var inventory = entity.GetComponent<Inventory>();
            if (inventory.Coins >= 3)
            {
                var bomb = new Bomb();
                bomb.GetComponent<Positional>().Position = entity.GetComponent<Positional>().Position;
                toAdd.Add(bomb);
                inventory.Coins -= 3;
            }
        }

        if (!velocity.Equals(Vector2.Zero))
        {
            velocity.Normalize();
            velocity *= playerVelocity;            
        }
        
        movable.Velocity = velocity;
        previousQState = keyboardState[Keys.Q];
    }
}