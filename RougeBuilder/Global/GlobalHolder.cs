using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RougeBuilder.Global;

public static class GlobalHolder
{
    public static SpriteBatch SpriteBatch { get; private set; }
    public static ContentManager Content { get; private set; }

    public static void Initialize(Game game)
    {
        Content = game.Content;
        SpriteBatch = new SpriteBatch(game.GraphicsDevice);
    }
}