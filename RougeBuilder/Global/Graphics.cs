using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RougeBuilder.Global;

public static class Graphics
{
    public static ContentManager Content { get; private set; }
    
    public static GraphicsDevice GraphicsDevice { get; private set; }

    public static SpriteBatch SpriteBatch { get; private set; }
    
    public static void Initialize(Game game)
    {
        Content = game.Content;
        GraphicsDevice = game.GraphicsDevice;
        SpriteBatch = new SpriteBatch(GraphicsDevice);
        
        game.IsMouseVisible = true;
    }
}