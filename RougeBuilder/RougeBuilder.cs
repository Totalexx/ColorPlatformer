using Microsoft.Xna.Framework;
using RougeBuilder.Global;
using RougeBuilder.State;

namespace RougeBuilder;

public class RougeBuilder : Game
{
    private GraphicsDeviceManager _graphics;
    private GameState _gameState;
    
    public RougeBuilder()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
    }

    protected override void Initialize()
    {
        Graphics.Initialize(this);
        _gameState = new RunGameState();
        
        base.Initialize();
    }

    protected override void Update(GameTime gameTime)
    {
        Graphics.SpriteBatch.Begin();
        GraphicsDevice.Clear(Color.CornflowerBlue);
        
        Time.GameTime = gameTime;
        _gameState = _gameState.Update();
        
        Graphics.SpriteBatch.End();
        base.Update(gameTime);
    }
}