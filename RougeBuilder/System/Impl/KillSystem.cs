using RougeBuilder.Component.Impl;
using RougeBuilder.Entity;
using RougeBuilder.Model;
using RougeBuilder.State;

namespace RougeBuilder.System.Impl;

public class KillSystem : AbstractSystem<Health>
{

    private readonly OnGameState _onGameState;

    public KillSystem(OnGameState onGameState)
    {
        _onGameState = onGameState;
    }
    
    protected override void UpdateEntity(AbstractEntity entity)
    {
        if (entity.GetComponent<Health>().HealthSize < 0)
        {
            if (entity.HasComponent<PlayerControllable>())
                _onGameState.playerIsDead = true;
            _onGameState.entitiesToRemove.Add(entity);
        } 
    }
}