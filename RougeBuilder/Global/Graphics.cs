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
    
    public static SpriteFont Font { get; private set; }    

    private static Game Game;

    private const int WindowWidth = 800; 
    private const int WindowHeight = 480;

    private static readonly Color backgroundColor = new (11,11,11);

    public static void Initialize(Game game)
    {
        Game = game;
        SetGraphicsVariables();
        SetWindowSettings();
        CreateCamera();
        CreateFont();
    }

    public static void DrawBegin()
    {
        GraphicsDevice.Clear(backgroundColor);
        SpriteBatch.Begin(
            samplerState: SamplerState.PointWrap, 
            transformMatrix: Camera.GetViewMatrix());
        // SpriteBatch.Begin();
    }

    public static void DrawEnd()
    {
        // DrawFPS();
        SpriteBatch.End();
    }
    
    private static void CreateCamera()
    {
        var viewport = new BoxingViewportAdapter(Game.Window, GraphicsDevice, WindowWidth, WindowHeight);
        Camera = new OrthographicCamera(viewport)
        {
            Position = new Vector2(0, 0),
            Zoom = 2
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

    private static void CreateFont()
    {
        Font = Content.Load<SpriteFont>("font");
    }
    
    private static void DrawFPS()
    {
        var fps = ((int)(1f / Time.DeltaTime * 1000)).ToString();
        SpriteBatch.DrawString(
            Font, 
            fps, 
            Camera.Position + new Vector2(210, 130), 
            Color.White, 
            0, 
            Vector2.Zero, 
            Vector2.One, 
            SpriteEffects.None, 
            0);
    }
}