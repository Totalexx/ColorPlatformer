using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RougeBuilder.Component.Impl;
using RougeBuilder.Model;

namespace RougeBuilder.System.Impl;

public class DrawSystem : AbstractSystem<Drawable>
{
    private readonly SpriteBatch spriteBatch;

    public DrawSystem(SpriteBatch spriteBatch)
    {
        this.spriteBatch = spriteBatch;
    }

    protected override void UpdateEntity(AbstractEntity entity)
    {
        var drawable = entity.GetComponent<Drawable>();
        var positional = entity.GetComponent<Positional>();
        
        spriteBatch.Draw(
            drawable.Texture, 
            positional.Position, 
            null, 
            Color.Transparent, 
            positional.RotateAngle, 
            new Vector2(0, 0), 
            Vector2.One, 
            SpriteEffects.None, 
            drawable.LayerDepth);
    }

    public override void Update(IEnumerable<AbstractEntity> entities)
    {
        
        spriteBatch.Begin();
        base.Update(entities);
        spriteBatch.End();
    }
}
