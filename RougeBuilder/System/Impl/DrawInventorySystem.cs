using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RougeBuilder.Component.Impl;
using RougeBuilder.Global;
using RougeBuilder.Model;

namespace RougeBuilder.System.Impl;

public class DrawInventorySystem : AbstractSystem<PlayerControllable>
{
    private readonly Texture2D CoinTexture = Graphics.Content.Load<Texture2D>("coin");
    private readonly Texture2D KeyTexture = Graphics.Content.Load<Texture2D>("key");
    protected override void UpdateEntity(AbstractEntity entity)
    {
        var inventory = entity.GetComponent<Inventory>();
        Graphics.SpriteBatch.Draw(CoinTexture, Graphics.Camera.Center + new Vector2(150, -100), Color.White);
        Graphics.SpriteBatch.DrawString(Graphics.Font,inventory.Coins + "", Graphics.Camera.Center + new Vector2(170, -105), Color.White);
        if (inventory.HasKey)
            Graphics.SpriteBatch.Draw(KeyTexture, Graphics.Camera.Center + new Vector2(150, -80), Color.White);
    }
}