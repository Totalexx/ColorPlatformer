using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        GlobalHolder.Initialize(this);
        _gameState = new GameState();
        base.Initialize();
    }

    protected override void Update(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _gameState.Update();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
    }
}