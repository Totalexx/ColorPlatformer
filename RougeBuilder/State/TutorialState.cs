using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RougeBuilder.Global;

namespace RougeBuilder.State;

public class TutorialState : GameState
{
    public override GameState Update()
    {
        Graphics.SpriteBatch.DrawString(Graphics.Font, "You need to find a chest and a key", Graphics.Camera.Center - new Vector2(120, 20), Color.White);
        Graphics.SpriteBatch.DrawString(Graphics.Font, "in one of the rooms", Graphics.Camera.Center - new Vector2(70, -10), Color.White);
        Graphics.SpriteBatch.DrawString(Graphics.Font, "Press space to start", Graphics.Camera.Center - new Vector2(70, -80), Color.White);
        
        if (Keyboard.GetState().IsKeyDown(Keys.Space))
            return new GameLoadingState();
        return this;
    }
}