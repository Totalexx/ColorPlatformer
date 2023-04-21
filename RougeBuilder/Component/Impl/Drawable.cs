using Microsoft.Xna.Framework.Graphics;
using RougeBuilder.Exception;
using RougeBuilder.Model;

namespace RougeBuilder.Component.Impl;

public class Drawable : AbstractComponent
{
    public float LayerDepth { get; set; }

    public Texture2D Texture { get; set; }

    public Drawable(Texture2D texture = null, float layerDepth = 0)
    {
        Texture = texture;
        LayerDepth = layerDepth;
    }

    public override void CheckCorrectComponent()
    {
        if (!HasOwnerComponent<Positional>())
            throw new OwnerHasNotDependentComponentException("Positional required for Drawable");
    }
}