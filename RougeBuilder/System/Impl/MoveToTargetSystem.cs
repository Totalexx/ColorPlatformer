using Microsoft.Xna.Framework;
using MonoGame.Extended;
using RougeBuilder.Component.Impl;
using RougeBuilder.Model;

namespace RougeBuilder.System.Impl;

public class MoveToTargetSystem : AbstractSystem<MoveToTarget>
{
    protected override void UpdateEntity(AbstractEntity entity)
    {
        var target = entity.GetComponent<MoveToTarget>().target;
        var walkerPosition = entity.GetComponent<Positional>().Position;

        var distance = target.Position - walkerPosition;

        if (distance.Length() > 200)
        {
            entity.GetComponent<Movable>().Velocity = Vector2.Zero;
            return;
        }
        
        var vectorMove = (target.Position - walkerPosition).NormalizedCopy() * 0.05f;
        entity.GetComponent<Movable>().Velocity = vectorMove;
    }
}