using Microsoft.Xna.Framework;
using MonoGame.Extended.ViewportAdapters;
using RougeBuilder.Global;
using RougeBuilder.State;
using RougeBuilder.Utils;

namespace RougeBuilder;

public class RougeBuilder : Game
{
    private GraphicsDeviceManager _graphics;
    private GameState _gameState;

    private BoxingViewportAdapter viewportAdapter;
    
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
        Graphics.DrawBegin();
        
        Time.GameTime = gameTime;
        _gameState = _gameState.Update();
        
        Graphics.DrawEnd();
        base.Update(gameTime);
    }

}