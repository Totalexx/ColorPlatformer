using System.Numerics;
using RougeBuilder.Component.Impl;
using RougeBuilder.Global;
using RougeBuilder.Model;

namespace RougeBuilder.System.Impl;

public class MoveSystem : AbstractSystem<Movable>
{
    protected override void UpdateEntity(AbstractEntity entity)
    {
        var movable = entity.GetComponent<Movable>();
        var positional = entity.GetComponent<Positional>();
        
        if (movable.Velocity.Equals(Vector2.Zero))
            return;

        positional.Position += movable.Velocity * (float)Time.DeltaTime;
    }
}