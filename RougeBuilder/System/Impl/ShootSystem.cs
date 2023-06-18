using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using RougeBuilder.Component.Impl;
using RougeBuilder.Global;
using RougeBuilder.Model;
using RougeBuilder.State;

namespace RougeBuilder.System;

public class ShootSystem : AbstractSystem<PlayerControllable>
{
    private TimeSpan lastShoot = Time.GameTime.TotalGameTime;
    private OnGameState _onGameState;
    
    public ShootSystem(OnGameState onGameState)
    {
        _onGameState = onGameState;
    }

    protected override void UpdateEntity(AbstractEntity entity)
    {
        var keyboardState = Keyboard.GetState(); 

        var shootReload = Time.GameTime.TotalGameTime.Subtract(lastShoot).TotalMilliseconds;
        if (!keyboardState.IsKeyDown(Keys.Space) || shootReload < 200) 
            return;
        var positional = entity.GetComponent<Positional>().Position;
        var mousePosition = Mouse.GetState().Position.ToVector2();
        var bulletAngle = mousePosition.Translate(-Graphics.GraphicsDevice.Viewport.Width / 2, -Graphics.GraphicsDevice.Viewport.Height / 2);

        Debug.WriteLine(mousePosition);
        
        var bullet = new Bullet();
        bullet.GetComponent<Positional>().Position = positional;
        bullet.GetComponent<Movable>().Velocity = bulletAngle.NormalizedCopy() * 0.3f;
        
        lastShoot = Time.GameTime.TotalGameTime;
        _onGameState.entitiesToAdd.Add(bullet);
    }
}