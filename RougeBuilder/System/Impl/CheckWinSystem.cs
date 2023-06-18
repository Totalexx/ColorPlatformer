using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RougeBuilder.Component.Impl;
using RougeBuilder.Entity;
using RougeBuilder.Global;
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
            if (collision.Item1.Owner.HasComponent<KeyMarker>() &&
                collision.Item2.Owner.HasComponent<PlayerControllable>() ||
                collision.Item2.Owner.HasComponent<KeyMarker>() &&
                collision.Item1.Owner.HasComponent<PlayerControllable>())
            {
                var playerInventory = collision.Item1.Owner.HasComponent<Inventory>()
                    ? collision.Item1.Owner.GetComponent<Inventory>()
                    : collision.Item2.Owner.GetComponent<Inventory>();
                var key = collision.Item1.Owner.HasComponent<KeyMarker>()
                    ? collision.Item1.Owner
                    : collision.Item2.Owner;
                playerInventory.HasKey = true;
                _onGameState.entitiesToRemove.Add(key);
            }
            
            if (collision.Item1.Owner.HasComponent<WinMarker>() &&
                collision.Item2.Owner.HasComponent<PlayerControllable>() ||
                collision.Item2.Owner.HasComponent<WinMarker>() &&
                collision.Item1.Owner.HasComponent<PlayerControllable>())
            {
                var playerInventory = collision.Item1.Owner.HasComponent<Inventory>()
                    ? collision.Item1.Owner.GetComponent<Inventory>()
                    : collision.Item2.Owner.GetComponent<Inventory>();
                
                var chest = collision.Item1.Owner.HasComponent<WinMarker>()
                    ? collision.Item1.Owner
                    : collision.Item2.Owner;
                _onGameState.isWin = playerInventory.HasKey;
                
                chest.GetComponent<Drawable>().Texture = Graphics.Content.Load<Texture2D>("chest");
            }
        }
    }
}