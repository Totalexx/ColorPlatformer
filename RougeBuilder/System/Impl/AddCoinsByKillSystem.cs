using System.Collections.Generic;
using System.Linq;
using RougeBuilder.Component.Impl;
using RougeBuilder.Model;

namespace RougeBuilder.System.Impl;

public class AddCoinsByKillSystem : AbstractSystem<PlayerControllable>
{
    private List<AbstractEntity> toRemove;

    public AddCoinsByKillSystem(List<AbstractEntity> toRemove)
    {
        this.toRemove = toRemove;
    }
    
    protected override void UpdateEntity(AbstractEntity entity)
    {
        entity.GetComponent<Inventory>().Coins += toRemove.Where(e => e.HasComponent<EnemyMarker>()).Count();
    }
}