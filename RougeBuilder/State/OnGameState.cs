using System.Collections.Generic;
using RougeBuilder.Global;
using RougeBuilder.Model;
using RougeBuilder.Model.Impl;
using RougeBuilder.System;
using RougeBuilder.System.Impl;

namespace RougeBuilder.State;

public class OnGameState : GameState
{
    private readonly LinkedList<AbstractEntity> allEntities = new();
    
    private readonly List<ISystem> gameSystems = new()
    {
        new DrawSystem(),
        new MoveSystem(),
        new PlayerControlSystem(),
    };
    
    public OnGameState(LinkedList<AbstractEntity> gameEntities)
    {
        allEntities = gameEntities;
    }
    
    public override GameState Update()
    {
        foreach (var system in gameSystems)
            system.Update(allEntities);
        
        return this;
    }
    
}