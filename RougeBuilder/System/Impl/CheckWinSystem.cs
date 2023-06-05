using System.Collections.Generic;
using RougeBuilder.Component.Impl;
using RougeBuilder.Model;
using RougeBuilder.State;

namespace RougeBuilder.System.Impl;

public class CheckWinSystem : ISystem
{
    private readonly CollisionsCheckSystem _collisionsCheckSystem;
    private readonly OnGameState _onGameState;
    
    public CheckWinSystem(CollisionsCheckSystem collisionsCheckSystem, OnGameState onGameState)
    {
        _collisionsCheckSystem = collisionsCheckSystem;
        _onGameState = onGameState;
    }

    public void Update(IEnumerable<AbstractEntity> entities)
    {
        var collisions = _collisionsCheckSystem.GetCollisions();

        foreach (var collision in collisions)
        {
            if (collision.Item1.Owner.HasComponent<WinMarker>() &&
                collision.Item2.Owner.HasComponent<PlayerControllable>() ||
                collision.Item2.Owner.HasComponent<WinMarker>() &&
                collision.Item1.Owner.HasComponent<PlayerControllable>())
                _onGameState.isWin = true;
        }
    }
}