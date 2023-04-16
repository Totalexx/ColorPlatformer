using Microsoft.Xna.Framework;
using RougeBuilder.Component.Impl;

namespace RougeBuilder.Model.Impl;

public class Player : AbstractEntity
{
    public Player()
    {
        AddComponent(new PositionalComponent());
    }
}