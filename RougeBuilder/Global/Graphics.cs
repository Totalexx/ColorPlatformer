using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace RougeBuilder.Global;

public static class Graphics
{
    public static ContentManager Content { get; private set; }
    
    public static GraphicsDevice GraphicsDevice { get; private set; }

    public static SpriteBatch SpriteBatch { get; private set; }
    
    public static OrthographicCamera Camera { get; private set; }

    private static Game Game;

    private const int WindowWidth = 800; 
    private const int WindowHeight = 480; 
    
    public static void Initialize(Game game)
    {
        Game = game;
        SetGraphicsVariables();
        SetWindowSettings();
        CreateCamera();
    }

    public static void DrawBegin()
    {
        GraphicsDevice.Clear(Color.Black);
        SpriteBatch.Begin(
            samplerState: SamplerState.PointWrap, 
            transformMatrix: Camera.GetViewMatrix());
    }

    public static void DrawEnd()
    {
        SpriteBatch.End();
    }
    
    private static void CreateCamera()
    {
        var viewport = new BoxingViewportAdapter(Game.Window, GraphicsDevice, WindowWidth, WindowHeight);
        Camera = new OrthographicCamera(viewport)
        {
            Zoom = 2,
            Position = new Vector2(0, 0)
        };
    }
    
    private static void SetGraphicsVariables()
    {
        Content = Game.Content;
        GraphicsDevice = Game.GraphicsDevice;
        SpriteBatch = new SpriteBatch(GraphicsDevice);
    }
    
    private static void SetWindowSettings()
    {
        Game.IsMouseVisible = true;
        Game.Window.AllowUserResizing = true;
        Game.Window.Title = "Rouge Builder";
    }
}