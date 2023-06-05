using RougeBuilder.Component.Impl;
using RougeBuilder.Model;
using RougeBuilder.State;

namespace RougeBuilder.System.Impl;

public class KillSystem : AbstractSystem<Health>
{

    private OnGameState _onGameState;

    public KillSystem(OnGameState onGameState)
    {
        _onGameState = onGameState;
    }
    
    protected override void UpdateEntity(AbstractEntity entity)
    {
        if (entity.GetComponent<Health>().health < 0)
        {
            _onGameState.entitiesToRemove.Add(entity);
        } 
    }
}