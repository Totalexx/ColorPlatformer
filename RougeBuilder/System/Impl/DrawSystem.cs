using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RougeBuilder.Component.Impl;
using RougeBuilder.Global;
using RougeBuilder.Model;

namespace RougeBuilder.System.Impl;

public class DrawSystem : AbstractSystem<Drawable>
{
    private readonly SpriteBatch spriteBatch = Graphics.SpriteBatch;

    protected override void UpdateEntity(AbstractEntity entity)
    {
        var drawable = entity.GetComponent<Drawable>();
        var positional = entity.GetComponent<Positional>();
        spriteBatch.Draw(
            drawable.Texture, 
            positional.Position, 
            null, 
            Color.White, 
            positional.RotateAngle, 
            new Vector2(drawable.Texture.Width / 2, drawable.Texture.Height / 2), 
            Vector2.One, 
            SpriteEffects.None, 
            drawable.LayerDepth);
    }
}
